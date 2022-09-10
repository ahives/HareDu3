namespace HareDu.Core;

using System;

public record Result
{
    public DateTimeOffset Timestamp { get; init; }

    public DebugInfo DebugInfo { get; init; }

    public virtual bool HasFaulted { get; init; }
}

public record Result<T> :
    Result
{
    public T Data { get; init; }

    public virtual bool HasData { get; init; }
}