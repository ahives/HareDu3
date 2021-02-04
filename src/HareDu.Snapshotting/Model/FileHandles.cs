namespace HareDu.Snapshotting.Model
{
    public record FileHandles
    {
        public ulong Recycled { get; init; }
        
        public decimal Rate { get; init; }
    }
}