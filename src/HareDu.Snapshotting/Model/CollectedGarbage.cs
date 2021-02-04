namespace HareDu.Snapshotting.Model
{
    public record CollectedGarbage
    {
        public ulong Total { get; init; }
        
        public decimal Rate { get; init; }
    }
}