namespace HareDu.Snapshotting.Model;

public record QueueDepth
{
    public ulong Total { get; init; }
        
    public decimal Rate { get; init; }
}