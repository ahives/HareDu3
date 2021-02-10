namespace HareDu.Diagnostics.Scanners
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Extensions;
    using Probes;
    using Snapshotting.Model;

    public class ClusterScanner :
        DiagnosticScanner<ClusterSnapshot>
    {
        readonly IReadOnlyList<DiagnosticProbe> _probes;

        IReadOnlyList<DiagnosticProbe> _nodeProbes;
        IReadOnlyList<DiagnosticProbe> _diskProbes;
        IReadOnlyList<DiagnosticProbe> _memoryProbes;
        IReadOnlyList<DiagnosticProbe> _runtimeProbes;
        IReadOnlyList<DiagnosticProbe> _osProbes;
        
        public string Identifier => GetType().GetIdentifier();

        public ClusterScanner(IReadOnlyList<DiagnosticProbe> probes)
        {
            _probes = probes.IsNotNull() ? probes : throw new ArgumentNullException(nameof(probes));

            FilterProbes(_probes);
        }

        public void FilterProbes(IReadOnlyList<DiagnosticProbe> probes)
        {
            _nodeProbes = probes
                .Where(x => x.IsNotNull() && x.ComponentType == ComponentType.Node)
                .ToList();
            _diskProbes = probes
                .Where(x => x.IsNotNull() && x.ComponentType == ComponentType.Disk)
                .ToList();
            _memoryProbes = probes
                .Where(x => x.IsNotNull() && x.ComponentType == ComponentType.Memory)
                .ToList();
            _runtimeProbes = probes
                .Where(x => x.IsNotNull() && x.ComponentType == ComponentType.Runtime)
                .ToList();
            _osProbes = probes
                .Where(x => x.IsNotNull() && x.ComponentType == ComponentType.OperatingSystem)
                .ToList();
        }

        public IReadOnlyList<ProbeResult> Scan(ClusterSnapshot snapshot)
        {
            if (snapshot == null)
                return DiagnosticCache.EmptyProbeResults;
            
            var results = new List<ProbeResult>();

            for (int i = 0; i < snapshot.Nodes.Count; i++)
            {
                if (snapshot.Nodes[i].IsNull())
                    continue;
                
                results.AddRange(_nodeProbes.Select(x => x.Execute(snapshot.Nodes[i])));

                if (snapshot.Nodes[i].Disk.IsNotNull())
                    results.AddRange(_diskProbes.Select(x => x.Execute(snapshot.Nodes[i].Disk)));

                if (snapshot.Nodes[i].Memory.IsNotNull())
                    results.AddRange(_memoryProbes.Select(x => x.Execute(snapshot.Nodes[i].Memory)));

                if (snapshot.Nodes[i].Runtime.IsNotNull())
                    results.AddRange(_runtimeProbes.Select(x => x.Execute(snapshot.Nodes[i].Runtime)));

                if (snapshot.Nodes[i].OS.IsNotNull())
                    results.AddRange(_osProbes.Select(x => x.Execute(snapshot.Nodes[i].OS)));
            }

            return results;
        }
    }
}