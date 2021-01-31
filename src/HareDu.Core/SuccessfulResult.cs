namespace HareDu.Core
{
    using System;
    using System.Collections.Generic;
    using Extensions;

    public record SuccessfulResult :
        Result
    {
        public SuccessfulResult()
        {
            Timestamp = DateTimeOffset.UtcNow;
        }
    }

    public record SuccessfulResult<T> :
        Result<T>
    {
        public SuccessfulResult()
        {
            Errors = new List<Error>();
            Timestamp = DateTimeOffset.UtcNow;
            HasData = !Data.IsNull();
        }
    }
}