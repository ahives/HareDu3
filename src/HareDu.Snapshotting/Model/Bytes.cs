namespace HareDu.Snapshotting.Model
{
    public record Bytes
    {
        public ulong Total { get; init; }
        
        public decimal Rate { get; init; }
    }
}