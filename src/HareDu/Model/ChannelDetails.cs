namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record ChannelDetails
    {
        [JsonPropertyName("peer_host")]
        public string PeerHost { get; init; }
        
        [JsonPropertyName("peer_port")]
        public long PeerPort { get; init; }
        
        [JsonPropertyName("number")]
        public long Number { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; }

        [JsonPropertyName("node")]
        public string Node { get; init; }

        [JsonPropertyName("connection_name")]
        public string ConnectionName { get; init; }

        [JsonPropertyName("user")]
        public string User { get; init; }
    }
}