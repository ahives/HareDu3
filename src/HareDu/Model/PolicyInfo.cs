namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents information related to a RabbitMQ policy.
/// </summary>
public record PolicyInfo
{
    /// <summary>
    /// Represents the name of the virtual host associated with the policy in RabbitMQ.
    /// </summary>
    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    /// <summary>
    /// Represents the name of the policy in RabbitMQ.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// Represents the pattern used to match queues or exchanges in RabbitMQ to which the policy is applied.
    /// </summary>
    [JsonPropertyName("pattern")]
    public string Pattern { get; init; }

    /// <summary>
    /// Specifies the type of RabbitMQ entities (e.g., queues, exchanges, or all) to which the policy is applied.
    /// </summary>
    [JsonPropertyName("apply-to")]
    public string AppliedTo { get; init; }

    /// <summary>
    /// Represents a collection of key-value pairs defining the parameters of the policy in RabbitMQ.
    /// </summary>
    [JsonPropertyName("definition")]
    public IDictionary<string, string> Definition { get; init; }

    /// <summary>
    /// Represents the priority level assigned to the policy.
    /// </summary>
    [JsonPropertyName("priority")]
    public int Priority { get; init; }
}