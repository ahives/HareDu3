namespace HareDu.Diagnostics.Scanners;

using System;
using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using Probes;
using Snapshotting.Model;

public class BrokerQueuesScanner :
    DiagnosticScanner<BrokerQueuesSnapshot>
{
    IReadOnlyList<DiagnosticProbe> _queueProbes;
    IReadOnlyList<DiagnosticProbe> _exchangeProbes;

    public DiagnosticScannerMetadata Metadata => new()
    {
        Identifier = GetType().GetIdentifier()
    };

    public BrokerQueuesScanner(IReadOnlyList<DiagnosticProbe> probes)
    {
        Configure(probes ?? throw new ArgumentNullException(nameof(probes)));
    }

    public void Configure(IReadOnlyList<DiagnosticProbe> probes)
    {
        _queueProbes = probes
            .Where(x => x is not null && x.ComponentType == ComponentType.Queue)
            .ToList();
        _exchangeProbes = probes
            .Where(x => x is not null && x.ComponentType == ComponentType.Exchange)
            .ToList();
    }

    public IReadOnlyList<ProbeResult> Scan(BrokerQueuesSnapshot snapshot)
    {
        if (snapshot is null)
            return DiagnosticCache.EmptyProbeResults;
            
        var results = new List<ProbeResult>();

        results.AddRange(_exchangeProbes.Select(x => x.Execute(snapshot)));
            
        for (int i = 0; i < snapshot.Queues.Count; i++)
        {
            if (snapshot.Queues[i] is not null)
                results.AddRange(_queueProbes.Select(x => x.Execute(snapshot.Queues[i])));
        }

        return results;
    }
}