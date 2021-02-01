namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record NodeContext
    {
        [JsonPropertyName("description")]
        public string Description { get; init; }

        [JsonPropertyName("path")]
        public string Path { get; init; }
        
        [JsonPropertyName("port")]
        public string Port { get; init; }
    }
}