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
    public virtual T Data { get; init; }

    public virtual bool HasData { get; init; }
}

static class ResultCache<T>
{
    // public static readonly Result<T> ResultEmptyValue = new EmptyValue<T>();
    public static readonly Result<T> MissingValue = new MissingValue<T>();
}