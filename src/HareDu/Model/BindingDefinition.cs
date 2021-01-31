namespace HareDu.Model
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public record BindingDefinition
    {
        [JsonPropertyName("routing_key")]
        public string RoutingKey { get; init; }
        
        [JsonPropertyName("arguments")]
        public IDictionary<string, object> Arguments { get; init; }
    }
}