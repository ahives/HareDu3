namespace HareDu.Model;

/// <summary>
/// Determines the strategy for locating the leader of a RabbitMQ queue.
/// </summary>
public enum QueueLeaderLocator
{
    /// <summary>
    /// Specifies that the queue leader should be located on the RabbitMQ node where the client that created the queue is connected.
    /// This option ensures the leader is local to the client’s connection.
    /// </summary>
    ClientLocal,

    /// <summary>
    /// Specifies that the queue leader should be located on a RabbitMQ node in a way that achieves a balanced distribution of queue leaders across all nodes.
    /// This option ensures even load distribution among nodes in the cluster.
    /// </summary>
    Balanced
}