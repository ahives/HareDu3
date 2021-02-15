namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record UserPermissionsDefinition
    {
        [JsonPropertyName("configure")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Configure { get; init; }
        
        [JsonPropertyName("write")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Write { get; init; }
        
        [JsonPropertyName("read")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Read { get; init; }
    }
}