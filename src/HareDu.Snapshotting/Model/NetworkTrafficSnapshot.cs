namespace HareDu.Snapshotting.Model;

public record NetworkTrafficSnapshot :
    Snapshot
{
    public ulong MaxFrameSize { get; init; }

    public Packets Sent { get; init; }
        
    public Packets Received { get; init; }
}