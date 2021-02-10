namespace HareDu.Diagnostics.Scanners
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Extensions;
    using Probes;
    using Snapshotting.Model;

    public class BrokerQueuesScanner :
        DiagnosticScanner<BrokerQueuesSnapshot>
    {
        readonly IReadOnlyList<DiagnosticProbe> _probes;

        IReadOnlyList<DiagnosticProbe> _queueProbes;
        List<DiagnosticProbe> _exchangeProbes;
        
        public string Identifier => GetType().GetIdentifier();

        public BrokerQueuesScanner(IReadOnlyList<DiagnosticProbe> probes)
        {
            _probes = probes.IsNotNull() ? probes : throw new ArgumentNullException(nameof(probes));

            FilterProbes(_probes);
        }

        public void FilterProbes(IReadOnlyList<DiagnosticProbe> probes)
        {
            _queueProbes = probes
                .Where(x => x.IsNotNull() && x.ComponentType == ComponentType.Queue)
                .ToList();
            _exchangeProbes = probes
                .Where(x => x.IsNotNull() && x.ComponentType == ComponentType.Exchange)
                .ToList();
        }

        public IReadOnlyList<ProbeResult> Scan(BrokerQueuesSnapshot snapshot)
        {
            if (snapshot == null)
                return DiagnosticCache.EmptyProbeResults;
            
            var results = new List<ProbeResult>();

            results.AddRange(_exchangeProbes.Select(x => x.Execute(snapshot)));
            
            for (int i = 0; i < snapshot.Queues.Count; i++)
            {
                if (snapshot.Queues[i].IsNotNull())
                    results.AddRange(_queueProbes.Select(x => x.Execute(snapshot.Queues[i])));
            }

            return results;
        }
    }
}