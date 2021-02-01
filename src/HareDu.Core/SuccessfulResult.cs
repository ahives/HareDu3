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
            HasFaulted = false;
            Errors = new List<Error>();
            Timestamp = DateTimeOffset.UtcNow;
        }
    }

    public record SuccessfulResult<T> :
        Result<T>
    {
        public SuccessfulResult()
        {
            HasFaulted = false;
            Errors = new List<Error>();
            Timestamp = DateTimeOffset.UtcNow;
        }
        
        public override bool HasData => !Data.IsNull();
    }
}