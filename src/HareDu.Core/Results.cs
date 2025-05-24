namespace HareDu.Core;

using System.Collections.Generic;

/// <summary>
/// Represents the result of an operation, providing typed data and metadata
/// about the operation's outcome, such as faulted state and data presence.
/// </summary>
/// <typeparam name="T">The type of data contained in the result.</typeparam>
public record Results<T> :
    Result
{
    /// <summary>
    /// Represents the collection of data associated with the result of an operation.
    /// </summary>
    public virtual IReadOnlyList<T> Data { get; init; }

    /// <summary>
    /// Indicates whether the result contains data.
    /// </summary>
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
