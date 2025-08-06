namespace HareDu;

/// <summary>
/// Specifies the type of broker object to which a policy is applied.
/// </summary>
public enum PolicyAppliedTo
{
    /// <summary>
    /// Represents the application of a policy to all types of broker objects,
    /// including both exchanges and queues.
    /// </summary>
    All,

    /// <summary>
    /// Represents the application of a policy specifically to exchanges within the broker.
    /// </summary>
    Exchanges,

    /// <summary>
    /// Represents the application of a policy specifically to queues within the broker.
    /// </summary>
    Queues
}