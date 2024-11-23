namespace HareDu.Diagnostics.Scanners;

using System.Collections.Generic;
using HareDu.Snapshotting.Model;

public interface DiagnosticScanner<in T>
    where T : Snapshot
{
    /// <summary>
    /// Miscellaneous information pertinent to describing the diagnostic scanner.
    /// </summary>
    ScannerMetadata Metadata { get; }

    /// <summary>
    /// Executes the diagnostic probes against the specified snapshot.
    /// </summary>
    /// <param name="snapshot"></param>
    /// <returns></returns>
    IReadOnlyList<ProbeResult> Scan(T snapshot);
}