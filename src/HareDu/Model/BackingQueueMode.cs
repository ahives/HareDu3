namespace HareDu.Model;

/// <summary>
/// Describes the mode of operation for a queue's backing mechanism.
/// </summary>
public enum BackingQueueMode
{
    /// <summary>
    /// Represents the default mode for the backing queue, where the queue manages its messages
    /// in memory and disk based on RabbitMQ's default behavior.
    /// </summary>
    Default,

    /// <summary>
    /// Represents the mode where the backing queue minimizes memory usage
    /// by lazily loading messages only when required, prioritizing disk storage.
    /// </summary>
    Lazy
}