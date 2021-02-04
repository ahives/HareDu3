namespace HareDu.Snapshotting.Model
{
    public record DiskCapacityDetails
    {
        public ulong Available { get; init; }
        
        public decimal Rate { get; init; }
    }
}