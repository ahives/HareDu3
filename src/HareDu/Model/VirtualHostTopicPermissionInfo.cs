namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents topic permission information for a specific virtual host in a RabbitMQ broker.
/// </summary>
public record VirtualHostTopicPermissionInfo
{
    /// <summary>
    /// Gets the name of the virtual host.
    /// A virtual host provides a logical grouping of connections, exchanges, queues, and bindings within a single RabbitMQ broker.
    /// </summary>
    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    /// <summary>
    /// Gets the name of the user associated with the virtual host topic permission.
    /// The user specifies which entity is granted permissions to perform specific actions within the virtual host.
    /// </summary>
    [JsonPropertyName("user")]
    public string User { get; init; }

    /// <summary>
    /// Gets the name of the exchange.
    /// An exchange is a component in RabbitMQ that routes messages to queues based on defined rules called bindings.
    /// </summary>
    [JsonPropertyName("exchange")]
    public string Exchange { get; init; }

    /// <summary>
    /// Gets the write permission for the specified exchange in the virtual host.
    /// This defines the topics or message-routing patterns the user has permission to write to.
    /// </summary>
    [JsonPropertyName("write")]
    public string Write { get; init; }

    /// <summary>
    /// Gets the read permission associated with the user on the specified exchange within a virtual host.
    /// This permission defines the ability to consume messages from the exchange.
    /// </summary>
    [JsonPropertyName("read")]
    public string Read { get; init; }
}