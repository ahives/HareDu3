namespace HareDu.Snapshotting;

using System;
using Model;

public record SnapshotContext<T>
    where T : Snapshot
{
    public string Identifier { get; init; }

    public T Snapshot { get; init; }

    public DateTimeOffset Timestamp { get; init; }
}