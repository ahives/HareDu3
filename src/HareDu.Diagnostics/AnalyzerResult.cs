namespace HareDu.Diagnostics
{
    public record AnalyzerResult
    {
        public uint Total { get; init; }

        public decimal Percentage { get; init; }
    }
}