namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record GarbageCollectionMetrics
    {
        [JsonPropertyName("connection_closed")]
        public ulong ConnectionsClosed { get; init; }

        [JsonPropertyName("channel_closed")]
        public ulong ChannelsClosed { get; init; }

        [JsonPropertyName("consumer_deleted")]
        public ulong ConsumersDeleted { get; init; }
        
        [JsonPropertyName("exchange_deleted")]
        public ulong ExchangesDeleted { get; init; }

        [JsonPropertyName("queue_deleted")]
        public ulong QueuesDeleted { get; init; }

        [JsonPropertyName("vhost_deleted")]
        public ulong VirtualHostsDeleted { get; init; }

        [JsonPropertyName("node_node_deleted")]
        public ulong NodesDeleted { get; init; }

        [JsonPropertyName("channel_consumer_deleted")]
        public ulong ChannelConsumersDeleted { get; init; }
    }
}