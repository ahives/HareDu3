namespace HareDu.Diagnostics.Scanners
{
    using System.Collections.Generic;
    using Core.Extensions;
    using Probes;
    using Snapshotting;

    public class NoOpScanner<T> :
        DiagnosticScanner<T>
        where T : Snapshot
    {
        public DiagnosticScannerMetadata Metadata => new()
        {
            Identifier = GetType().GetIdentifier()
        };
        
        public IReadOnlyList<ProbeResult> Scan(T snapshot) => DiagnosticCache.EmptyProbeResults;
        
        public void Configure(IReadOnlyList<DiagnosticProbe> probes) { }
    }
}