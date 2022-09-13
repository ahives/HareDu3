namespace HareDu.Diagnostics.Scanners;

using System;
using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using Probes;
using Snapshotting.Model;

public class BrokerConnectivityScanner :
    DiagnosticScanner<BrokerConnectivitySnapshot>
{
    IReadOnlyList<DiagnosticProbe> _channelProbes;
    IReadOnlyList<DiagnosticProbe> _connectionProbes;
    IReadOnlyList<DiagnosticProbe> _connectivityProbes;

    public DiagnosticScannerMetadata Metadata => new()
    {
        Identifier = GetType().GetIdentifier()
    };

    public BrokerConnectivityScanner(IReadOnlyList<DiagnosticProbe> probes)
    {
        Configure(probes ?? throw new ArgumentNullException(nameof(probes)));
    }

    public void Configure(IReadOnlyList<DiagnosticProbe> probes)
    {
        _connectionProbes = probes
            .Where(x => x is not null
                        && x.ComponentType == ComponentType.Connection
                        && x.Category != ProbeCategory.Connectivity)
            .ToList();
        _channelProbes = probes
            .Where(x => x is not null
                        && x.ComponentType == ComponentType.Channel
                        && x.Category != ProbeCategory.Connectivity)
            .ToList();
        _connectivityProbes = probes
            .Where(x => x is not null
                        && (x.ComponentType == ComponentType.Connection || x.ComponentType == ComponentType.Channel)
                        && x.Category == ProbeCategory.Connectivity)
            .ToList();
    }

    public IReadOnlyList<ProbeResult> Scan(BrokerConnectivitySnapshot snapshot)
    {
        if (snapshot is null)
            return DiagnosticCache.EmptyProbeResults;
            
        var results = new List<ProbeResult>();
            
        results.AddRange(_connectivityProbes.Select(x => x.Execute(snapshot)));

        if (snapshot.Connections is null)
            return results;
            
        for (int i = 0; i < snapshot.Connections.Count; i++)
        {
            if (snapshot.Connections[i] is null)
                continue;
                
            results.AddRange(_connectionProbes.Select(x => x.Execute(snapshot.Connections[i])));

            if (snapshot.Connections[i].Channels is null)
                continue;
                
            for (int j = 0; j < snapshot.Connections[i].Channels.Count; j++)
            {
                if (snapshot.Connections[i].Channels[j] is null)
                    continue;
                    
                results.AddRange(_channelProbes.Select(x => x.Execute(snapshot.Connections[i].Channels[j])));
            }
        }

        return results;
    }
}