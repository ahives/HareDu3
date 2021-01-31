namespace HareDu.Core
{
    using System.Collections.Generic;

    public record ResultList<T> :
        Result
    {
        public IReadOnlyList<T> Data { get; init; }

        public bool HasData { get; init; }
    }
}