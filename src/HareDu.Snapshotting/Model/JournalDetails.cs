namespace HareDu.Snapshotting.Model;

public record JournalDetails
{
    public IndexUsageDetails Writes { get; init; }
}