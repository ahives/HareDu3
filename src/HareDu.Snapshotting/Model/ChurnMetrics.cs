namespace HareDu.Snapshotting.Model
{
    public record ChurnMetrics
    {
        public ulong Total { get; init; }
        
        public decimal Rate { get; init; }
    }
}