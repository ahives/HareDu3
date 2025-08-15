namespace HareDu.Model;

/// <summary>
/// Represents the operational state of a channel.
/// </summary>
public enum ChannelState
{
    /// <summary>
    /// Represents the state of a channel that is currently active and processing operations.
    /// </summary>
    Running,

    /// <summary>
    /// Represents the state of a channel that is inactive and not currently processing operations.
    /// </summary>
    Idle
}