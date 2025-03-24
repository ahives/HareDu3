namespace HareDu;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public interface VirtualHost :
    BrokerAPI
{
    /// <summary>
    /// Returns information about each virtual host on the current RabbitMQ server.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Results<VirtualHostInfo>> GetAll(CancellationToken cancellationToken = default);

    Task<Result<VirtualHostInfo>> Get(string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates the specified RabbitMQ virtual host on the current server.
    /// </summary>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="configurator">Describes how the virtual host will be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Create(string vhost, Action<VirtualHostConfigurator> configurator = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified RabbitMQ virtual host on the current server.
    /// </summary>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Delete(string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts up the specified RabbitMQ virtual host on the specified node.
    /// </summary>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="node">Name of the RabbitMQ server node.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Startup(string vhost, string node, CancellationToken cancellationToken = default);
}