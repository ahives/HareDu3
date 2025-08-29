namespace HareDu.Model;

/// <summary>
/// Specifies the type of broker object to which a policy is applied.
/// </summary>
public enum PolicyAppliedTo
{
    /// <summary>
    /// Specifies that the operator policy should be applied to queues.
    /// </summary>
    Queues,

    /// <summary>
    /// Specifies that the operator policy should be applied to classic queues.
    /// </summary>
    ClassicQueues,

    /// <summary>
    /// Specifies that the operator policy should be applied to quorum queues.
    /// </summary>
    QuorumQueues,

    /// <summary>
    /// Specifies that the operator policy should be applied to streams.
    /// </summary>
    Streams,

    /// <summary>
    /// Specifies that the operator policy should be applied to both queues and exchanges.
    /// </summary>
    QueuesAndExchanges
}