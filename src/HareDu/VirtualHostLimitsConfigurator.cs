namespace HareDu;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Configures resource limits for a RabbitMQ virtual host.
/// </summary>
public interface VirtualHostLimitsConfigurator
{
    /// <summary>
    /// Set the 'max-connections' RabbitMQ virtual host limit value.
    /// </summary>
    /// <param name="value">The maximum number of connections allowed for the virtual host.</param>
    void SetMaxConnectionLimit([NotNull] ulong value);

    /// <summary>
    /// Set the 'max-queues' RabbitMQ virtual host limit value.
    /// </summary>
    /// <param name="value">The maximum number of queues allowed for the virtual host.</param>
    void SetMaxQueueLimit([NotNull] ulong value);
}