namespace HareDu.Snapshotting.Model
{
    public record MessageStoreDetails
    {
        public ulong Total { get; init; }
        
        public decimal Rate { get; init; }
    }
}