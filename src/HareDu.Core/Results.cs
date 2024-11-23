namespace HareDu.Core;

using System.Collections.Generic;

public record Results<T> :
    Result
{
    public virtual IReadOnlyList<T> Data { get; init; }

    public virtual bool HasData { get; init; }
}

public static class Results2
{
    public static Results<T> Missing<T>() => ResultsCache<T>.MissingValues;
}

static class ResultsCache<T>
{
    public static readonly Results<T> MissingValues = new MissingValues<T>();
}
