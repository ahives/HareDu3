namespace HareDu;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public interface VirtualHostLimits :
    BrokerAPI
{
    /// <summary>
    /// Returns limit information about each virtual host on the current RabbitMQ server.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result<IReadOnlyList<VirtualHostLimitsInfo>>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Defines specified limits on the RabbitMQ virtual host on the current server.
    /// </summary>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="configurator">Describes how the virtual host limits will be defined.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Define(string vhost, Action<VirtualHostLimitsConfigurator> configurator = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the limits for the specified virtual host on the current RabbitMQ server.
    /// </summary>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Delete(string vhost, CancellationToken cancellationToken = default);
}