namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Defines the configuration details associated with an operator policy in RabbitMQ.
/// Used to specify various operational constraints and behaviors for queues, such as size limits, overflow behavior, and expiration settings.
/// </summary>
public record OperatorPolicyDefinition
{
    /// <summary>
    /// Defines the maximum number of delivery attempts for a message before it is considered undeliverable.
    /// </summary>
    [JsonPropertyName("delivery-limit")]
    public uint DeliveryLimit { get; init; }

    /// <summary>
    /// Specifies the maximum number of messages that a queue can hold before triggering overflow behavior.
    /// </summary>
    [JsonPropertyName("max-length")]
    public ulong MaxLength { get; init; }

    /// <summary>
    /// Specifies the maximum total size, in bytes, of messages that the queue can hold before triggering overflow behavior.
    /// </summary>
    [JsonPropertyName("max-length-bytes")]
    public ulong MaxLengthBytes { get; init; }

    /// <summary>
    /// Specifies the maximum number of messages that can be kept in memory for the queue before triggering overflow behavior.
    /// </summary>
    [JsonPropertyName("max-in-memory-length")]
    public ulong MaxInMemoryLength { get; init; }

    /// <summary>
    /// Specifies the maximum amount of memory, in bytes, that can be used to hold messages in the queue before overflow behavior is triggered.
    /// </summary>
    [JsonPropertyName("max-in-memory-bytes")]
    public ulong MaxInMemoryBytes { get; init; }

    /// <summary>
    /// Determines the behavior of the queue when its capacity exceeds the defined limit.
    /// </summary>
    [JsonPropertyName("overflow")]
    public QueueOverflowBehavior OverflowBehavior { get; init; }

    /// <summary>
    /// Specifies the duration after which a queue will be considered expired and automatically deleted if it remains unused.
    /// </summary>
    [JsonPropertyName("expires")]
    public ulong AutoExpire { get; init; }

    /// <summary>
    /// Defines the time-to-live (TTL) for a message in the queue, specifying the duration after which the message will be considered expired and removed.
    /// </summary>
    [JsonPropertyName("message-ttl")]
    public ulong MessageTimeToLive { get; init; }

    /// <summary>
    /// Represents the desired target size for a group or batch in a queue.
    /// </summary>
    [JsonPropertyName("target-group-size")]
    public uint TargetGroupSize { get; init; }
}