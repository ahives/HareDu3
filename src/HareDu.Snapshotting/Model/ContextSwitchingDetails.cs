namespace HareDu.Snapshotting.Model;

public record ContextSwitchingDetails
{
    public ulong Total { get; init; }
        
    public decimal Rate { get; init; }
}