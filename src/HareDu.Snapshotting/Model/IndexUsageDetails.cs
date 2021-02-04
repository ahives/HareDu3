namespace HareDu.Snapshotting.Model
{
    public record IndexUsageDetails
    {
        public ulong Total { get; init; }

        public decimal Rate { get; init; }
    }
}