namespace HareDu.Core;

using System;

public record SuccessfulResults<T> :
    Results<T>
{
    public SuccessfulResults()
    {
        HasFaulted = false;
        Timestamp = DateTimeOffset.UtcNow;
    }
        
    public override bool HasData => Data is not null;
}