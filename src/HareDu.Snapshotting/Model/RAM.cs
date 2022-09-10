namespace HareDu.Snapshotting.Model;

public record RAM
{
    public ulong Target { get; init; }
        
    /// <summary>
    /// Total messages in RAM that are written to disk.
    /// </summary>
    public ulong Total { get; init; }
        
    /// <summary>
    /// Total size in bytes of the messages that were written to disk from RAM.
    /// </summary>
    public ulong Bytes { get; init; }
        
    public ulong Unacknowledged { get; init; }
        
    public ulong Ready { get; init; }
}