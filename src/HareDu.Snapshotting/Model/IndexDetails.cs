namespace HareDu.Snapshotting.Model
{
    public record IndexDetails
    {
        public IndexUsageDetails Reads { get; init; }
        
        public IndexUsageDetails Writes { get; init; }
        
        public JournalDetails Journal { get; init; }
    }
}