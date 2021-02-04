namespace HareDu.Snapshotting.Model
{
    public record DiskOperationWallTime
    {
        public decimal Average { get; init; }
        
        public decimal Rate { get; init; }
    }
}