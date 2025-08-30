namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents information about a scoped parameter within a RabbitMQ server, including its virtual host context,
/// associated component, parameter name, and value.
/// </summary>
public record ScopedParameterInfo
{
    /// <summary>
    /// Represents the virtual host associated with a scoped parameter in RabbitMQ.
    /// The virtual host is an isolated namespace within the broker, allowing for
    /// separation of resources such as exchanges and queues.
    /// </summary>
    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    /// <summary>
    /// Represents the specific RabbitMQ component associated with the scoped parameter.
    /// The component typically identifies the aspect of RabbitMQ (e.g., exchange, queue)
    /// to which the parameter is related.
    /// </summary>
    [JsonPropertyName("component")]
    public string Component { get; init; }

    /// <summary>
    /// Gets the name of the scoped parameter.
    /// The name is typically associated with a unique identifier for the parameter within its scope.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// Gets the parameter's configured value associated with the scoped parameter.
    /// The value is represented as a dictionary where the keys are strings and values are objects.
    /// </summary>
    [JsonPropertyName("value")]
    public ScopedParameterValue Value { get; init; }
}