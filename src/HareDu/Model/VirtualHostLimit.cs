namespace HareDu.Model;

/// <summary>
/// Represents the types of limits that can be configured for a virtual host.
/// </summary>
public enum VirtualHostLimit
{
    /// <summary>
    /// Represents a limit on the maximum number of concurrent client connections allowed for a virtual host.
    /// </summary>
    MaxConnections,

    /// <summary>
    /// Represents a limit on the maximum number of queues that can be created for a virtual host.
    /// </summary>
    MaxQueues
}