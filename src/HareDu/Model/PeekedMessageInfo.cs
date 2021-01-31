namespace HareDu.Model
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public record PeekedMessageInfo
    {
        [JsonPropertyName("payload_bytes")]
        public ulong PayloadBytes { get; init; }
        
        [JsonPropertyName("redelivered")]
        public bool Redelivered { get; init; }
        
        [JsonPropertyName("exchange")]
        public string Exchange { get; init; }
        
        [JsonPropertyName("routing_key")]
        public string RoutingKey { get; init; }
        
        [JsonPropertyName("message_count")]
        public ulong MessageCount { get; init; }
        
        [JsonPropertyName("properties")]
        public PeekedMessageProperties Properties { get; init; }
        
        [JsonPropertyName("payload")]
        public IDictionary<string, object> Payload { get; init; }
        
        [JsonPropertyName("payload_encoding")]
        public string PayloadEncoding { get; init; }
    }
}