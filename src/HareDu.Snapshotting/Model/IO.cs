namespace HareDu.Snapshotting.Model;

public record IO
{
    public DiskUsageDetails Reads { get; init; }
        
    public DiskUsageDetails Writes { get; init; }
        
    public DiskUsageDetails Seeks { get; init; }

    public FileHandles FileHandles { get; init; }
}