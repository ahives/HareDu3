namespace HareDu.Diagnostics.Scanners;

using System;
using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using Probes;
using Snapshotting.Model;

public class BrokerConnectivityScanner :
    BaseDiagnosticScanner,
    DiagnosticScanner<BrokerConnectivitySnapshot>
{
    IEnumerable<DiagnosticProbe> _connectionProbes;
    IEnumerable<DiagnosticProbe> _channelProbes;
    IEnumerable<DiagnosticProbe> _connectivityProbes;

    public ScannerMetadata Metadata => new()
    {
        Identifier = GetType().GetIdentifier()
    };

    public BrokerConnectivityScanner(IReadOnlyList<DiagnosticProbe> probes)
    {
        Configure(probes ?? throw new ArgumentNullException(nameof(probes)));
    }

    public IReadOnlyList<ProbeResult> Scan(BrokerConnectivitySnapshot snapshot)
    {
        if (snapshot is null)
            return DiagnosticCache.EmptyProbeResults;
        
        var results = new List<ProbeResult>(_connectivityProbes.Select(x => x.Execute(snapshot)));

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

    protected sealed override void Configure(IReadOnlyList<DiagnosticProbe> probes)
    {
        _connectionProbes = probes
            .Where(x => x is not null
                        && x.ComponentType == ComponentType.Connection
                        && x.Category != ProbeCategory.Connectivity);
        _channelProbes = probes
            .Where(x => x is not null
                        && x.ComponentType == ComponentType.Channel
                        && x.Category != ProbeCategory.Connectivity);
        _connectivityProbes = probes
            .Where(x => x is not null
                        && x.ComponentType is ComponentType.Connection or ComponentType.Channel
                        && x.Category == ProbeCategory.Connectivity);
    }
}