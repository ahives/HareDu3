namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record QueueStats
    {
        [JsonPropertyName("messages_ready")]
        public ulong TotalMessagesReadyForDelivery { get; init; }

        [JsonPropertyName("messages_ready_details")]
        public Rate MessagesReadyForDeliveryDetails { get; init; }
        
        [JsonPropertyName("messages_unacknowledged")]
        public ulong TotalUnacknowledgedDeliveredMessages { get; init; }

        [JsonPropertyName("messages_unacknowledged_details")]
        public Rate UnacknowledgedDeliveredMessagesDetails { get; init; }
        
        [JsonPropertyName("messages")]
        public ulong TotalMessages { get; init; }

        [JsonPropertyName("messages_details")]
        public Rate MessageDetails { get; init; }
    }
}