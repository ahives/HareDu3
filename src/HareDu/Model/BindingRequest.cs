namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public record BindingRequest
{
    [JsonPropertyName("routing_key")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string BindingKey { get; init; }
        
    [JsonPropertyName("arguments")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IDictionary<string, object> Arguments { get; init; }
}