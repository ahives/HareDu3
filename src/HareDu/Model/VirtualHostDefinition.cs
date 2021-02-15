namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record VirtualHostDefinition
    {
        [JsonPropertyName("tracing")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Tracing { get; init; }
        
        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Description { get; init; }
        
        [JsonPropertyName("tags")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Tags { get; init; }
    }
}