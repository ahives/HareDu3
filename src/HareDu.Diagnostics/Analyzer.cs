namespace HareDu.Diagnostics;

using System;
using System.Collections.Generic;
using Model;

internal static class Analyzer
{
    public static AnalyzerContext Context(List<AnalyzerSummary> result) =>
        new() {Id = Guid.CreateVersion7(DateTimeOffset.UtcNow), Summary = result, Timestamp = DateTimeOffset.UtcNow};
}