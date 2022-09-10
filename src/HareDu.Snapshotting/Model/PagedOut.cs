namespace HareDu.Snapshotting.Model;

public record PagedOut
{
    public ulong Total { get; init; }
        
    public ulong Bytes { get; init; }
}