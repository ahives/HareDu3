namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record ScopedParameterDefinition<T>
    {
        [JsonPropertyName("vhost")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string VirtualHost { get; init; }

        [JsonPropertyName("component")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Component { get; init; }

        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string ParameterName { get; init; }

        [JsonPropertyName("value")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T ParameterValue { get; init; }
    }
}