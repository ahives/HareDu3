namespace HareDu;

/// <summary>
/// Defines the high availability modes used in configuring RabbitMQ policies.
/// </summary>
public enum HighAvailabilityModes
{
    /// <summary>
    /// Represents a high availability mode where all nodes in the cluster are involved.
    /// </summary>
    All,

    /// <summary>
    /// Represents a high availability mode where a specific number of nodes in the cluster are involved.
    /// </summary>
    Exactly,

    /// <summary>
    /// Represents a high availability mode where the policy applies to a specified set of nodes in the cluster.
    /// </summary>
    Nodes
}