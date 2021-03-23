namespace HareDu.Model
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public record ExchangeRequest
    {
        [JsonPropertyName("type")]
        public ExchangeRoutingType RoutingType { get; init; }
        
        [JsonPropertyName("durable")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Durable { get; init; }
        
        [JsonPropertyName("auto_delete")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool AutoDelete { get; init; }
        
        [JsonPropertyName("internal")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Internal { get; init; }
        
        [JsonPropertyName("arguments")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IDictionary<string, object> Arguments { get; init; }
    }
}