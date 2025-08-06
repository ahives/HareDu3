namespace HareDu;

/// <summary>
/// Represents the action that can be taken to manage the synchronization of a queue's messages
/// between its master and mirrors in a RabbitMQ cluster.
/// </summary>
public enum QueueSyncAction
{
    /// <summary>
    /// Represents the action to initiate synchronization of a queue's messages
    /// from its master to its mirrors in a RabbitMQ cluster.
    /// </summary>
    Sync,

    /// <summary>
    /// Represents the action to cancel an ongoing synchronization of a queue's messages
    /// between the master and its mirrors in a RabbitMQ cluster.
    /// </summary>
    CancelSync
}