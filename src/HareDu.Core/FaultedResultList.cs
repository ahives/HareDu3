namespace HareDu.Core
{
    using System;

    public record FaultedResultList<T> :
        ResultList<T>
    {
        public FaultedResultList()
        {
            Timestamp = DateTimeOffset.UtcNow;
        }
    }
}