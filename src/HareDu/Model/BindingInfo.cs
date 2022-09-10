namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public record BindingInfo
{
    /// <summary>
    /// Name of the source exchange.
    /// </summary>
    [JsonPropertyName("source")]
    public string Source { get; init; }
        
    /// <summary>
    /// Name of the RabbitMQ virtual host object.
    /// </summary>
    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }
        
    /// <summary>
    /// Name of the destination exchange/queue object.
    /// </summary>
    [JsonPropertyName("destination")]
    public string Destination { get; init; }
        
    /// <summary>
    /// Qualifies the destination object by defining the type of object (e.g., queue, exchange, etc.).
    /// </summary>
    [JsonPropertyName("destination_type")]
    public string DestinationType { get; init; }
        
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("routing_key")]
    public string RoutingKey { get; init; }
        
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("arguments")]
    public IDictionary<string, object> Arguments { get; init; }
        
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("properties_key")]
    public string PropertiesKey { get; init; }
}