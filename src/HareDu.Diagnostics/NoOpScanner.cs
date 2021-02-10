namespace HareDu.Diagnostics
{
    using System.Collections.Generic;
    using Core.Extensions;
    using Probes;
    using Scanners;
    using Snapshotting;

    public class NoOpScanner<T> :
        DiagnosticScanner<T>
        where T : Snapshot
    {
        public string Identifier => GetType().GetIdentifier();

        public IReadOnlyList<ProbeResult> Scan(T snapshot) => DiagnosticCache.EmptyProbeResults;
        
        public void Configure(IReadOnlyList<DiagnosticProbe> probes) { }
    }
}