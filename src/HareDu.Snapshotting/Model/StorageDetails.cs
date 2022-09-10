namespace HareDu.Snapshotting.Model;

public record StorageDetails
{
    public MessageStoreDetails Reads { get; init; }
        
    public MessageStoreDetails Writes { get; init; }
}