namespace HareDu.Model
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public record ExchangeInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; init; }
        
        [JsonPropertyName("vhost")]
        public string VirtualHost { get; init; }
        
        [JsonPropertyName("type")]
        public ExchangeRoutingType RoutingType { get; init; }
        
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