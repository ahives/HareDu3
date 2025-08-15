namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents information about a consumer in RabbitMQ.
/// </summary>
public record ConsumerInfo
{
    /// <summary>
    /// Defines the maximum number of messages that can be delivered to a consumer but remain unacknowledged at any time.
    /// </summary>
    /// <remarks>
    /// This property is used to limit the number of unacknowledged messages sent to a consumer, helping to manage memory and
    /// ensure the consumer is not overwhelmed. A value of 0 indicates no specific limit is enforced.
    /// </remarks>
    [JsonPropertyName("prefetch_count")]
    public ulong PreFetchCount { get; init; }

    /// <summary>
    /// Represents a collection of additional arguments or metadata associated with a consumer.
    /// </summary>
    /// <remarks>
    /// Arguments are key-value pairs that can be used to pass additional information or customize the behavior of a consumer.
    /// These may vary based on specific use-case requirements or broker features.
    /// </remarks>
    [JsonPropertyName("arguments")]
    public IDictionary<string, object> Arguments { get; init; }

    /// <summary>
    /// Indicates whether message acknowledgements are required from the consumer.
    /// </summary>
    /// <remarks>
    /// When set to true, the consumer must explicitly acknowledge each message it processes. This is used to ensure
    /// message reliability by preventing message loss in case of a consumer failure.
    /// </remarks>
    [JsonPropertyName("ack_required")]
    public bool AcknowledgementRequired { get; init; }

    /// <summary>
    /// Indicates whether the consumer is the exclusive consumer of the queue.
    /// </summary>
    /// <remarks>
    /// When set to true, the consumer has exclusive access to the queue, preventing other consumers from receiving messages.
    /// This is used to ensure that only one consumer processes messages from the specified queue.
    /// </remarks>
    [JsonPropertyName("exclusive")]
    public bool Exclusive { get; init; }

    /// <summary>
    /// Represents a unique identifier assigned to a consumer within a messaging system.
    /// </summary>
    /// <remarks>
    /// The consumer tag is used to identify a specific consumer instance associated with a queue,
    /// enabling operations and tracking at the consumer level.
    /// </remarks>
    [JsonPropertyName("consumer_tag")]
    public string ConsumerTag { get; init; }

    /// <summary>
    /// Provides detailed information about a specific channel associated with a consumer in a messaging system.
    /// </summary>
    /// <remarks>
    /// This includes metadata regarding the channel's connection, such as peer information, connection name, node, and user.
    /// </remarks>
    [JsonPropertyName("channel_details")]
    public ChannelDetails ChannelDetails { get; init; }

    /// <summary>
    /// Represents the details of a queue associated with a consumer in a RabbitMQ virtual host.
    /// </summary>
    /// <remarks>
    /// This includes information about the name of the queue and the virtual host it resides in.
    /// </remarks>
    [JsonPropertyName("queue")]
    public QueueConsumerDetails QueueConsumerDetails { get; init; }
}