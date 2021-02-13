namespace HareDu.Diagnostics
{
    using System;
    using System.Collections.Generic;

    public record AnalyzerContext
    {
        public Guid Id { get; init; }
        
        public IReadOnlyList<AnalyzerSummary> Summary { get; init; }
        
        public DateTimeOffset Timestamp { get; init; }
    }
}