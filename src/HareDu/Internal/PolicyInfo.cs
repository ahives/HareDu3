namespace HareDu.Internal
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public record PolicyInfo
    {
        [JsonPropertyName("vhost")]
        public string VirtualHost { get; init; }
        
        [JsonPropertyName("name")]
        public string Name { get; init; }
        
        [JsonPropertyName("pattern")]
        public string Pattern { get; init; }
        
        [JsonPropertyName("apply-to")]
        public string AppliedTo { get; init; }

        [JsonPropertyName("definition")]
        public IDictionary<string, string> Definition { get; init; }

        [JsonPropertyName("priority")]
        public int Priority { get; init; }
    }
}