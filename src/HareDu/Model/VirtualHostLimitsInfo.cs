namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents information pertaining to the configured limits for a RabbitMQ virtual host.
/// </summary>
public record VirtualHostLimitsInfo
{
    /// <summary>
    /// Represents the name of the virtual host in RabbitMQ.
    /// </summary>
    /// <remarks>
    /// The virtual host is a namespace within RabbitMQ used to segregate and scope resources such as queues and exchanges.
    /// Each virtual host acts as a mini-RabbitMQ server with its own set of permissions and configurations.
    /// </remarks>
    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    /// <summary>
    /// Represents the limits configured for a virtual host in RabbitMQ.
    /// </summary>
    /// <remarks>
    /// This property is a dictionary containing key-value pairs, where the key is the name of the limit
    /// (e.g., "max-connections", "max-queues") and the value is the numeric limit associated with the key.
    /// </remarks>
    [JsonPropertyName("value")]
    public IDictionary<string, ulong> Limits { get; init; }
}