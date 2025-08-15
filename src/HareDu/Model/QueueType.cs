namespace HareDu.Model;

/// <summary>
/// Defines the types of queues available within the messaging system.
/// </summary>
/// <remarks>
/// QueueType enumerates the distinct queue implementations that can be utilized, each offering specific features and use cases:
/// - Classic: Suitable for traditional messaging patterns with support for durability and delivery guarantees.
/// - Quorum: Provides quorum-based reliability for scenarios requiring higher consistency and fault tolerance.
/// - Stream: Optimized for high-throughput message streaming and log-based use cases.
/// </remarks>
public enum QueueType
{
    /// <summary>
    /// Represents a queue type that follows the traditional implementation of queues, providing standard messaging semantics.
    /// </summary>
    /// <remarks>
    /// The Classic queue type is suitable for use cases requiring a general-purpose message queue with options for durability, persistence,
    /// and various delivery guarantees. It is widely used for reliable and straightforward message processing scenarios.
    /// </remarks>
    Classic,

    /// <summary>
    /// Represents a queue type that is designed to provide strong data consistency using a quorum-based replication mechanism.
    /// </summary>
    /// <remarks>
    /// The Quorum queue type is suitable for use cases requiring high availability and strong consistency by replicating state across multiple nodes.
    /// It is optimized for durability and fault tolerance, making it ideal for use in distributed systems with strict consistency requirements.
    /// </remarks>
    Quorum,

    /// <summary>
    /// Represents a queue type designed for streaming use cases.
    /// </summary>
    /// <remarks>
    /// The Stream queue type is optimized for high-throughput scenarios where messages
    /// are primarily appended and consumed sequentially, making it suitable for log or event-driven systems.
    /// </remarks>
    Stream
}