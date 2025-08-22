namespace HareDu.Diagnostics.Model;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents the result of a diagnostic scanner execution, including the scanner's identifier,
/// a unique identifier for the result, a list of probe results, and a timestamp indicating when the diagnostics were run.
/// </summary>
public record ScannerResult
{
    /// <summary>
    /// Gets a unique identifier for the diagnostic scanner result.
    /// This identifier is generated to uniquely distinguish each instance of a
    /// <see cref="ScannerResult"/> and is used to track specific diagnostic outcomes.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the identifier for the diagnostic scanner that generated this result.
    /// This identifier associates the result with a specific scanner to provide
    /// traceability and context about the diagnostic process.
    /// </summary>
    public string ScannerId { get; init; }

    /// <summary>
    /// Gets the collection of diagnostic probe results generated during the scanning process.
    /// Each item in the collection represents the outcome of a specific diagnostic probe,
    /// providing detailed information about its execution status and findings.
    /// </summary>
    public IReadOnlyList<ProbeResult> Results { get; init; }

    /// <summary>
    /// Gets the timestamp indicating when the diagnostic scanner execution occurred.
    /// This property captures the date and time of the scan, allowing precise tracking
    /// of when the diagnostics were performed.
    /// </summary>
    public DateTimeOffset Timestamp { get; init; }
}
