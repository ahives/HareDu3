namespace HareDu.Core;

using System;
using System.Linq;
using Extensions;

public record SuccessfulResultList<T> :
    ResultList<T>
{
    public SuccessfulResultList()
    {
        HasFaulted = false;
        Timestamp = DateTimeOffset.UtcNow;
    }

    public override bool HasData => Data.IsNotNull() && Data.Any();
}