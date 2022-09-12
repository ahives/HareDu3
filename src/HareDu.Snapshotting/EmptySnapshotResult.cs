namespace HareDu.Snapshotting;

using System;
using Model;

public record EmptySnapshotResult<T> :
    SnapshotResult<T>
    where T : Snapshot
{
    public T Snapshot => throw new HareDuSnapshotException("There is no snapshot present.");
    public DateTimeOffset Timestamp => DateTimeOffset.Now;
}