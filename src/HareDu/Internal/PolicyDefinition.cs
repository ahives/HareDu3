namespace HareDu.Internal
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public record PolicyDefinition
    {
        [JsonPropertyName("pattern")]
        public string Pattern { get; init; }

        [JsonPropertyName("definition")]
        public IDictionary<string, string> Arguments { get; init; }

        [JsonPropertyName("priority")]
        public int Priority { get; init; }

        [JsonPropertyName("apply-to")]
        public string ApplyTo { get; init; }
    }
}