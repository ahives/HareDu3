namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record ClusterObjectTotals
    {
        [JsonPropertyName("consumers")]
        public ulong TotalConsumers { get; init; }
        
        [JsonPropertyName("queues")]
        public ulong TotalQueues { get; init; }
        
        [JsonPropertyName("exchanges")]
        public ulong TotalExchanges { get; init; }
        
        [JsonPropertyName("connections")]
        public ulong TotalConnections { get; init; }
        
        [JsonPropertyName("channels")]
        public ulong TotalChannels { get; init; }
    }
}