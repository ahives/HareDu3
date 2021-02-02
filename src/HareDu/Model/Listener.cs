namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record Listener
    {
        [JsonPropertyName("node")]
        public string Node { get; init; }
        
        [JsonPropertyName("protocol")]
        public string Protocol { get; init; }
        
        [JsonPropertyName("ip_address")]
        public string IPAddress { get; init; }
        
        [JsonPropertyName("port")]
        public string Port { get; init; }
        
        [JsonPropertyName("socket_opts")]
        public SocketOptions SocketOptions { get; init; }
    }
}