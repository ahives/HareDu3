namespace HareDu.Diagnostics.Extensions;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Model;

public static class AnalyzerExtensions
{
    /// <summary>
    /// Analyzes the provided scanner result using the given analyzer and applies a filter to the probe results.
    /// </summary>
    /// <param name="report">The scanner result to be analyzed.</param>
    /// <param name="analyzer">The analyzer responsible for analyzing the scanner result.</param>
    /// <param name="filter">A function to filter the probe results.</param>
    /// <returns>A read-only list of analysis summaries produced by the analyzer.</returns>
    public static IReadOnlyList<AnalyzerSummary> Analyze(
        [NotNull] this ScannerResult report,
        [NotNull] IScannerResultAnalyzer analyzer,
        [NotNull] Func<ProbeResult, string> filter)
        => analyzer is not null
            ? analyzer.Analyze(report, filter)
            : DiagnosticCache.EmptyAnalyzerSummary;
}