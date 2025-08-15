namespace HareDu.Model;

/// <summary>
/// Defines the synchronization state of mirrored queues on a RabbitMQ node.
/// </summary>
public enum NodeMirrorSyncState
{
    /// <summary>
    /// Represents the state where mirrored queues on the RabbitMQ node are synchronized:
    /// at least one synchronized mirror for each mirrored queue exists, and the node is operational.
    /// </summary>
    WithSyncedMirrorsOnline,

    /// <summary>
    /// Represents the state where no synchronized mirrors exist for one or more mirrored queues,
    /// indicating potential issues with the RabbitMQ node's mirrored queue synchronization.
    /// </summary>
    WithoutSyncedMirrorsOnline,

    /// <summary>
    /// Represents an unknown or unrecognized state for the synchronization status of mirrored queues
    /// on the RabbitMQ node. This state indicates that the system is unable to determine or categorize
    /// the synchronization condition of the mirrored queues.
    /// </summary>
    NotRecognized
}