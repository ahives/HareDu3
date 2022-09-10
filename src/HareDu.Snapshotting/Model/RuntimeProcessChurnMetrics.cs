namespace HareDu.Snapshotting.Model;

public record RuntimeProcessChurnMetrics
{
    public ulong Limit { get; init; }
        
    public ulong Used { get; init; }

    public decimal UsageRate { get; init; }
}