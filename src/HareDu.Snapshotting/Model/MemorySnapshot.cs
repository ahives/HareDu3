namespace HareDu.Snapshotting.Model;

public record MemorySnapshot :
    Snapshot
{
    public string NodeIdentifier { get; init; }
        
    public ulong Used { get; init; }
        
    public decimal UsageRate { get; init; }

    public ulong Limit { get; init; }

    public bool AlarmInEffect { get; init; }
}