namespace HareDu;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Internal;
using Model;

public interface Channel :
    BrokerAPI
{
    /// <summary>
    /// Returns all channels on the current RabbitMQ node given the specified pagination parameters.
    /// </summary>
    /// <param name="configurator">Pagination parameters (e.g., page number, size, etc.) used to control how much data is returned.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Results<ChannelInfo>> GetAll(Action<PaginationConfigurator> configurator = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns all channels on the current RabbitMQ node given the specified connection.
    /// </summary>
    /// <param name="connectionName">The name of the connection that encapsulates channels.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Results<ChannelInfo>> GetByConnection(string connectionName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns all channels on the current RabbitMQ node given the specified virtual host.
    /// </summary>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Results<ChannelInfo>> GetByVirtualHost(string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns channel information on the current RabbitMQ node given the specified channel name.
    /// </summary>
    /// <param name="name">Name of the RabbitMQ broker channel.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result<ChannelInfo>> GetByName(string name, CancellationToken cancellationToken = default);
}