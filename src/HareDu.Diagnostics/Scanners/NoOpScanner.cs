namespace HareDu.Diagnostics.Scanners;

using System.Collections.Generic;
using Core.Extensions;
using Probes;
using HareDu.Snapshotting.Model;

public class NoOpScanner<T> :
    BaseDiagnosticScanner,
    DiagnosticScanner<T>
    where T : Snapshot
{
    public ScannerMetadata Metadata => new()
    {
        Identifier = GetType().GetIdentifier()
    };
        
    public IReadOnlyList<ProbeResult> Scan(T snapshot) => DiagnosticCache.EmptyProbeResults;

    protected override void Configure(IReadOnlyList<DiagnosticProbe> probes) { }
}