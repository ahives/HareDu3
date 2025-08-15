namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents information about a virtual host in a messaging broker.
/// </summary>
public record VirtualHostInfo
{
    /// <summary>
    /// Represents the name of the virtual host, used to uniquely identify it within the broker environment.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// Indicates whether tracing is enabled for the virtual host, typically used for debugging and monitoring
    /// purposes by providing detailed insight into message flow within the system.
    /// </summary>
    [JsonPropertyName("tracing")]
    public bool Tracing { get; init; }

    /// <summary>
    /// Provides a textual description of the virtual host, often used to give additional context or information
    /// about its purpose or characteristics.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; }

    /// <summary>
    /// Represents a collection of tags associated with a virtual host.
    /// These tags are used for categorization or providing additional metadata.
    /// </summary>
    [JsonPropertyName("tags")]
    public List<string> Tags { get; init; }

    /// <summary>
    /// Represents the state of the RabbitMQ cluster for the associated virtual host.
    /// Includes key-value pairs that provide details about the cluster's configuration or status.
    /// </summary>
    [JsonPropertyName("cluster_state")]
    public IDictionary<string, string> ClusterState { get; init; }

    /// <summary>
    /// Provides metadata associated with the virtual host, including attributes such as description and tags.
    /// Represents additional contextual information that characterizes the virtual host.
    /// </summary>
    [JsonPropertyName("metadata")]
    public VirtualHostMetadata Metadata { get; init; }

    /// <summary>
    /// Indicates the default type of queues created within the virtual host.
    /// Represents the selected configuration among the available queue types such as Classic, Quorum, and Stream.
    /// </summary>
    [JsonPropertyName("default_queue_type")]
    public DefaultQueueType DefaultQueueType { get; init; }

    /// <summary>
    /// Provides rate details for the total number of messages in a virtual host.
    /// </summary>
    [JsonPropertyName("messages_details")]
    public Rate MessagesDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages in a virtual host, including ready and unacknowledged messages.
    /// </summary>
    [JsonPropertyName("messages")]
    public ulong TotalMessages { get; init; }

    /// <summary>
    /// Provides detailed rate information about messages that have been delivered to consumers but are not yet acknowledged
    /// within the context of a virtual host.
    /// </summary>
    [JsonPropertyName("messages_unacknowledged_details")]
    public Rate UnacknowledgedMessagesDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages that have been delivered to consumers but not yet acknowledged
    /// within the context of a virtual host.
    /// </summary>
    [JsonPropertyName("messages_unacknowledged")]
    public ulong UnacknowledgedMessages { get; init; }

    /// <summary>
    /// Represents the rate details of messages that are ready to be delivered to consumers
    /// within the context of a virtual host.
    /// </summary>
    [JsonPropertyName("messages_ready_details")]
    public Rate ReadyMessagesDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages that are ready to be delivered to consumers
    /// within the context of a virtual host.
    /// </summary>
    [JsonPropertyName("messages_ready")]
    public ulong ReadyMessages { get; init; }
}