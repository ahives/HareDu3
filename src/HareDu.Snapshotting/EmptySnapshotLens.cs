namespace HareDu.Snapshotting
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class EmptySnapshotLens<T> :
        SnapshotLens<T>
        where T : Snapshot
    {
        public SnapshotHistory<T> History => new EmptySnapshotHistory<T>();

        public SnapshotResult<T> TakeSnapshot(CancellationToken cancellationToken = default) => new EmptySnapshotResult<T>();

        public SnapshotLens<T> RegisterObserver(IObserver<SnapshotContext<T>> observer) => this;

        public SnapshotLens<T> RegisterObservers(IReadOnlyList<IObserver<SnapshotContext<T>>> observers) => this;
    }
}