namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record TopicPermissionsRequest
    {
        [JsonPropertyName("exchange")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Exchange { get; init; }
        
        [JsonPropertyName("write")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Write { get; init; }
        
        [JsonPropertyName("read")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Read { get; init; }
    }
}