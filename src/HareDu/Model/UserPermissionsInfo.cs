namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record UserPermissionsInfo
    {
        [JsonPropertyName("user")]
        public string User { get; init; }
        
        [JsonPropertyName("vhost")]
        public string VirtualHost { get; init; }
        
        [JsonPropertyName("configure")]
        public string Configure { get; init; }
        
        [JsonPropertyName("write")]
        public string Write { get; init; }
        
        [JsonPropertyName("read")]
        public string Read { get; init; }
    }
}