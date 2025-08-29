namespace HareDu.Model;

/// <summary>
/// Represents the strategies for routing messages to a dead-letter queue during the dead-lettering process.
/// </summary>
public enum DeadLetterQueueStrategy
{
    /// <summary>
    /// Represents a dead-letter queue strategy where a message is routed to the dead-letter exchange at most once
    /// during the dead-lettering process. This ensures that a message is not requeued or reprocessed multiple times,
    /// reducing potential redundancy in the dead-letter queue.
    /// </summary>
    AtMostOnce,

    /// <summary>
    /// Represents a dead-letter queue strategy where a message may be routed to the dead-letter exchange multiple times
    /// during the dead-lettering process. This approach ensures that messages are requeued or reprocessed
    /// until successfully handled, prioritizing reliability over potential duplication.
    /// </summary>
    AtLeastOnce
}