namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record VirtualHostDefinition
    {
        [JsonPropertyName("tracing")]
        public bool Tracing { get; init; }
        
        [JsonPropertyName("description")]
        public string Description { get; init; }
        
        [JsonPropertyName("tags")]
        public string Tags { get; init; }
    }
}