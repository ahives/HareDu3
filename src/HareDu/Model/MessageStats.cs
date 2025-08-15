namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents statistical information about messaging activities in the broker.
/// </summary>
public record MessageStats
{
    /// <summary>
    /// Gets the total count of messages that have been published.
    /// </summary>
    [JsonPropertyName("publish")]
    public ulong TotalMessagesPublished { get; init; }

    /// <summary>
    /// Gets the details of the rate at which messages have been published.
    /// </summary>
    [JsonPropertyName("publish_details")]
    public Rate MessagesPublishedDetails { get; init; }

    /// <summary>
    /// Gets the total count of messages that have been confirmed.
    /// </summary>
    [JsonPropertyName("confirm")]
    public ulong TotalMessagesConfirmed { get; init; }

    /// <summary>
    /// Gets the rate details of messages that have been confirmed.
    /// </summary>
    [JsonPropertyName("confirm_details")]
    public Rate MessagesConfirmedDetails { get; init; }

    /// <summary>
    /// Gets the total count of messages that were returned as unroutable.
    /// </summary>
    [JsonPropertyName("return_unroutable")]
    public ulong TotalUnroutableMessages { get; init; }

    /// <summary>
    /// Gets the details pertaining to the rate of unroutable messages.
    /// </summary>
    [JsonPropertyName("return_unroutable_details")]
    public Rate UnroutableMessagesDetails { get; init; }

    /// <summary>
    /// Gets the total number of disk read operations performed.
    /// </summary>
    [JsonPropertyName("disk_reads")]
    public ulong TotalDiskReads { get; init; }

    /// <summary>
    /// Represents the details of the disk read rate for the system.
    /// </summary>
    [JsonPropertyName("disk_reads_details")]
    public Rate DiskReadDetails { get; init; }

    /// <summary>
    /// Gets the total number of disk write operations performed.
    /// </summary>
    [JsonPropertyName("disk_writes")]
    public ulong TotalDiskWrites { get; init; }

    /// <summary>
    /// Represents the details of disk write rates.
    /// </summary>
    [JsonPropertyName("disk_writes_details")]
    public Rate DiskWriteDetails { get; init; }

    /// <summary>
    /// Gets the total count of messages retrieved via a `get` operation.
    /// </summary>
    [JsonPropertyName("get")]
    public ulong TotalMessageGets { get; init; }

    /// <summary>
    /// Gets the rate details related to message retrieval.
    /// </summary>
    [JsonPropertyName("get_details")]
    public Rate MessageGetDetails { get; init; }

    /// <summary>
    /// Gets the total number of messages retrieved without acknowledgment.
    /// </summary>
    [JsonPropertyName("get_no_ack")]
    public ulong TotalMessageGetsWithoutAck { get; init; }

    /// <summary>
    /// Gets the rate details for messages that were fetched but not acknowledged.
    /// </summary>
    [JsonPropertyName("get_no_ack_details")]
    public Rate MessageGetsWithoutAckDetails { get; init; }

    /// <summary>
    /// Gets the total count of messages that have been delivered.
    /// </summary>
    [JsonPropertyName("deliver")]
    public ulong TotalMessagesDelivered { get; init; }

    /// <summary>
    /// Represents the rate information associated with message delivery.
    /// </summary>
    [JsonPropertyName("deliver_details")]
    public Rate MessageDeliveryDetails { get; init; }

    /// <summary>
    /// Gets the total count of messages delivered without acknowledgment.
    /// </summary>
    [JsonPropertyName("deliver_no_ack")]
    public ulong TotalMessageDeliveredWithoutAck { get; init; }

    /// <summary>
    /// Gets the details regarding the rate at which messages have been delivered without acknowledgment.
    /// </summary>
    [JsonPropertyName("deliver_no_ack_details")]
    public Rate MessagesDeliveredWithoutAckDetails { get; init; }

    /// <summary>
    /// Gets the total count of messages that have been redelivered.
    /// </summary>
    [JsonPropertyName("redeliver")]
    public ulong TotalMessagesRedelivered { get; init; }

    /// <summary>
    /// Gets the rate details of messages that have been redelivered.
    /// </summary>
    [JsonPropertyName("redeliver_details")]
    public Rate MessagesRedeliveredDetails { get; init; }

    /// <summary>
    /// Gets the total count of messages that have been acknowledged.
    /// </summary>
    [JsonPropertyName("ack")]
    public ulong TotalMessagesAcknowledged { get; init; }

    /// <summary>
    /// Gets the details of the rate at which messages are acknowledged.
    /// </summary>
    [JsonPropertyName("ack_details")]
    public Rate MessagesAcknowledgedDetails { get; init; }

    /// <summary>
    /// Gets the total count of messages that have been delivered and gotten.
    /// </summary>
    [JsonPropertyName("deliver_get")]
    public ulong TotalMessageDeliveryGets { get; init; }

    /// <summary>
    /// Provides detailed rate information about messages that were delivered and subsequently consumed by clients.
    /// </summary>
    [JsonPropertyName("deliver_get_details")]
    public Rate MessageDeliveryGetDetails { get; init; }
}