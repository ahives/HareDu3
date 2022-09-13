namespace HareDu.Core;

using System;
using System.Linq;

public record SuccessfulResultList<T> :
    ResultList<T>
{
    public SuccessfulResultList()
    {
        HasFaulted = false;
        Timestamp = DateTimeOffset.UtcNow;
    }

    public override bool HasData => Data is not null && Data.Any();
}