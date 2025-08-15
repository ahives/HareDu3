namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a request configuration for creating or managing a queue.
/// </summary>
public record QueueRequest
{
    /// <summary>
    /// Specifies the name of the node where the queue will be created.
    /// </summary>
    /// <remarks>
    /// This property identifies the specific RabbitMQ node that will host the queue. If not specified, the system may
    /// use a default or previously configured node. Configuring this value allows for explicit placement
    /// of queues across the cluster, assisting in load balancing or specific partitioning strategies.
    /// </remarks>
    [JsonPropertyName("node")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Node { get; init; }

    /// <summary>
    /// Indicates whether the queue is durable and will survive a broker restart.
    /// </summary>
    /// <remarks>
    /// Durable queues ensure that their configuration and messages are retained even if the broker service is restarted.
    /// When set to true, the queue becomes persistent. If false, the queue will be temporary and only exist during the
    /// broker's runtime as long as it is not manually deleted or disconnected.
    /// </remarks>
    [JsonPropertyName("durable")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Durable { get; init; }

    /// <summary>
    /// Indicates whether the queue will be automatically deleted when it is no longer in use.
    /// </summary>
    /// <remarks>
    /// This property is useful for temporary queues that should be removed when no consumers or bindings remain.
    /// When set to true, the queue will be deleted automatically once its usage conditions are no longer met.
    /// </remarks>
    [JsonPropertyName("auto_delete")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool AutoDelete { get; init; }

    /// <summary>
    /// Represents a collection of key-value pairs that can be used to configure advanced settings for a queue.
    /// </summary>
    /// <remarks>
    /// This property is typically used to specify optional parameters for the queue, such as policies,
    /// time-to-live settings, or other queue-related configurations. The keys represent the parameter names,
    /// and the values represent their corresponding settings or limits.
    /// </remarks>
    [JsonPropertyName("arguments")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IDictionary<string, object> Arguments { get; init; }
}