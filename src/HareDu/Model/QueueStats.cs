namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents statistical information about a message queue in a broker.
/// </summary>
public record QueueStats
{
    /// <summary>
    /// Represents the total number of messages in the queue that are ready to be delivered to consumers.
    /// </summary>
    [JsonPropertyName("messages_ready")]
    public ulong TotalMessagesReadyForDelivery { get; init; }

    /// <summary>
    /// Provides detailed information about the rate at which messages are ready for delivery to consumers in the queue.
    /// </summary>
    [JsonPropertyName("messages_ready_details")]
    public Rate MessagesReadyForDeliveryDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages that have been delivered to consumers but have not yet been acknowledged.
    /// </summary>
    [JsonPropertyName("messages_unacknowledged")]
    public ulong TotalUnacknowledgedDeliveredMessages { get; init; }

    /// <summary>
    /// Provides details about the rate of messages that have been delivered but not yet acknowledged in the queue.
    /// </summary>
    [JsonPropertyName("messages_unacknowledged_details")]
    public Rate UnacknowledgedDeliveredMessagesDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages currently in the queue.
    /// </summary>
    [JsonPropertyName("messages")]
    public ulong TotalMessages { get; init; }

    /// <summary>
    /// Represents details about the rate of messages in the queue.
    /// </summary>
    [JsonPropertyName("messages_details")]
    public Rate MessageDetails { get; init; }
}