namespace HareDu.Snapshotting.Lens;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Model;

public class NoOpLens<T> :
    Lens<T>
    where T : Snapshot
{
    public ISnapshotHistory<T> History => new EmptySnapshotHistory<T>();

    public async Task<SnapshotResult<T>> TakeSnapshot(CancellationToken cancellationToken = default) => new EmptySnapshotResult<T>();

    public Lens<T> RegisterObserver(IObserver<SnapshotContext<T>> observer) => this;

    public Lens<T> RegisterObservers(IReadOnlyList<IObserver<SnapshotContext<T>>> observers) => this;
}