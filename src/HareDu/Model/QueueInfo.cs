namespace HareDu.Model;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents detailed information about a queue in the message broker system.
/// Provides data regarding message counts, message rates, memory usage, consumer details, and more.
/// </summary>
public record QueueInfo
{
    /// <summary>
    /// Provides detailed statistical information regarding the rate of message activities within the queue.
    /// </summary>
    /// <remarks>
    /// This property encapsulates rate-related metrics, facilitating precise analysis of message behavior and throughput trends over time.
    /// </remarks>
    [JsonPropertyName("messages_details")]
    public Rate MessageDetails { get; init; }

    /// <summary>
    /// Represents the total count of messages currently present in the queue.
    /// </summary>
    /// <remarks>
    /// This property provides the aggregate number of messages in the queue, including ready and unacknowledged messages,
    /// assisting in monitoring overall queue size and message load.
    /// </remarks>
    [JsonPropertyName("messages")]
    public ulong TotalMessages { get; init; }

    /// <summary>
    /// Provides detailed rate-related information for messages that have been delivered to consumers but are not yet acknowledged.
    /// </summary>
    /// <remarks>
    /// This property supplies additional metrics related to unacknowledged messages, aiding in monitoring and analyzing
    /// the rate of change or trends in acknowledgment activity within the queue.
    /// </remarks>
    [JsonPropertyName("messages_unacknowledged_details")]
    public Rate UnacknowledgedMessageDetails { get; init; }

    /// <summary>
    /// Represents the count of messages that have been delivered to consumers but have not yet been acknowledged.
    /// </summary>
    /// <remarks>
    /// This property provides insight into the current state of message acknowledgment within the queue,
    /// allowing monitoring of pending acknowledgment and assisting in identifying delays or issues in consumer processing.
    /// </remarks>
    [JsonPropertyName("messages_unacknowledged")]
    public ulong UnacknowledgedMessages { get; init; }

    /// <summary>
    /// Provides detailed rate information about the messages that are ready for delivery to consumers in the queue.
    /// </summary>
    /// <remarks>
    /// This property tracks metrics related to ready messages, offering visibility into their delivery rate,
    /// which can help in monitoring queue performance and diagnosing potential bottlenecks or imbalances in load handling.
    /// </remarks>
    [JsonPropertyName("messages_ready_details")]
    public Rate ReadyMessageDetails { get; init; }

    /// <summary>
    /// Represents the number of messages in the queue that are ready to be delivered to consumers.
    /// </summary>
    /// <remarks>
    /// This property provides insight into the current state of the queue by indicating how many messages
    /// are awaiting delivery. It can assist in monitoring queue activity and ensuring timely processing of messages.
    /// </remarks>
    [JsonPropertyName("messages_ready")]
    public ulong ReadyMessages { get; init; }

    /// <summary>
    /// Provides detailed metrics regarding the rate of reductions for the queue.
    /// </summary>
    /// <remarks>
    /// This property conveys information about the rate at which reductions, defined as computation steps or function executions,
    /// are being processed within the context of the queue. It helps in monitoring performance trends
    /// and assessing workload efficiency over time.
    /// </remarks>
    [JsonPropertyName("reductions_details")]
    public Rate ReductionDetails { get; init; }

    /// <summary>
    /// Represents the total number of reductions performed for the queue.
    /// </summary>
    /// <remarks>
    /// This property indicates the cumulative count of reductions, which are effectively function calls or processing steps,
    /// performed within the Erlang runtime system for the queue. It provides a metric for understanding the workload
    /// and processing efficiency of the queue in the system.
    /// </remarks>
    [JsonPropertyName("reductions")]
    public long TotalReductions { get; init; }

    /// <summary>
    /// Represents additional arguments or custom options configured for the queue.
    /// </summary>
    /// <remarks>
    /// This property holds a dictionary of key-value pairs where the key is a string and the value represents
    /// the corresponding argument or configuration setting. These arguments are often used to set advanced
    /// queue parameters, such as message TTL, dead-letter exchange, or other custom behaviors.
    /// </remarks>
    [JsonPropertyName("arguments")]
    public IDictionary<string, object> Arguments { get; init; }

    /// <summary>
    /// Indicates whether the queue is exclusive to the connection that declared it.
    /// </summary>
    /// <remarks>
    /// An exclusive queue is restricted for use by the connection that declared it and will be deleted
    /// if the connection is closed. This property is typically used when a private, temporary queue
    /// is needed for specific connection-based operations.
    /// </remarks>
    [JsonPropertyName("exclusive")]
    public bool Exclusive { get; init; }

    /// <summary>
    /// Specifies whether the queue is automatically deleted when the last consumer unsubscribes.
    /// </summary>
    /// <remarks>
    /// Auto-delete queues are automatically removed once they are no longer in use,
    /// typically when the last consumer unsubscribes from the queue. This property is useful
    /// in scenarios where temporary or disposable queues are needed, reducing the need
    /// for manual cleanup of unused queues.
    /// </remarks>
    [JsonPropertyName("auto_delete")]
    public bool AutoDelete { get; init; }

    /// <summary>
    /// Indicates whether the queue is durable.
    /// </summary>
    /// <remarks>
    /// Durable queues remain present even after a RabbitMQ broker restart, whereas non-durable queues
    /// are deleted once the broker stops. This property is essential for ensuring message durability
    /// and system resilience in scenarios where long-term persistence of the queue is required.
    /// </remarks>
    [JsonPropertyName("durable")]
    public bool Durable { get; init; }

    /// <summary>
    /// Represents the virtual host associated with the queue.
    /// </summary>
    /// <remarks>
    /// This property indicates the name of the virtual host where the queue resides. Virtual hosts
    /// are used to provide logical grouping and segregation of resources such as exchanges,
    /// queues, and bindings in the RabbitMQ system. Proper use of virtual hosts helps to
    /// manage multiple environments or tenants within the messaging infrastructure.
    /// </remarks>
    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    /// <summary>
    /// Represents the name of the queue.
    /// </summary>
    /// <remarks>
    /// This property specifies the unique identifier for the queue, which is used to
    /// reference it within the RabbitMQ system. Proper naming conventions can help
    /// with management and tracking of different queues in the messaging infrastructure.
    /// </remarks>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// Identifies the server node on which the queue resides.
    /// </summary>
    /// <remarks>
    /// This property indicates the specific RabbitMQ node responsible for hosting
    /// the queue. It is useful for understanding the distribution of queues across nodes
    /// in a clustered environment.
    /// </remarks>
    [JsonPropertyName("node")]
    public string Node { get; init; }

    /// <summary>
    /// Represents the total size, in bytes, of messages that have been moved from memory to disk.
    /// </summary>
    /// <remarks>
    /// This property provides details regarding the volume of message data paged out
    /// to disk as part of system resource management and optimization processes.
    /// </remarks>
    [JsonPropertyName("message_bytes_paged_out")]
    public ulong MessageBytesPagedOut { get; init; }

    /// <summary>
    /// Represents the total count of messages that have been moved from memory to disk.
    /// </summary>
    /// <remarks>
    /// This property provides insight into the number of messages that have been paged out
    /// by the system, typically as part of memory management or resource optimization efforts.
    /// </remarks>
    [JsonPropertyName("messages_paged_out")]
    public ulong TotalMessagesPagedOut { get; init; }

    /// <summary>
    /// Represents the status of the backing queue associated with the system.
    /// </summary>
    /// <remarks>
    /// This property provides detailed information regarding the internal state and characteristics of the queue's backing mechanism,
    /// which may include data on storage and operational behavior.
    /// </remarks>
    [JsonPropertyName("backing_queue_status")]
    public BackingQueueStatus BackingQueueStatus { get; init; }

    /// <summary>
    /// Indicates the timestamp of the message currently at the head of the queue.
    /// </summary>
    /// <remarks>
    /// This property provides information on when the oldest message in the queue was enqueued, allowing users to assess message age and timing within the system.
    /// </remarks>
    [JsonPropertyName("head_message_timestamp")]
    public DateTimeOffset HeadMessageTimestamp { get; init; }

    /// <summary>
    /// Represents the total size, in bytes, of message bodies that are persisted to disk for the queue.
    /// </summary>
    /// <remarks>
    /// This property provides information about the storage usage associated with messages that have been written to persistent storage, aiding in tracking disk-based resource utilization.
    /// </remarks>
    [JsonPropertyName("message_bytes_persistent")]
    public ulong MessageBytesPersisted { get; init; }

    /// <summary>
    /// Represents the total size, in bytes, of message bodies currently residing in RAM for the queue.
    /// </summary>
    /// <remarks>
    /// This property provides an indication of the memory usage specific to the message bodies stored in RAM, helping to assess in-memory resource utilization.
    /// </remarks>
    [JsonPropertyName("message_bytes_ram")]
    public ulong MessageBytesInRAM { get; init; }

    /// <summary>
    /// Represents the total size, in bytes, of messages that have been delivered but have not yet been acknowledged by consumers.
    /// </summary>
    /// <remarks>
    /// This property reflects the cumulative size of messages awaiting acknowledgment, providing visibility into delivery status and aiding in monitoring unacknowledged message volumes.
    /// </remarks>
    [JsonPropertyName("message_bytes_unacknowledged")]
    public ulong TotalBytesOfMessagesDeliveredButUnacknowledged { get; init; }

    /// <summary>
    /// Represents the total size, in bytes, of messages ready to be delivered from the queue.
    /// </summary>
    /// <remarks>
    /// This property provides the collective size of all messages that are prepared and waiting for delivery,
    /// enabling insights into current message readiness and aiding in delivery management.
    /// </remarks>
    [JsonPropertyName("message_bytes_ready")]
    public ulong TotalBytesOfMessagesReadyForDelivery { get; init; }

    /// <summary>
    /// Represents the total size, in bytes, of all messages currently in the queue.
    /// </summary>
    /// <remarks>
    /// This property indicates the accumulated size of all messages stored in the queue,
    /// allowing for monitoring of storage consumption and aiding in capacity planning.
    /// </remarks>
    [JsonPropertyName("message_bytes")]
    public ulong TotalBytesOfAllMessages { get; init; }

    /// <summary>
    /// Represents the number of messages currently persisted to disk for the queue.
    /// </summary>
    /// <remarks>
    /// This property provides the count of messages stored on disk, which helps monitor disk usage
    /// and assess the durability of the message storage for the queue.
    /// </remarks>
    [JsonPropertyName("messages_persistent")]
    public ulong MessagesPersisted { get; init; }

    /// <summary>
    /// Represents the number of unacknowledged messages currently stored in RAM for the queue.
    /// </summary>
    /// <remarks>
    /// This property indicates the count of messages in memory that have been delivered but not yet acknowledged,
    /// allowing for monitoring of memory usage and acknowledgment flow in the system.
    /// </remarks>
    [JsonPropertyName("messages_unacknowledged_ram")]
    public ulong UnacknowledgedMessagesInRAM { get; init; }

    /// <summary>
    /// Represents the number of messages ready for delivery that are currently stored in RAM for the queue.
    /// </summary>
    /// <remarks>
    /// This property indicates the count of messages available for delivery and held in memory,
    /// which helps monitor memory-specific message handling and assess the queue's delivery readiness.
    /// </remarks>
    [JsonPropertyName("messages_ready_ram")]
    public ulong MessagesReadyForDeliveryInRAM { get; init; }

    /// <summary>
    /// Represents the number of messages currently stored in RAM for the queue.
    /// </summary>
    /// <remarks>
    /// This property indicates the count of messages residing in memory, providing insight into the in-memory
    /// utilization of the queue, which can help in assessing performance and resource management.
    /// </remarks>
    [JsonPropertyName("messages_ram")]
    public ulong MessagesInRAM { get; init; }

    /// <summary>
    /// Provides details about the queue's garbage collection properties.
    /// </summary>
    /// <remarks>
    /// This property contains information regarding the garbage collection behavior
    /// and performance of the queue, useful for analyzing and optimizing resources.
    /// </remarks>
    [JsonPropertyName("garbage_collection")]
    public GarbageCollectionDetails GC { get; init; }

    /// <summary>
    /// Gets the current state of the queue.
    /// </summary>
    /// <remarks>
    /// This property indicates the operational status of the queue.
    /// Possible states include "Running" and "Idle", which represent the activity level of the queue.
    /// </remarks>
    [JsonPropertyName("state")]
    public QueueState State { get; init; }

    /// <summary>
    /// Gets the type of the queue.
    /// </summary>
    /// <remarks>
    /// This property specifies the queue's operational mode, defining its behavior and functionality.
    /// Possible types include "Classic", "Quorum", and "Stream".
    /// </remarks>
    [JsonPropertyName("type")]
    public QueueType Type { get; init; }

    /// <summary>
    /// Gets the list of recoverable slave nodes associated with the queue.
    /// </summary>
    /// <remarks>
    /// This property contains the names of slave nodes that can be recovered in the event
    /// of a failure. These nodes help ensure the durability and availability of the queue's data.
    /// </remarks>
    [JsonPropertyName("recoverable_slaves")]
    public IList<string> RecoverableSlaves { get; init; }

    /// <summary>
    /// Gets the total number of consumers currently subscribed to the queue.
    /// </summary>
    /// <remarks>
    /// This property represents the count of consumers actively consuming messages
    /// from the queue. Consumers subscribe to the queue to retrieve messages for processing.
    /// </remarks>
    [JsonPropertyName("consumers")]
    public ulong Consumers { get; init; }

    /// <summary>
    /// Gets the tag of the exclusive consumer associated with the queue.
    /// </summary>
    /// <remarks>
    /// This property reflects the consumer tag of the exclusive consumer, if present,
    /// which indicates that the queue is currently restricted to a single consumer.
    /// Exclusive consumers prevent other consumers from accessing the queue.
    /// </remarks>
    [JsonPropertyName("exclusive_consumer_tag")]
    public string ExclusiveConsumerTag { get; init; }

    /// <summary>
    /// Gets the name of the operator policy applied to the queue.
    /// </summary>
    /// <remarks>
    /// This property identifies the operator policy, if any, that is currently associated with the queue.
    /// Operator policies provide additional control over queue behavior, often used for administrative
    /// or operational constraints on queues throughout a system.
    /// </remarks>
    [JsonPropertyName("operator_policy")]
    public string OperatorPolicy { get; init; }

    /// <summary>
    /// Gets the name of the policy applied to the queue.
    /// </summary>
    /// <remarks>
    /// This property indicates which policy, if any, is currently associated with the queue.
    /// Policies allow for the automatic configuration of queues, enabling features like message expiration,
    /// dead lettering, and more, based on the policy definition.
    /// </remarks>
    [JsonPropertyName("policy")]
    public string Policy { get; init; }

    /// <summary>
    /// Gets the level of utilization of the queue's consumers.
    /// </summary>
    /// <remarks>
    /// This property represents the proportion of time that the queue's consumers are busy processing messages.
    /// A value closer to 1 indicates higher utilization, while a value closer to 0 indicates lower activity.
    /// </remarks>
    [JsonPropertyName("consumer_utilisation")]
    public decimal ConsumerUtilization { get; init; }

    /// <summary>
    /// Gets the timestamp indicating when the queue was last idle.
    /// </summary>
    /// <remarks>
    /// This property represents the time when the queue was last considered idle, providing insight into its activity status.
    /// An idle queue is one that has not been consuming or producing messages since the specified time.
    /// </remarks>
    [JsonPropertyName("idle_since")]
    public DateTimeOffset IdleSince { get; init; }

    /// <summary>
    /// Gets the memory usage associated with the queue.
    /// </summary>
    /// <remarks>
    /// This property provides information about the amount of memory being utilized by the queue,
    /// which can be useful for monitoring resource consumption and performance analysis.
    /// </remarks>
    [JsonPropertyName("memory")]
    public ulong Memory { get; init; }

    /// <summary>
    /// Represents statistics related to messages in the queue.
    /// </summary>
    /// <remarks>
    /// This property provides detailed metrics about message activity in the queue, such as counts of messages published, delivered,
    /// acknowledged, or other relevant statistics, allowing for monitoring and analysis of the queue's message throughput.
    /// </remarks>
    [JsonPropertyName("message_stats")]
    public QueueMessageStats MessageStats { get; init; }
}