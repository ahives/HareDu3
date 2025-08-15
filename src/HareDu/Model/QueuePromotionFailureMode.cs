namespace HareDu.Model;

/// <summary>
/// Represents the modes for handling queue promotion failures.
/// </summary>
public enum QueuePromotionFailureMode
{
    /// <summary>
    /// Specifies that the promotion of the queue will always occur, regardless of its current state or conditions.
    /// </summary>
    Always,

    /// <summary>
    /// Specifies that the promotion of the queue will occur only when it is synchronized.
    /// </summary>
    WhenSynced
}