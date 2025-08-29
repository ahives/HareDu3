namespace HareDu.Model;

/// <summary>
/// Defines the types of entities to which an operator policy can be applied. (see https://www.rabbitmq.com/docs/policies)
/// </summary>
public enum OperatorPolicyAppliedTo
{
    /// <summary>
    /// Specifies that the operator policy should be applied to queues.
    /// </summary>
    Queues,

    /// <summary>
    /// Specifies that the operator policy should be applied to exchanges.
    /// </summary>
    Exchanges,

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
    Streams
}