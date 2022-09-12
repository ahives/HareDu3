namespace HareDu.Snapshotting.Lens;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Model;

public interface Lens<T>
    where T : Snapshot
{
    /// <summary>
    /// Recorded history of each <see cref="SnapshotResult{T}"/> taken via calling the TakeSnapshot method.
    /// </summary>
    ISnapshotHistory<T> History { get; }
        
    /// <summary>
    /// Returns combined information from several API calls to the RabbitMQ cluster into a single result accessible through <see cref="History"/>.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<SnapshotResult<T>> TakeSnapshot(CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers an observer which is notified every time a snapshot is taken.
    /// </summary>
    /// <param name="observer"></param>
    /// <returns></returns>
    Lens<T> RegisterObserver(IObserver<SnapshotContext<T>> observer);
        
    /// <summary>
    /// Registers a list of observers which are notified every time a snapshot is taken.
    /// </summary>
    /// <param name="observers"></param>
    /// <returns></returns>
    Lens<T> RegisterObservers(IReadOnlyList<IObserver<SnapshotContext<T>>> observers);
}