namespace HareDu.Snapshotting.Model
{
    public record TransactionDetails
    {
        public PersistenceDetails RAM { get; init; }
        
        public PersistenceDetails Disk { get; init; }
    }
}