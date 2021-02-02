namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record TopicPermissionsInfo
    {
        [JsonPropertyName("user")]
        public string User { get; init; }
        
        [JsonPropertyName("vhost")]
        public string VirtualHost { get; init; }
        
        [JsonPropertyName("exchange")]
        public string Exchange { get; init; }
        
        [JsonPropertyName("write")]
        public string Write { get; init; }
        
        [JsonPropertyName("read")]
        public string Read { get; init; }
    }
}