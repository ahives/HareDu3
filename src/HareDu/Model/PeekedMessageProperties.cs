namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public record PeekedMessageProperties
{
    [JsonPropertyName("message_id")]
    public string MessageId { get; init; }
        
    [JsonPropertyName("correlation_id")]
    public string CorrelationId { get; init; }
        
    [JsonPropertyName("delivery_mode")]
    public uint DeliveryMode { get; init; }
        
    [JsonPropertyName("headers")]
    public IDictionary<string, object> Headers { get; init; }
        
    [JsonPropertyName("content_type")]
    public string ContentType { get; init; }
}