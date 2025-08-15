namespace HareDu.Model;

/// <summary>
/// Defines the behavior for queue promotion to master during a shutdown operation.
/// </summary>
public enum QueuePromotionShutdownMode
{
    /// <summary>
    /// Represents a mode where the queue will always be promoted to master during a shutdown operation,
    /// regardless of its synchronization state.
    /// </summary>
    Always,

    /// <summary>
    /// Represents a mode where the queue will only be promoted to master during a shutdown operation
    /// if it is in a synchronized state.
    /// </summary>
    WhenSynced
}