namespace HareDu.Core;

using System;

/// <summary>
/// Represents an error that occurred during the execution of a process or operation,
/// providing details about the reason, criticality, and timestamp of the error.
/// </summary>
public record Error
{
    /// <summary>
    /// Provides a description or explanation for the error that occurred during the execution of a process or operation.
    /// </summary>
    public string Reason { get; init; }

    /// <summary>
    /// Represents the level of severity or importance associated with an error during the execution of a process or operation.
    /// </summary>
    public ErrorCriticality Criticality { get; init; }

    /// <summary>
    /// Represents the date and time at which the error or event occurred, expressed as a DateTimeOffset value.
    /// </summary>
    public DateTimeOffset Timestamp { get; init; }
}