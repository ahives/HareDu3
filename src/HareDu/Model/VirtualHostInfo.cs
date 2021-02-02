namespace HareDu.Model
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public record VirtualHostInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; init; }
        
        [JsonPropertyName("tracing")]
        public bool Tracing { get; init; }
        
        [JsonPropertyName("cluster_state")]
        public IDictionary<string, string> ClusterState { get; init; }

        [JsonPropertyName("message_stats")]
        public VirtualHostMessageStats MessageStats { get; init; }

        [JsonPropertyName("recv_oct")]
        public ulong PacketBytesReceived { get; init; }

        [JsonPropertyName("recv_oct_details")]
        public Rate PacketBytesReceivedDetails { get; init; }

        [JsonPropertyName("send_oct")]
        public ulong PacketBytesSent { get; init; }

        [JsonPropertyName("send_oct_details")]
        public Rate PacketBytesSentDetails { get; init; }
        
        [JsonPropertyName("messages_details")]
        public Rate MessagesDetails { get; init; }
        
        [JsonPropertyName("messages")]
        public ulong TotalMessages { get; init; }
        
        [JsonPropertyName("messages_unacknowledged_details")]
        public Rate UnacknowledgedMessagesDetails { get; init; }
        
        [JsonPropertyName("messages_unacknowledged")]
        public ulong UnacknowledgedMessages { get; init; }
        
        [JsonPropertyName("messages_ready_details")]
        public Rate ReadyMessagesDetails { get; init; }
        
        [JsonPropertyName("messages_ready")]
        public ulong ReadyMessages { get; init; }
    }
}