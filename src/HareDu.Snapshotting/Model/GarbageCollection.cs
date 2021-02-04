namespace HareDu.Snapshotting.Model
{
    public record GarbageCollection
    {
        public CollectedGarbage ChannelsClosed { get; init; }
        
        public CollectedGarbage ConnectionsClosed { get; init; }

        public CollectedGarbage QueuesDeleted { get; init; }

        public CollectedGarbage ReclaimedBytes { get; init; }
    }
}