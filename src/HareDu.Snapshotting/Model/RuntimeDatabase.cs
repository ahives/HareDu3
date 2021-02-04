namespace HareDu.Snapshotting.Model
{
    public record RuntimeDatabase
    {
        public TransactionDetails Transactions { get; init; }
        
        public IndexDetails Index { get; init; }
        
        public StorageDetails Storage { get; init; }
    }
}