namespace HareDu.Model;

/// <summary>
/// Defines the behavior and storage mode of a queue.
/// </summary>
public enum QueueMode
{
    /// <summary>
    /// Represents the default queue mode where messages are stored in memory and on disk as needed.
    /// This mode ensures faster access to messages by utilizing memory for storage while
    /// utilizing disk storage for durability.
    /// </summary>
    Default,

    /// <summary>
    /// Represents the lazy queue mode where messages are predominantly stored on disk rather than in memory.
    /// This mode is intended to optimize memory usage, particularly useful for queues with large backlogs
    /// or infrequent message retrieval.
    /// </summary>
    Lazy
}