namespace HareDu.Snapshotting;

using System.Collections.Generic;
using System.Linq;
using Model;

public record SnapshotHistory<T> :
    ISnapshotHistory<T>
    where T : Snapshot
{
    readonly IDictionary<string, SnapshotResult<T>> _snapshots;

    public SnapshotHistory(IDictionary<string, SnapshotResult<T>> snapshots)
    {
        _snapshots = snapshots;
    }

    public IReadOnlyList<SnapshotResult<T>> Results => _snapshots.Values.ToList();

    public void PurgeAll() => _snapshots.Clear();

    public void Purge<U>(SnapshotResult<U> result)
        where U : Snapshot => _snapshots.Remove(result.Identifier);
}