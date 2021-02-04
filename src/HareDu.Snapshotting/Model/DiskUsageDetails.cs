namespace HareDu.Snapshotting.Model
{
    public record DiskUsageDetails
    {
        public ulong Total { get; init; }
        
        public decimal Rate { get; init; }
        
        public Bytes Bytes { get; init; }
        
        public DiskOperationWallTime WallTime { get; init; }
    }
}