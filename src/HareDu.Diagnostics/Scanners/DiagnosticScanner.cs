namespace HareDu.Diagnostics.Scanners;

using System.Collections.Generic;
using HareDu.Snapshotting.Model;
using Model;

/// <summary>
/// Represents a diagnostic scanner interface that defines the contract for scanning diagnostic probes against a specific snapshot type.
/// </summary>
/// <typeparam name="T">The type of the snapshot to be scanned. Must implement the <see cref="Snapshot"/> interface.</typeparam>
public interface DiagnosticScanner<in T>
    where T : Snapshot
{
    /// <summary>
    /// A property that provides metadata information about the diagnostic scanner, including its unique identifier.
    /// </summary>
    ScannerMetadata Metadata { get; }

    /// <summary>
    /// Scans the specified snapshot and performs diagnostic analysis using the implemented probes.
    /// </summary>
    /// <typeparam name="T">The type of snapshot to scan, which must implement the <see cref="Snapshot"/> interface.</typeparam>
    /// <param name="snapshot">The snapshot instance to be scanned for diagnostic results.</param>
    /// <returns>A read-only list of <see cref="ProbeResult"/> containing the results of the scan.</returns>
    IReadOnlyList<ProbeResult> Scan(T snapshot);
}