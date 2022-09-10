namespace HareDu.Snapshotting.Model;

public record FileDescriptorChurnMetrics
{
    public ulong Available { get; init; }

    public ulong Used { get; init; }

    public decimal UsageRate { get; init; }

    public ulong OpenAttempts { get; init; }

    public decimal OpenAttemptRate { get; init; }

    public decimal AvgTimePerOpenAttempt { get; init; }

    public decimal AvgTimeRatePerOpenAttempt { get; init; }
}