namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents information about a binding in RabbitMQ, including source, destination, routing key,
/// arguments, and other binding-specific details.
/// </summary>
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
    /// Specifies the routing key used for binding messages between an exchange and a queue.
    /// </summary>
    [JsonPropertyName("routing_key")]
    public string RoutingKey { get; init; }

    /// <summary>
    /// Collection of key/value pairs that define additional arguments for the binding.
    /// </summary>
    [JsonPropertyName("arguments")]
    public IDictionary<string, object> Arguments { get; init; }

    /// <summary>
    /// Represents the key associated with the binding properties.
    /// </summary>
    [JsonPropertyName("properties_key")]
    public string PropertiesKey { get; init; }
}