namespace HareDu;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Provides a configuration interface for setting various arguments on an operator policy.
/// </summary>
public interface OperatorPolicyArgumentConfigurator
{
    /// <summary>
    /// Set 'message-ttl' argument on the operator policy.
    /// </summary>
    /// <param name="milliseconds"></param>
    void SetMessageTimeToLive([NotNull] ulong milliseconds);

    /// <summary>
    /// Set 'max-length-bytes' argument on the operator policy.
    /// </summary>
    /// <param name="value"></param>
    void SetMessageMaxSizeInBytes([NotNull] ulong value);

    /// <summary>
    /// Set 'max-length' argument on the operator policy.
    /// </summary>
    /// <param name="value"></param>
    void SetMessageMaxSize([NotNull] ulong value);

    /// <summary>
    /// Set 'expires' argument on the operator policy.
    /// </summary>
    /// <param name="milliseconds"></param>
    void SetExpiry([NotNull] ulong milliseconds);

    /// <summary>
    /// Set 'x-max-in-memory-bytes' argument on the operator policy.
    /// </summary>
    /// <param name="bytes"></param>
    void SetMaxInMemoryBytes([NotNull] ulong bytes);
        
    /// <summary>
    /// Set 'x-max-in-memory-length' argument on the operator policy.
    /// </summary>
    /// <param name="???"></param>
    void SetMaxInMemoryLength([NotNull] ulong messages);
        
    /// <summary>
    /// Set 'delivery-limit' argument on the operator policy.
    /// </summary>
    void SetDeliveryLimit([NotNull] ulong limit);
}