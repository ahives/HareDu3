namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the churn rates within the RabbitMQ Broker overview details.
/// Includes metrics related to the creation and deletion of queues, connections, and channels.
/// </summary>
public record ChurnRates
{
    /// <summary>
    /// Gets the total number of channels that were closed.
    /// </summary>
    [JsonPropertyName("channel_closed")]
    public ulong TotalChannelsClosed { get; init; }

    /// <summary>
    /// Provides details about the rate at which channels were closed.
    /// </summary>
    [JsonPropertyName("channel_closed_details")]
    public Rate ClosedChannelDetails { get; init; }

    /// <summary>
    /// Gets the total number of channels that were created.
    /// </summary>
    [JsonPropertyName("channel_created")]
    public ulong TotalChannelsCreated { get; init; }

    /// <summary>
    /// Gets the rate details for created channels.
    /// </summary>
    [JsonPropertyName("channel_created_details")]
    public Rate CreatedChannelDetails { get; init; }

    /// <summary>
    /// Gets the total number of connections that were closed.
    /// </summary>
    [JsonPropertyName("connection_closed")]
    public ulong TotalConnectionsClosed { get; init; }

    /// <summary>
    /// Gets the details of the rate at which connections were closed.
    /// </summary>
    [JsonPropertyName("connection_closed_details")]
    public Rate ClosedConnectionDetails { get; init; }

    /// <summary>
    /// Gets the total number of connections that were created.
    /// </summary>
    [JsonPropertyName("connection_created")]
    public ulong TotalConnectionsCreated { get; init; }

    /// <summary>
    /// Gets the details for the rate at which connections are created.
    /// </summary>
    [JsonPropertyName("connection_created_details")]
    public Rate CreatedConnectionDetails { get; init; }

    /// <summary>
    /// Gets the total number of queues that were created.
    /// </summary>
    [JsonPropertyName("queue_created")]
    public ulong TotalQueuesCreated { get; init; }

    /// <summary>
    /// Provides details about the rate at which queues were created.
    /// </summary>
    [JsonPropertyName("queue_created_details")]
    public Rate CreatedQueueDetails { get; init; }

    /// <summary>
    /// Gets the total number of queues that were declared.
    /// </summary>
    [JsonPropertyName("queue_declared")]
    public ulong TotalQueuesDeclared { get; init; }

    /// <summary>
    /// Represents the rate details of declared queues.
    /// </summary>
    [JsonPropertyName("queue_declared_details")]
    public Rate DeclaredQueueDetails { get; init; }

    /// <summary>
    /// Gets the total number of queues that were deleted.
    /// </summary>
    [JsonPropertyName("queue_deleted")]
    public ulong TotalQueuesDeleted { get; init; }

    /// <summary>
    /// Gets details about the rate of deleted queues.
    /// </summary>
    [JsonPropertyName("queue_deleted_details")]
    public Rate DeletedQueueDetails { get; init; }
}