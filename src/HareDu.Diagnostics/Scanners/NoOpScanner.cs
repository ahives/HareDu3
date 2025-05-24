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
    public ScannerMetadata Metadata => new() {Identifier = GetType().GetIdentifier()};
        
    public NoOpScanner(IReadOnlyList<DiagnosticProbe> probes) : base(probes)
    {
    }

    public IReadOnlyList<ProbeResult> Scan(T snapshot) => DiagnosticCache.EmptyProbeResults;

    protected override void Configure(IReadOnlyList<DiagnosticProbe> probes) { }
}