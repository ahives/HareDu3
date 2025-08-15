namespace HareDu.Model;

/// <summary>
/// Represents the possible statuses of a node, indicating whether it is operational or experiencing failures.
/// </summary>
public enum NodeStatus
{
    /// <summary>
    /// Represents a state indicating that the node is operational and functioning correctly without any detected issues.
    /// </summary>
    Ok,

    /// <summary>
    /// Represents a state indicating that the node has encountered a failure and is not functioning as expected.
    /// </summary>
    Failed
}