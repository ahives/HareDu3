namespace HareDu.Core;

using System;

public record FaultedResults<T> :
    Results<T>
{
    public FaultedResults()
    {
        Data = default;
        HasData = false;
        HasFaulted = true;
        Timestamp = DateTimeOffset.UtcNow;
    }
}