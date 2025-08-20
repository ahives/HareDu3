namespace HareDu;

/// <summary>
/// Provides a configuration interface for setting various arguments on an operator policy.
/// </summary>
public interface OperatorPolicyArgumentConfigurator
{
    /// <summary>
    /// Set 'message-ttl' argument on the operator policy.
    /// </summary>
    /// <param name="milliseconds">The time-to-live for a message in milliseconds.</param>
    void SetMessageTimeToLive(ulong milliseconds);

    /// <summary>
    /// Set 'max-length-bytes' argument on the operator policy.
    /// </summary>
    /// <param name="value">The maximum size of a message in bytes that the queue can hold.</param>
    void SetMessageMaxSizeInBytes(ulong value);

    /// <summary>
    /// Set 'max-length' argument on the operator policy.
    /// </summary>
    /// <param name="value">The maximum number of messages that the queue can hold.</param>
    void SetMessageMaxSize(ulong value);

    /// <summary>
    /// Set 'expires' argument on the operator policy.
    /// </summary>
    /// <param name="milliseconds">The expiry timeout in milliseconds.</param>
    void SetExpiry(ulong milliseconds);

    /// <summary>
    /// Set 'max-in-memory-bytes' argument on the operator policy.
    /// </summary>
    /// <param name="bytes">The maximum number of bytes to be kept in memory.</param>
    void SetMaxInMemoryBytes(ulong bytes);

    /// <summary>
    /// Set 'max-in-memory-length' argument on the operator policy.
    /// </summary>
    /// <param name="messages">The maximum number of messages to be kept in memory.</param>
    void SetMaxInMemoryLength(ulong messages);

    /// <summary>
    /// Set 'delivery-limit' argument on the operator policy.
    /// </summary>
    /// <param name="limit">The maximum number of times a message may be delivered.</param>
    void SetDeliveryLimit(ulong limit);
}