namespace HareDu.Core;

using System;

public record Error
{
    public string Reason { get; init; }

    public DateTimeOffset Timestamp { get; init; }
}