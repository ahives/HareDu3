namespace HareDu.Core;

using System;

public record UnsuccessfulResults<T> :
    Results<T>
{
    public UnsuccessfulResults()
    {
        Data = default;
        HasData = false;
        HasFaulted = false;
        Timestamp = DateTimeOffset.UtcNow;
    }
}