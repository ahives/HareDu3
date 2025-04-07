namespace HareDu;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public interface Exchange :
    BrokerAPI
{
    /// <summary>
    /// Returns all exchanges on the current RabbitMQ node.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Results<ExchangeInfo>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates the specified exchange on the target RabbitMQ virtual host.
    /// </summary>
    /// <param name="exchange">Name of the RabbitMQ exchange.</param>
    /// <param name="vhost">Name of the RabbitMQ virtual host.</param>
    /// <param name="configurator">Describes how the queue will be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Create(string exchange, string vhost, Action<ExchangeConfigurator> configurator = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified exchange on the target RabbitMQ virtual host.
    /// </summary>
    /// <param name="exchange">Name of the RabbitMQ exchange.</param>
    /// <param name="vhost">Name of the RabbitMQ virtual host.</param>
    /// <param name="configurator">Describes how the queue will be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Delete(string exchange, string vhost, Action<ExchangeDeletionConfigurator> configurator = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates the specified binding between source (i.e. queue/exchange) and destination (i.e. queue/exchange) on the specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="vhost">The virtual host where the binding is defined.</param>
    /// <param name="exchange"></param>
    /// <param name="configurator">Describes how the binding will be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result<BindingInfo>> Bind(string vhost, string exchange, Action<BindingConfigurator> configurator, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified exchange on the specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="vhost">The virtual host where the binding is defined.</param>
    /// <param name="exchange"></param>
    /// <param name="configurator">Describes how the binding will be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Unbind(string vhost, Action<UnbindingConfigurator> configurator, CancellationToken cancellationToken = default);
}