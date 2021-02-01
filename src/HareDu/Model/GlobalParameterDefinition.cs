namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record GlobalParameterDefinition
    {
        [JsonPropertyName("name")]
        public string Name { get; init; }
            
        [JsonPropertyName("value")]
        public object Value { get; init; }
    }
}