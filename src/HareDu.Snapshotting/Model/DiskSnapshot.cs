namespace HareDu.Snapshotting.Model;

public record DiskSnapshot :
    Snapshot
{
    public string NodeIdentifier { get; init; }
        
    public DiskCapacityDetails Capacity { get; init; }

    public ulong Limit { get; init; }

    public bool AlarmInEffect { get; init; }
        
    public IO IO { get; init; }
}