namespace HareDu.Model
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public record QueueDefinition
    {
        [JsonPropertyName("node")]
        public string Node { get; init; }
        
        [JsonPropertyName("durable")]
        public bool Durable { get; init; }
        
        [JsonPropertyName("auto_delete")]
        public bool AutoDelete { get; init; }
                
        [JsonPropertyName("arguments")]
        public IDictionary<string, object> Arguments { get; init; }
    }
}