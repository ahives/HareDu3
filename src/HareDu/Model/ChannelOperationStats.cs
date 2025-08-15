namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents statistics related to operations performed on a channel in RabbitMQ.
/// </summary>
public record ChannelOperationStats
{
    /// <summary>
    /// Represents the total number of messages published on the channel.
    /// </summary>
    [JsonPropertyName("publish")]
    public ulong TotalMessagesPublished { get; init; }

    /// <summary>
    /// Provides detailed rate information about the messages published on the channel.
    /// </summary>
    [JsonPropertyName("publish_details")]
    public Rate MessagesPublishedDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages confirmed on the channel.
    /// </summary>
    [JsonPropertyName("confirm")]
    public ulong TotalMessagesConfirmed { get; init; }

    /// <summary>
    /// Represents details about the rate of confirmed messages on the channel.
    /// </summary>
    [JsonPropertyName("confirm_details")]
    public Rate MessagesConfirmedDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages that were not routed on the channel.
    /// </summary>
    [JsonPropertyName("return_unroutable")]
    public ulong TotalMessagesNotRouted { get; init; }

    /// <summary>
    /// Provides details about the rate of messages that were not routed in the channel.
    /// </summary>
    [JsonPropertyName("return_unroutable_details")]
    public Rate MessagesNotRoutedDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages retrieved (get operations) from the channel.
    /// </summary>
    [JsonPropertyName("get")]
    public ulong TotalMessageGets { get; init; }

    /// <summary>
    /// Provides detailed rate information associated with message retrieval operations on the channel.
    /// </summary>
    [JsonPropertyName("get_details")]
    public Rate MessageGetDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages retrieved from the queue without acknowledgment.
    /// </summary>
    [JsonPropertyName("get_no_ack")]
    public ulong TotalMessageGetsWithoutAck { get; init; }

    /// <summary>
    /// Provides details regarding the rate of messages retrieved from the queue without acknowledgement.
    /// </summary>
    [JsonPropertyName("get_no_ack_details")]
    public Rate MessageGetsWithoutAckDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages delivered on the channel.
    /// </summary>
    [JsonPropertyName("deliver")]
    public ulong TotalMessagesDelivered { get; init; }

    /// <summary>
    /// Provides details about the rate of message delivery on the channel.
    /// </summary>
    [JsonPropertyName("deliver_details")]
    public Rate MessageDeliveryDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages delivered on the channel without requiring an acknowledgment.
    /// </summary>
    [JsonPropertyName("deliver_no_ack")]
    public ulong TotalMessageDeliveredWithoutAck { get; init; }

    /// <summary>
    /// Provides the rate details of messages delivered on the channel without requiring an acknowledgment.
    /// </summary>
    [JsonPropertyName("deliver_no_ack_details")]
    public Rate MessagesDeliveredWithoutAckDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages delivered to consumers, including messages obtained using the "basic.get" method.
    /// </summary>
    [JsonPropertyName("deliver_get")]
    public ulong TotalMessageDeliveryGets { get; init; }

    /// <summary>
    /// Provides details about the rate of delivering messages that are acknowledged on the channel.
    /// </summary>
    [JsonPropertyName("deliver_get_details")]
    public Rate MessageDeliveryGetDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages that have been redelivered on the channel.
    /// </summary>
    [JsonPropertyName("redeliver")]
    public ulong TotalMessagesRedelivered { get; init; }

    /// <summary>
    /// Provides the details of messages that have been redelivered in the channel.
    /// </summary>
    [JsonPropertyName("redeliver_details")]
    public Rate MessagesRedeliveredDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages acknowledged on the channel.
    /// </summary>
    [JsonPropertyName("ack")]
    public ulong TotalMessagesAcknowledged { get; init; }

    /// <summary>
    /// Represents the details of the rate at which messages are acknowledged on the channel.
    /// </summary>
    [JsonPropertyName("ack_details")]
    public Rate MessagesAcknowledgedDetails { get; init; }
}