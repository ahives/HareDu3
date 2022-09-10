namespace HareDu.Core;

using System;
using System.Collections.Generic;

public record FaultedResultList<T> :
    ResultList<T>
{
    public FaultedResultList()
    {
        Data = new List<T>();
        HasData = false;
        HasFaulted = true;
        Timestamp = DateTimeOffset.UtcNow;
    }
}