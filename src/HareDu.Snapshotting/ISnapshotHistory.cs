namespace HareDu.Snapshotting;

using System.Collections.Generic;
using Model;

public interface ISnapshotHistory<T>
    where T : Snapshot
{
    /// <summary>
    /// List of all <see cref="SnapshotResult{T}"/> objects currently in memory.
    /// </summary>
    IReadOnlyList<SnapshotResult<T>> Results { get; }

    /// <summary>
    /// Removes all <see cref="SnapshotResult{T}"/> objects currently in memory.
    /// </summary>
    void PurgeAll();

    /// <summary>
    /// Removes specified <see cref="SnapshotResult{T}"/> object in memory.
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="U"></typeparam>
    void Purge<U>(SnapshotResult<U> result)
        where U : Snapshot;
}