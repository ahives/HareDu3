namespace HareDu.Diagnostics.Scanners;

using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using Model;
using Probes;
using Snapshotting.Model;

public class BrokerQueuesScanner :
    BaseDiagnosticScanner,
    DiagnosticScanner<BrokerQueuesSnapshot>
{
    IReadOnlyList<DiagnosticProbe> _queueProbes;
    IReadOnlyList<DiagnosticProbe> _exchangeProbes;

    public ScannerMetadata Metadata => new() {Identifier = GetType().GetIdentifier()};

    public BrokerQueuesScanner(IReadOnlyList<DiagnosticProbe> probes) : base(probes)
    {
    }

    public IReadOnlyList<ProbeResult> Scan(BrokerQueuesSnapshot snapshot)
    {
        if (snapshot is null)
            return DiagnosticCache.EmptyProbeResults;

        var results = new List<ProbeResult>(_exchangeProbes.Select(x => x.Execute(snapshot)));

        for (int i = 0; i < snapshot.Queues.Count; i++)
        {
            if (snapshot.Queues[i] is not null)
                results.AddRange(_queueProbes.Select(x => x.Execute(snapshot.Queues[i])));
        }

        return results;
    }

    protected sealed override void Configure(IReadOnlyList<DiagnosticProbe> probes)
    {
        _queueProbes = probes
            .Where(x => x is not null && x.ComponentType == ComponentType.Queue)
            .ToList();
        _exchangeProbes = probes
            .Where(x => x is not null && x.ComponentType == ComponentType.Exchange)
            .ToList();
    }
}