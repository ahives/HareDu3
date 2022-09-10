namespace HareDu.Snapshotting.Model;

public record QueueMemoryDetails
{
    /// <summary>
    /// 
    /// </summary>
    public ulong Total { get; init; }
        
    public PagedOut PagedOut { get; init; }
        
    /// <summary>
    /// 
    /// </summary>
    public RAM RAM { get; init; }
}