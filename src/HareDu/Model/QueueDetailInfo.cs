namespace HareDu.Model;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents detailed information about a RabbitMQ queue, including its configuration, state, statistics,
/// and resource utilization.
/// </summary>
public record QueueDetailInfo
{
    /// <summary>
    /// Represents a collection of optional key-value pairs configured for the queue.
    /// </summary>
    /// <remarks>
    /// These arguments allow for the specification of additional queue configuration options such as TTL,
    /// dead-letter exchange, maximum message size, and others, enabling fine-grained control over queue behavior.
    /// </remarks>
    [JsonPropertyName("arguments")]
    public IDictionary<string, object> Arguments { get; init; }

    /// <summary>
    /// Indicates whether the queue is automatically deleted when no longer in use.
    /// </summary>
    /// <remarks>
    /// This property determines if the queue will be automatically removed when it is no longer bound to any exchanges
    /// and has no active consumers, helping to manage resource cleanup effectively in the messaging system.
    /// </remarks>
    [JsonPropertyName("auto_delete")]
    public bool AutoDelete { get; init; }

    /// <summary>
    /// Indicates the maximum number of messages that a consumer is capable of handling at a time for the queue.
    /// </summary>
    /// <remarks>
    /// This property provides an insight into the capacity of a consumer tied to the queue, helping determine
    /// the efficiency and limitations in message processing at the consumer level.
    /// </remarks>
    [JsonPropertyName("consumer_capacity")]
    public int ConsumerCapacity { get; init; }

    /// <summary>
    /// Represents the version of the storage mechanism utilized by the queue.
    /// </summary>
    /// <remarks>
    /// This property indicates the specific version of the storage format or structure,
    /// potentially associated with queue data representation or persistence.
    /// </remarks>
    [JsonPropertyName("storage_version")]
    public int StorageVersion { get; init; }

    /// <summary>
    /// Provides detailed rate information related to messages in the queue.
    /// </summary>
    /// <remarks>
    /// This property holds metrics or statistical data that describe the rate at which messages are being processed,
    /// offering insight for monitoring and analysis of queue performance.
    /// </remarks>
    [JsonPropertyName("messages_details")]
    public Rate MessageDetails { get; init; }

    /// <summary>
    /// Represents the total number of messages currently present in the queue.
    /// </summary>
    /// <remarks>
    /// This property provides the current count of all messages in the queue, including both ready and unacknowledged messages.
    /// </remarks>
    [JsonPropertyName("messages")]
    public ulong TotalMessages { get; init; }

    /// <summary>
    /// Represents metrics related to the rate of unacknowledged messages in the queue.
    /// </summary>
    /// <remarks>
    /// This property provides visibility into the flow of messages that remain unacknowledged by consumers,
    /// aiding in monitoring and diagnosing potential processing inefficiencies or issues in the message acknowledgment process.
    /// </remarks>
    [JsonPropertyName("messages_unacknowledged_details")]
    public Rate UnacknowledgedMessageDetails { get; init; }

    /// <summary>
    /// Represents the number of messages in the queue that have been delivered to consumers
    /// but remain unacknowledged.
    /// </summary>
    /// <remarks>
    /// This property is useful for understanding the current state of message processing in the queue,
    /// helping identify delays or issues in consumer acknowledgment behavior.
    /// </remarks>
    [JsonPropertyName("messages_unacknowledged")]
    public ulong UnacknowledgedMessages { get; init; }

    /// <summary>
    /// Represents metrics related to the rate of ready messages in the queue.
    /// </summary>
    /// <remarks>
    /// This property provides insights into the flow of messages that are ready for delivery to consumers
    /// but have not yet been delivered, assisting in monitoring and analyzing queue performance.
    /// </remarks>
    [JsonPropertyName("messages_ready_details")]
    public Rate ReadyMessageDetails { get; init; }

    /// <summary>
    /// Represents the number of messages that are ready for delivery but have not yet been acknowledged in the queue.
    /// </summary>
    /// <remarks>
    /// Indicates the current volume of messages waiting to be consumed, assisting in monitoring queue utilization.
    /// </remarks>
    [JsonPropertyName("messages_ready")]
    public ulong ReadyMessages { get; init; }

    /// <summary>
    /// Represents the rate statistics associated with reduction operations in the context of a specific queue.
    /// </summary>
    /// <remarks>
    /// Provides detailed insights into the performance and computational load associated with the queue's reduction processes.
    /// </remarks>
    [JsonPropertyName("reductions_details")]
    public Rate ReductionDetails { get; init; }

    /// <summary>
    /// Indicates the cumulative number of reduction operations performed within the context of the queue.
    /// </summary>
    /// <remarks>
    /// Reductions are a metric of workload measurement in the Erlang runtime, representing the amount of computational tasks executed.
    /// </remarks>
    [JsonPropertyName("reductions")]
    public long TotalReductions { get; init; }

    /// <summary>
    /// Indicates whether the queue is exclusive to the connection that declared it.
    /// </summary>
    /// <remarks>
    /// An exclusive queue can only be used by the connection that originally created it.
    /// This property helps determine if the queue is designed for a single-connection use case.
    /// </remarks>
    [JsonPropertyName("exclusive")]
    public bool Exclusive { get; init; }

    /// <summary>
    /// Indicates whether the queue is durable.
    /// </summary>
    /// <remarks>
    /// A durable queue will persist even when the message broker restarts, ensuring the queue's data is retained across server restarts or crashes.
    /// </remarks>
    [JsonPropertyName("durable")]
    public bool Durable { get; init; }

    /// <summary>
    /// Specifies the name of the virtual host associated with the queue.
    /// </summary>
    /// <remarks>
    /// A virtual host is a logical grouping of resources such as queues and exchanges within a message broker. It provides isolation and acts as a namespace for different applications or tenants.
    /// </remarks>
    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    /// <summary>
    /// Specifies the name associated with the queue.
    /// </summary>
    /// <remarks>
    /// This property denotes the unique identifier for the queue, utilized in distinguishing it from other queues.
    /// </remarks>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// Represents the node where the queue resides.
    /// </summary>
    /// <remarks>
    /// This property identifies the specific cluster node associated with the queue, serving as an indicator of its location within the system.
    /// </remarks>
    [JsonPropertyName("node")]
    public string Node { get; init; }

    /// <summary>
    /// Represents the total number of bytes of messages that have been paged out to disk from the queue.
    /// </summary>
    /// <remarks>
    /// This property indicates the volume of message data that was moved to disk storage when memory usage exceeded configured thresholds.
    /// </remarks>
    [JsonPropertyName("message_bytes_paged_out")]
    public ulong MessageBytesPagedOut { get; init; }

    /// <summary>
    /// Indicates the total number of messages that have been moved to disk from the queue.
    /// </summary>
    /// <remarks>
    /// This property provides the count of messages temporarily paged out of memory and stored on disk to optimize memory usage in the queue system.
    /// </remarks>
    [JsonPropertyName("messages_paged_out")]
    public ulong TotalMessagesPagedOut { get; init; }

    /// <summary>
    /// Represents the operational status and metadata of the underlying backing queue.
    /// </summary>
    /// <remarks>
    /// This property provides detailed information pertaining to the configuration and runtime state
    /// of the backing queue system that underpins message storage and delivery mechanisms.
    /// </remarks>
    [JsonPropertyName("backing_queue_status")]
    public BackingQueueStatus BackingQueueStatus { get; init; }

    /// <summary>
    /// Represents the timestamp of the oldest message currently present in the queue.
    /// </summary>
    /// <remarks>
    /// This property provides insight into the age of the oldest message in the queue, aiding in queue monitoring and performance assessments.
    /// </remarks>
    [JsonPropertyName("head_message_timestamp")]
    public DateTimeOffset HeadMessageTimestamp { get; init; }

    /// <summary>
    /// Represents the total size, in bytes, of persistent messages stored in the queue.
    /// </summary>
    /// <remarks>
    /// This property is used to determine the amount of storage being utilized by messages marked as persistent,
    /// providing insights into the queue's resource consumption.
    /// </remarks>
    [JsonPropertyName("message_bytes_persistent")]
    public ulong MessageBytesPersisted { get; init; }

    /// <summary>
    /// Represents the total size, in bytes, of message data currently held in RAM for the queue.
    /// </summary>
    /// <remarks>
    /// This property helps assess the memory consumption of the queue by tracking the size of message data stored in RAM.
    /// It can be useful for monitoring system performance and optimizing resource allocation.
    /// </remarks>
    [JsonPropertyName("message_bytes_ram")]
    public ulong MessageBytesInRAM { get; init; }

    /// <summary>
    /// Represents the total size, in bytes, of messages that have been delivered to consumers but have not yet been acknowledged.
    /// </summary>
    /// <remarks>
    /// This property provides insights into the amount of unacknowledged message data, which can help monitor and manage message flow
    /// and potential bottlenecks in consumer processing.
    /// </remarks>
    [JsonPropertyName("message_bytes_unacknowledged")]
    public ulong TotalBytesOfMessagesDeliveredButUnacknowledged { get; init; }

    /// <summary>
    /// Represents the total size, in bytes, of messages that are ready to be delivered to consumers but have not yet been sent out.
    /// </summary>
    /// <remarks>
    /// This property provides information about the amount of message data currently queued for delivery, aiding in monitoring queue backlogs and delivery readiness.
    /// </remarks>
    [JsonPropertyName("message_bytes_ready")]
    public ulong TotalBytesOfMessagesReadyForDelivery { get; init; }

    /// <summary>
    /// Represents the total size, in bytes, of all messages in the queue, including those ready for delivery and those awaiting acknowledgment.
    /// </summary>
    /// <remarks>
    /// This property provides a comprehensive measure of the total message data within the queue, which can be useful for monitoring
    /// system resource usage and ensuring efficient queue management.
    /// </remarks>
    [JsonPropertyName("message_bytes")]
    public ulong TotalBytesOfAllMessages { get; init; }

    /// <summary>
    /// Indicates the total number of messages that are stored persistently on disk within the queue.
    /// </summary>
    /// <remarks>
    /// This property provides a measure of the queue's usage of persistent storage, which ensures
    /// message durability in case of server restarts or failures.
    /// </remarks>
    [JsonPropertyName("messages_persistent")]
    public ulong MessagesPersisted { get; init; }

    /// <summary>
    /// Represents the total number of unacknowledged messages currently residing in memory (RAM) within the queue.
    /// </summary>
    /// <remarks>
    /// This property reflects the number of messages that have been delivered to consumers but not yet acknowledged,
    /// and remain stored in RAM, offering insight into memory usage and message flow in the queue.
    /// </remarks>
    [JsonPropertyName("messages_unacknowledged_ram")]
    public ulong UnacknowledgedMessagesInRAM { get; init; }

    /// <summary>
    /// Represents the number of messages in the queue that are ready for immediate delivery and stored in RAM.
    /// </summary>
    /// <remarks>
    /// This property provides an indication of the message load currently held in memory,
    /// which can impact system performance and resource utilization.
    /// </remarks>
    [JsonPropertyName("messages_ready_ram")]
    public ulong MessagesReadyForDeliveryInRAM { get; init; }

    /// <summary>
    /// Represents the number of messages currently stored in RAM for the queue.
    /// </summary>
    /// <remarks>
    /// This property indicates the total count of messages held in memory for the queue,
    /// which can provide insights into memory usage and performance of the broker.
    /// </remarks>
    [JsonPropertyName("messages_ram")]
    public ulong MessagesInRAM { get; init; }

    /// <summary>
    /// Represents details about the garbage collection process for the queue.
    /// </summary>
    /// <remarks>
    /// This property provides detailed information about the garbage collection processes applied to the queue,
    /// which may offer insights into resource management and performance efficiency.
    /// </remarks>
    [JsonPropertyName("garbage_collection")]
    public GarbageCollectionDetails GC { get; init; }

    /// <summary>
    /// Represents the current operational state of the queue.
    /// </summary>
    /// <remarks>
    /// The state indicates whether the queue is actively processing messages or is idle.
    /// Possible values include states such as "Running" or "Idle".
    /// </remarks>
    [JsonPropertyName("state")]
    public QueueState State { get; init; }

    /// <summary>
    /// Specifies the type of the queue.
    /// </summary>
    /// <remarks>
    /// This property indicates the queue's type, such as Classic, Quorum, or Stream,
    /// which defines its behavior and characteristics within the broker.
    /// </remarks>
    [JsonPropertyName("type")]
    public QueueType Type { get; init; }

    /// <summary>
    /// Represents a list of recoverable slave nodes associated with the queue.
    /// </summary>
    /// <remarks>
    /// This property identifies the slave nodes that can be recovered to ensure data replication and availability
    /// in a distributed messaging environment.
    /// </remarks>
    [JsonPropertyName("recoverable_slaves")]
    public IList<string> RecoverableSlaves { get; init; }

    /// <summary>
    /// Represents the number of active consumers for the queue.
    /// </summary>
    /// <remarks>
    /// This property indicates the total count of consumers currently subscribed to the queue,
    /// actively consuming messages from it.
    /// </remarks>
    [JsonPropertyName("consumers")]
    public ulong Consumers { get; init; }

    /// <summary>
    /// Represents the tag identifying the exclusive consumer of the queue.
    /// </summary>
    /// <remarks>
    /// This property specifies the unique identifier of the consumer that has exclusive access to the queue,
    /// preventing other consumers from receiving messages from it during the exclusivity period.
    /// </remarks>
    [JsonPropertyName("exclusive_consumer_tag")]
    public string ExclusiveConsumerTag { get; init; }

    /// <summary>
    /// Represents the operator policy applied to the queue.
    /// </summary>
    /// <remarks>
    /// This property defines the operator-specific policy associated with the queue, which may dictate higher-level
    /// governance, override settings, or control parameters for broader system administration within the messaging environment.
    /// </remarks>
    [JsonPropertyName("operator_policy")]
    public string OperatorPolicy { get; init; }

    /// <summary>
    /// Represents the name of the policy applied to the queue.
    /// </summary>
    /// <remarks>
    /// This property specifies the queue's associated policy, which governs operational or configuration settings such as
    /// message expiration, replication, or other queue-specific rules within the messaging infrastructure.
    /// </remarks>
    [JsonPropertyName("policy")]
    public string Policy { get; init; }

    /// <summary>
    /// Represents the proportion of time that the consumers are actively retrieving messages from the queue.
    /// </summary>
    /// <remarks>
    /// This property indicates the utilization rate of the consumers connected to the queue.
    /// A value closer to 1 signifies higher utilization, whereas a value further from 1 indicates underutilization.
    /// </remarks>
    [JsonPropertyName("consumer_utilisation")]
    public decimal ConsumerUtilization { get; init; }

    /// <summary>
    /// Indicates the timestamp when the queue was last utilized.
    /// </summary>
    /// <remarks>
    /// This property records the most recent time the queue transitioned into an idle state, signifying no read or write operations.
    /// </remarks>
    [JsonPropertyName("idle_since")]
    public DateTimeOffset IdleSince { get; init; }

    /// <summary>
    /// Represents the memory consumed by the queue.
    /// </summary>
    /// <remarks>
    /// This property provides the amount of memory, in bytes, utilized by the queue. It includes
    /// memory usage from message storage, metadata, and other internal operations related to the queue.
    /// </remarks>
    [JsonPropertyName("memory")]
    public ulong Memory { get; init; }

    /// <summary>
    /// Represents statistics related to messages in the queue.
    /// </summary>
    /// <remarks>
    /// This property provides detailed metrics and insights about message activity within the queue,
    /// such as the number of messages published, delivered, or acknowledged.
    /// </remarks>
    [JsonPropertyName("message_stats")]
    public QueueMessageStats MessageStats { get; init; }
}