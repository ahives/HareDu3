namespace HareDu.Diagnostics;

using System;
using HareDu.Snapshotting.Model;

public interface IScanner
{
    /// <summary>
    /// Executes a list of predefined diagnostic sensors against the snapshot and returns a concatenated report of the results from executing said sensors.
    /// </summary>
    /// <param name="snapshot"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    ScannerResult Scan<T>(T snapshot)
        where T : Snapshot;
    
    /// <summary>
    /// Registers a list of observers that receives the output in real-time as each sensor executes.
    /// </summary>
    /// <param name="subscriber"></param>
    /// <param name="subscribers"></param>
    /// <returns></returns>
    IScanner RegisterSubscribers(IObserver<ProbeContext> subscriber, params IObserver<ProbeContext>[] subscribers);
}