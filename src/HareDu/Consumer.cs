namespace HareDu;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

/// <summary>
/// Represents a component that provides access to RabbitMQ consumer data.
/// </summary>
public interface Consumer :
    BrokerAPI
{
    /// <summary>
    /// Returns all consumers on the current RabbitMQ node.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    [return: NotNull]
    Task<Results<ConsumerInfo>> GetAll([NotNull] CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns all consumers on the current RabbitMQ node within the virtual host.
    /// </summary>
    /// <param name="vhost">The name of the virtual host to filter the active consumers.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    [return: NotNull]
    Task<Results<ConsumerInfo>> GetByVirtualHost([NotNull] string vhost, [NotNull] CancellationToken cancellationToken = default);
}