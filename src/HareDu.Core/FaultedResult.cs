namespace HareDu.Core
{
    using System;

    public record FaultedResult :
        Result
    {
        public FaultedResult()
        {
            HasFaulted = true;
            Timestamp = DateTimeOffset.UtcNow;
        }
    }

    public record FaultedResult<T> :
        Result<T>
    {
        public FaultedResult()
        {
            Data = default;
            HasData = false;
            HasFaulted = true;
            Timestamp = DateTimeOffset.UtcNow;
        }
    }
}