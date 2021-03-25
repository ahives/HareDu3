namespace HareDu.Model
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public record OperatorPolicyInfo
    {
        [JsonPropertyName("vhost")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string VirtualHost { get; init; }
        
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Name { get; init; }
        
        [JsonPropertyName("pattern")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Pattern { get; init; }
        
        [JsonPropertyName("apply-to")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public OperatorPolicyAppliedTo AppliedTo { get; init; }
        
        [JsonPropertyName("definition")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IDictionary<string, ulong> Definition { get; init; }
        
        [JsonPropertyName("priority")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Priority { get; init; }
    }
}