namespace HareDu.Diagnostics.Model;

using System;

/// <summary>
/// Represents the context of a diagnostic probe, providing information about the outcome
/// of a probe execution along with the timestamp when the probe was evaluated.
/// </summary>
public record ProbeContext
{
    /// <summary>
    /// Represents the result of a diagnostic probe execution, providing key details
    /// such as the outcome, associated components, and supporting data.
    /// </summary>
    public ProbeResult Result { get; init; }

    /// <summary>
    /// Represents the exact date and time when a diagnostic probe result was generated,
    /// providing a temporal reference for when the probe execution occurred.
    /// </summary>
    public DateTimeOffset Timestamp { get; init; }
}