namespace HareDu;

/// <summary>
/// Represents the state of node quorum within a RabbitMQ cluster.
/// </summary>
public enum NodeQuorumState
{
    /// <summary>
    /// Indicates that the current state of the node quorum in the RabbitMQ cluster has met the minimum required quorum for operations.
    /// </summary>
    MinimumQuorum,

    /// <summary>
    /// Indicates that the current state of the node quorum in the RabbitMQ cluster has fallen below the minimum required
    /// quorum for operations, potentially leading to degraded functionality or cluster issues.
    /// </summary>
    BelowMinimumQuorum,

    /// <summary>
    /// Represents a state where the node quorum status in the RabbitMQ cluster could not be identified or matched to any expected state.
    /// </summary>
    NotRecognized
}