namespace HareDu.Snapshotting
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public interface SnapshotLens<T>
        where T : Snapshot
    {
        SnapshotHistory<T> History { get; }
        
        SnapshotResult<T> TakeSnapshot(CancellationToken cancellationToken = default);

        SnapshotLens<T> RegisterObserver(IObserver<SnapshotContext<T>> observer);
        
        SnapshotLens<T> RegisterObservers(IReadOnlyList<IObserver<SnapshotContext<T>>> observers);
    }
}