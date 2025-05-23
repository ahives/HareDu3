namespace HareDu.Diagnostics;

using System;
using System.Collections.Generic;

/// <summary>
/// Provides functionality to analyze the results of scanner probes and produce aggregated insights
/// or summaries based on the provided analysis criteria.
/// </summary>
public interface IScannerResultAnalyzer
{
    /// <summary>
    /// Analyzes the provided scanner results and generates a summarized list of analyzer summaries
    /// based on the specified filter logic.
    /// </summary>
    /// <param name="report">The report produced by the diagnostic scanner, containing the list of probe results to analyze.</param>
    /// <param name="filterBy">A function used to filter or group the probe results during analysis by specifying the aggregation key.</param>
    /// <returns>A read-only list of summaries derived from the analysis of the provided scanner results.</returns>
    IReadOnlyList<AnalyzerSummary> Analyze(ScannerResult report, Func<ProbeResult, string> filterBy);

    /// <summary>
    /// Registers an observer to receive notifications about analyzer context updates during the analysis process.
    /// </summary>
    /// <param name="observer">The observer instance that will handle notifications concerning the analyzer context.</param>
    /// <returns>The current instance of <see cref="IScannerResultAnalyzer"/> allowing for additional configuration or fluent chaining.</returns>
    IScannerResultAnalyzer RegisterObserver(IObserver<AnalyzerContext> observer);
}