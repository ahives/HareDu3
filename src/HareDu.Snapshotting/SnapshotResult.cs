namespace HareDu.Snapshotting;

using System;

public record SnapshotResult<T>
    where T : Snapshot
{
    public string Identifier { get; init; }

    public T Snapshot { get; init; }

    public DateTimeOffset Timestamp { get; init; }
}