namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public record QueueRequest
{
    [JsonPropertyName("node")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Node { get; init; }
        
    [JsonPropertyName("durable")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Durable { get; init; }
        
    [JsonPropertyName("auto_delete")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool AutoDelete { get; init; }
                
    [JsonPropertyName("arguments")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IDictionary<string, object> Arguments { get; init; }
}