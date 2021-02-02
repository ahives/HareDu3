namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record ScopedParameterDefinition<T>
    {
        [JsonPropertyName("vhost")]
        public string VirtualHost { get; init; }

        [JsonPropertyName("component")]
        public string Component { get; init; }

        [JsonPropertyName("name")]
        public string ParameterName { get; init; }

        [JsonPropertyName("value")]
        public T ParameterValue { get; init; }
    }
}