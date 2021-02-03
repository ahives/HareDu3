namespace HareDu.Snapshotting.Model
{
    public record Packets
    {
        public ulong Total { get; init; }
        
        public ulong Bytes { get; init; }
        
        public decimal Rate { get; init; }
    }
}