namespace HareDu.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    public record SuccessfulResultList<T> :
        ResultList<T>
    {
        public SuccessfulResultList()
        {
            Errors = new List<Error>();
            Timestamp = DateTimeOffset.UtcNow;
            HasData = !Data.IsNull() && Data.Any();
        }
    }
}