namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents detailed metadata and operational information about the RabbitMQ broker and its current state.
/// </summary>
public record BrokerOverviewInfo
{
    /// <summary>
    /// Gets the version of the management plugin currently in use by the broker.
    /// </summary>
    [JsonPropertyName("management_version")]
    public string ManagementVersion { get; init; }

    /// <summary>
    /// Gets the current mode used to display rates in the broker's statistics.
    /// </summary>
    [JsonPropertyName("rates_mode")]
    public string RatesMode { get; init; }

    /// <summary>
    /// Gets the sample retention policies currently configured for the broker.
    /// </summary>
    [JsonPropertyName("sample_retention_policies")]
    public SampleRetentionPolicies SampleRetentionPolicies { get; init; }

    /// <summary>
    /// Gets the collection of exchange types available in the broker.
    /// </summary>
    [JsonPropertyName("exchange_types")]
    public IList<ExchangeType> ExchangeTypes { get; init; }

    /// <summary>
    /// Gets the version of the product currently in use by the broker.
    /// </summary>
    [JsonPropertyName("product_version")]
    public string ProductVersion { get; init; }

    /// <summary>
    /// Gets the name of the product associated with the broker.
    /// </summary>
    [JsonPropertyName("product_name")]
    public string ProductName { get; init; }

    /// <summary>
    /// Gets the version of the RabbitMQ broker currently in use.
    /// </summary>
    [JsonPropertyName("rabbitmq_version")]
    public string RabbitMqVersion { get; init; }

    /// <summary>
    /// Gets the name of the cluster that the RabbitMQ broker belongs to.
    /// </summary>
    [JsonPropertyName("cluster_name")]
    public string ClusterName { get; init; }

    /// <summary>
    /// Gets the version of the Erlang runtime installed on the broker.
    /// </summary>
    [JsonPropertyName("erlang_version")]
    public string ErlangVersion { get; init; }

    /// <summary>
    /// Gets the complete version details of the Erlang runtime used by the broker.
    /// </summary>
    [JsonPropertyName("erlang_full_version")]
    public string ErlangFullVersion { get; init; }

    /// <summary>
    /// Indicates whether statistics collection and reporting are disabled for the RabbitMQ broker.
    /// </summary>
    [JsonPropertyName("disable_stats")]
    public bool DisableStats { get; init; }

    /// <summary>
    /// Indicates whether the broker is configured to report aggregate statistics for all queues.
    /// </summary>
    [JsonPropertyName("enable_queue_totals")]
    public bool EnableQueueTotals { get; init; }

    /// <summary>
    /// Provides statistical data and metrics related to messages processed by the broker.
    /// </summary>
    [JsonPropertyName("message_stats")]
    public MessageStats MessageStats { get; init; }

    /// <summary>
    /// Represents the churn rates related to the broker, capturing statistical metrics on connection and channel churn over time.
    /// </summary>
    [JsonPropertyName("churn_rates")]
    public ChurnRates ChurnRates { get; init; }

    /// <summary>
    /// Represents the statistical details and metrics related to message queues on the broker.
    /// </summary>
    [JsonPropertyName("queue_totals")]
    public QueueStats QueueStats { get; init; }

    /// <summary>
    /// Gets the total counts of various object types within the cluster.
    /// </summary>
    [JsonPropertyName("object_totals")]
    public ClusterObjectTotals ObjectTotals { get; init; }

    /// <summary>
    /// Represents the number of events currently in the statistics database event queue on the broker.
    /// </summary>
    [JsonPropertyName("statistics_db_event_queue")]
    public ulong StatsDatabaseEventQueue { get; init; }

    /// <summary>
    /// Gets the name of the RabbitMQ node where the broker is running.
    /// </summary>
    [JsonPropertyName("node")]
    public string Node { get; init; }

    /// <summary>
    /// Gets the list of listeners configured for the broker, providing details about the
    /// network interfaces the broker is bound to and listening on.
    /// </summary>
    [JsonPropertyName("listeners")]
    public IList<Listener> Listeners { get; init; }

    /// <summary>
    /// Gets the collection of node contexts associated with the broker.
    /// </summary>
    [JsonPropertyName("contexts")]
    public IList<NodeContext> Contexts { get; init; }
}