namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record TopicPermissionsDefinition
    {
        [JsonPropertyName("exchange")]
        public string Exchange { get; init; }
        
        [JsonPropertyName("write")]
        public string Write { get; init; }
        
        [JsonPropertyName("read")]
        public string Read { get; init; }
    }
}