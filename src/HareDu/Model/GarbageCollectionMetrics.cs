namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents metrics related to garbage collection activities in a RabbitMQ environment.
/// </summary>
public record GarbageCollectionMetrics
{
    /// <summary>
    /// Gets the total number of connections that have been closed.
    /// </summary>
    [JsonPropertyName("connection_closed")]
    public ulong ConnectionsClosed { get; init; }

    /// <summary>
    /// Gets the total number of channels that have been closed.
    /// </summary>
    [JsonPropertyName("channel_closed")]
    public ulong ChannelsClosed { get; init; }

    /// <summary>
    /// Gets the total number of consumers that have been deleted.
    /// </summary>
    [JsonPropertyName("consumer_deleted")]
    public ulong ConsumersDeleted { get; init; }

    /// <summary>
    /// Gets the total number of exchanges that have been deleted.
    /// </summary>
    [JsonPropertyName("exchange_deleted")]
    public ulong ExchangesDeleted { get; init; }

    /// <summary>
    /// Gets the total number of queues that have been deleted.
    /// </summary>
    [JsonPropertyName("queue_deleted")]
    public ulong QueuesDeleted { get; init; }

    /// <summary>
    /// Gets the total number of virtual hosts that have been deleted.
    /// </summary>
    [JsonPropertyName("vhost_deleted")]
    public ulong VirtualHostsDeleted { get; init; }

    /// <summary>
    /// Gets the total number of nodes that have been deleted.
    /// </summary>
    [JsonPropertyName("node_node_deleted")]
    public ulong NodesDeleted { get; init; }

    /// <summary>
    /// Gets the total number of channel consumers that have been deleted.
    /// </summary>
    [JsonPropertyName("channel_consumer_deleted")]
    public ulong ChannelConsumersDeleted { get; init; }
}