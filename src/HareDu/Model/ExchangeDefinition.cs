namespace HareDu.Model
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class ExchangeDefinition
    {
        [JsonPropertyName("type")]
        public string RoutingType { get; init; }
        
        [JsonPropertyName("durable")]
        public bool Durable { get; init; }
        
        [JsonPropertyName("auto_delete")]
        public bool AutoDelete { get; init; }
        
        [JsonPropertyName("internal")]
        public bool Internal { get; init; }
        
        [JsonPropertyName("arguments")]
        public IDictionary<string, object> Arguments { get; init; }
    }
}