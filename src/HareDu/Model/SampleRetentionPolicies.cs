namespace HareDu.Model
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public record SampleRetentionPolicies
    {
        [JsonPropertyName("global")]
        public IList<ulong> Global { get; init; }
        
        [JsonPropertyName("basic")]
        public IList<ulong> Basic { get; init; }
        
        [JsonPropertyName("detailed")]
        public IList<ulong> Detailed { get; init; }
    }
}