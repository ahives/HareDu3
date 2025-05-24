namespace HareDu.Core;

using System;

/// <summary>
/// Represents the result of an operation, providing information about its success, fault state, and debug details.
/// </summary>
public record Result
{
    /// <summary>
    /// Gets the timestamp indicating when the result was created or initialized.
    /// </summary>
    public DateTimeOffset Timestamp { get; init; }

    /// <summary>
    /// Gets the debug information related to the execution of the operation,
    /// including request, response, stack trace, and error details.
    /// </summary>
    public DebugInfo DebugInfo { get; init; }

    /// <summary>
    /// Indicates whether the operation or process associated with the result has encountered a fault or error.
    /// </summary>
    public virtual bool HasFaulted { get; init; }
}

/// <summary>
/// Provides a representation of the result of an operation, including its success state, fault details, and associated debug information.
/// </summary>
public record Result<T> :
    Result
{
    /// <summary>
    /// Gets the data associated with the result, representing the operation's output or returned value.
    /// </summary>
    public virtual T Data { get; init; }

    /// <summary>
    /// Indicates whether the current result contains valid data. Returns true if the result contains data; otherwise, false.
    /// </summary>
    public virtual bool HasData { get; init; }
}

static class ResultCache<T>
{
    // public static readonly Result<T> ResultEmptyValue = new EmptyValue<T>();
    public static readonly Result<T> MissingValue = new MissingValue<T>();
}