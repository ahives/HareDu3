namespace HareDu.Model;

/// <summary>
/// Specifies the default queue type settings for a RabbitMQ virtual host.
/// </summary>
public enum DefaultQueueType
{
    /// <summary>
    /// Represents a classic queue type in RabbitMQ.
    /// Classic queues store messages using traditional methods, with messages retained
    /// based on queue policies such as time-to-live (TTL) or capacity limits.
    /// </summary>
    Classic,

    /// <summary>
    /// Represents a quorum queue type in RabbitMQ. Quorum queues are designed for high availability and data
    /// replication, using the Raft consensus algorithm to ensure reliability and consistency across multiple nodes.
    /// </summary>
    Quorum,

    /// <summary>
    /// Represents a stream queue type in RabbitMQ.
    /// Stream queues are designed for high-throughput and persistent message streaming use cases,
    /// allowing sequential access and efficient retention of message history.
    /// </summary>
    Stream,

    /// <summary>
    /// Represents an undefined queue type. This value is used when the queue type is not explicitly specified or
    /// could not be determined.
    /// </summary>
    Undefined
}