namespace HareDu;

using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public interface Consumer :
    BrokerAPI
{
    /// <summary>
    /// Returns all consumers on the current RabbitMQ node.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Results<ConsumerInfo>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns all consumers on the current RabbitMQ node within the virtual host.
    /// </summary>
    /// <param name="vhost">The name of the virtual host to filter the active consumers.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Results<ConsumerInfo>> GetByVirtualHost(string vhost, CancellationToken cancellationToken = default);
}