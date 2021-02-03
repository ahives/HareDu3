namespace HareDu.Snapshotting.Model
{
    public record ChannelSnapshot :
        Snapshot
    {
        public uint PrefetchCount { get; init; }

        public ulong UncommittedAcknowledgements { get; init; }

        public ulong UncommittedMessages { get; init; }

        public ulong UnconfirmedMessages { get; init; }

        public ulong UnacknowledgedMessages { get; init; }

        public ulong Consumers { get; init; }

        public string Identifier { get; init; }
        
        public string ConnectionIdentifier { get; init; }
        
        public string Node { get; init; }
        
        public QueueOperationMetrics QueueOperations { get; init; }
    }
}