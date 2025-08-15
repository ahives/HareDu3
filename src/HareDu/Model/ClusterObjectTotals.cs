namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents various totals for objects within a RabbitMQ cluster, including consumers, queues, exchanges, connections, and channels.
/// </summary>
public record ClusterObjectTotals
{
    /// <summary>
    /// Represents the total number of consumers in a cluster.
    /// </summary>
    [JsonPropertyName("consumers")]
    public ulong TotalConsumers { get; init; }

    /// <summary>
    /// Represents the total number of queues in a cluster.
    /// </summary>
    [JsonPropertyName("queues")]
    public ulong TotalQueues { get; init; }

    /// <summary>
    /// Represents the total number of exchanges in a cluster.
    /// </summary>
    [JsonPropertyName("exchanges")]
    public ulong TotalExchanges { get; init; }

    /// <summary>
    /// Represents the total number of connections in a cluster.
    /// </summary>
    [JsonPropertyName("connections")]
    public ulong TotalConnections { get; init; }

    /// <summary>
    /// Represents the total number of channels in a cluster.
    /// </summary>
    [JsonPropertyName("channels")]
    public ulong TotalChannels { get; init; }
}