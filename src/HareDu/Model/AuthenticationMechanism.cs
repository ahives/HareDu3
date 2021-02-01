namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record AuthenticationMechanism
    {
        [JsonPropertyName("name")]
        public string Name { get; init; }

        [JsonPropertyName("description")]
        public string Description { get; init; }

        [JsonPropertyName("enabled")]
        public bool IsEnabled { get; init; }
    }
}