namespace HareDu.Diagnostics;

using System;
using HareDu.Snapshotting.Model;

/// <summary>
/// Provides functionality to execute diagnostic sensors on system snapshots and process the results.
/// </summary>
public interface IScanner
{
    /// <summary>
    /// Analyzes the given snapshot and generates a diagnostic report based on its data.
    /// </summary>
    /// <typeparam name="T">The type of the snapshot to be scanned, which must implement the <see cref="Snapshot"/> interface.</typeparam>
    /// <param name="snapshot">The snapshot instance containing data to analyze.</param>
    /// <returns>A <see cref="ScannerResult"/> representing the analysis and diagnostic outcomes.</returns>
    ScannerResult Scan<T>(T snapshot)
        where T : Snapshot;

    /// <summary>
    /// Registers a list of observers that will receive notifications about the probe context during diagnostics.
    /// </summary>
    /// <param name="subscriber">The primary observer to be registered for receiving probe context notifications.</param>
    /// <param name="subscribers">Additional observers to be registered for receiving probe context notifications.</param>
    /// <returns>An instance of <see cref="IScanner"/> with the registered observers.</returns>
    IScanner RegisterSubscribers(IObserver<ProbeContext> subscriber, params IObserver<ProbeContext>[] subscribers);
}