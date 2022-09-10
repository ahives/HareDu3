namespace HareDu.Snapshotting.Model;

public record QueueOperation
{
    public ulong Total { get; init; }
        
    public decimal Rate { get; init; }
}