namespace HareDu.Diagnostics;

/// <summary>
/// Represents the result of an analysis, containing the total count of results and the corresponding percentage.
/// </summary>
public record AnalyzerResult
{
    /// <summary>
    /// Represents the total count of items associated with a specific diagnostic result status.
    /// </summary>
    public uint Total { get; init; }

    /// <summary>
    /// Represents the percentage of a particular diagnostic result category within a set of analysis results.
    /// </summary>
    public decimal Percentage { get; init; }
}