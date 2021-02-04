namespace HareDu.Diagnostics
{
    public record AnalyzerSummary
    {
        public string Id { get; init; }
        
        public AnalyzerResult Healthy { get; init; }
        
        public AnalyzerResult Unhealthy { get; init; }
        
        public AnalyzerResult Warning { get; init; }
        
        public AnalyzerResult Inconclusive { get; init; }
    }
}