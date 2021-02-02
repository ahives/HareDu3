namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record UserPermissionsDefinition
    {
        [JsonPropertyName("configure")]
        public string Configure { get; init; }
        
        [JsonPropertyName("write")]
        public string Write { get; init; }
        
        [JsonPropertyName("read")]
        public string Read { get; init; }
    }
}