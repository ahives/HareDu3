namespace HareDu.Model;

/// <summary>
/// Describes the various states that a queue can be in, reflecting its current operational state.
/// </summary>
public enum QueueState
{
    /// <summary>
    /// Represents the state of a queue when it is actively processing messages or performing actions.
    /// </summary>
    Running,

    /// <summary>
    /// Represents the state of a queue when it is not processing any messages or performing any actions.
    /// </summary>
    Idle
}