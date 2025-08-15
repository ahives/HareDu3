namespace HareDu.Model;

/// <summary>
/// Represents the synchronization modes for high availability in a cluster environment.
/// </summary>
public enum HighAvailabilitySyncMode
{
    /// <summary>
    /// Indicates that the synchronization mode for high availability is set to manual.
    /// In this mode, the user must explicitly initiate synchronization of data
    /// between the nodes in the cluster.
    /// </summary>
    Manual,

    /// <summary>
    /// Indicates that the synchronization mode for high availability is set to automatic.
    /// In this mode, data synchronization between the nodes in the cluster is handled automatically
    /// without requiring user intervention.
    /// </summary>
    Automatic
}