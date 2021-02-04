namespace HareDu.Snapshotting.Model
{
    public record SocketDescriptorChurnMetrics
    {
        public ulong Available { get; init; }

        public ulong Used { get; init; }

        public decimal UsageRate { get; init; }
    }
}