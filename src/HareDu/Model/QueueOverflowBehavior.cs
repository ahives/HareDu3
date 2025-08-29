namespace HareDu.Model;

/// <summary>
/// Specifies the behavior of a queue when it exceeds its maximum capacity.
/// </summary>
public enum QueueOverflowBehavior
{
    /// <summary>
    /// Represents a queue overflow behavior where the oldest message (head of the queue) is discarded
    /// when the queue exceeds its maximum capacity, allowing new messages to be enqueued.
    /// </summary>
    DropHead,

    /// <summary>
    /// Represents a queue overflow behavior where new messages are rejected
    /// when the queue exceeds its maximum capacity, preventing further publishing
    /// until space becomes available.
    /// </summary>
    RejectPublish,

    /// <summary>
    /// Represents a queue overflow behavior where messages that exceed the queue's capacity are rejected,
    /// and a dead-letter exchange is employed to route the rejected messages.
    /// </summary>
    RejectPublishDeadLetter
}