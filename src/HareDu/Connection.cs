namespace HareDu;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Internal;
using Model;

public interface Connection :
    BrokerAPI
{
    /// <summary>
    /// Returns all connections on the current RabbitMQ node.
    /// </summary>
    /// <param name="configurator"></param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Results<ConnectionInfo>> GetAll(Action<PaginationConfigurator> configurator = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vhost"></param>
    /// <param name="configurator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Results<ConnectionInfo>> GetByVirtualHost(string vhost, Action<PaginationConfigurator> configurator = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Results<ConnectionInfo>> GetByName(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Results<ConnectionInfo>> GetByUser(string username, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an active connection on the current RabbitMQ node.
    /// </summary>
    /// <param name="connection">The name of the RabbitMQ connection that is to be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Delete(string connection, CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result> DeleteByUser(string username, CancellationToken cancellationToken = default);
}