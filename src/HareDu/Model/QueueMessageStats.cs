namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents statistical information about messages associated with a queue.
/// </summary>
public record QueueMessageStats
{
    /// <summary>
    /// Represents the total number of messages published to the queue.
    /// </summary>
    [JsonPropertyName("publish")]
    public ulong TotalMessagesPublished { get; init; }

    /// <summary>
    /// Holds details about the rate of messages being published to the queue.
    /// </summary>
    [JsonPropertyName("publish_details")]
    public Rate MessagesPublishedDetails { get; init; }

    /// <summary>
    /// Represents the total number of get operations performed on messages in the queue.
    /// </summary>
    [JsonPropertyName("get")]
    public ulong TotalMessageGets { get; init; }

    /// <summary>
    /// Represents details about the rate of messages retrieved from the queue.
    /// </summary>
    [JsonPropertyName("get_details")]
    public Rate MessageGetDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages retrieved from the queue without acknowledgment.
    /// </summary>
    [JsonPropertyName("get_no_ack")]
    public ulong TotalMessageGetsWithoutAck { get; init; }

    /// <summary>
    /// Represents the details of the rate at which messages are retrieved from the queue without acknowledgment.
    /// </summary>
    [JsonPropertyName("get_no_ack_details")]
    public Rate MessageGetsWithoutAckDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages delivered from the queue.
    /// </summary>
    [JsonPropertyName("deliver")]
    public ulong TotalMessagesDelivered { get; init; }

    /// <summary>
    /// Represents the message delivery rate details for the queue.
    /// </summary>
    [JsonPropertyName("deliver_details")]
    public Rate MessageDeliveryDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages delivered from the queue that were not acknowledged.
    /// </summary>
    [JsonPropertyName("deliver_no_ack")]
    public ulong TotalMessageDeliveredWithoutAck { get; init; }

    /// <summary>
    /// Represents the rate details of messages delivered to the queue without acknowledgments.
    /// </summary>
    [JsonPropertyName("deliver_no_ack_details")]
    public Rate MessagesDeliveredWithoutAckDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages delivered and subsequently fetched from the queue.
    /// </summary>
    [JsonPropertyName("deliver_get")]
    public ulong TotalMessageDeliveryGets { get; init; }

    /// <summary>
    /// Provides detailed rate information about messages delivered and subsequently acknowledged from the queue.
    /// </summary>
    [JsonPropertyName("deliver_get_details")]
    public Rate MessageDeliveryGetDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages that were redelivered in the queue.
    /// </summary>
    [JsonPropertyName("redeliver")]
    public ulong TotalMessagesRedelivered { get; init; }

    /// <summary>
    /// Provides details on the rate of messages redelivered in the queue.
    /// </summary>
    [JsonPropertyName("redeliver_details")]
    public Rate MessagesRedeliveredDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages acknowledged in the queue.
    /// </summary>
    [JsonPropertyName("ack")]
    public ulong TotalMessagesAcknowledged { get; init; }

    /// <summary>
    /// Contains details about the rate of messages being acknowledged in the queue.
    /// </summary>
    [JsonPropertyName("ack_details")]
    public Rate MessagesAcknowledgedDetails { get; init; }
}