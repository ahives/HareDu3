namespace HareDu;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Internal;
using Model;

public interface Queue :
    BrokerAPI
{
    /// <summary>
    /// Returns information of all queues on the current RabbitMQ node.
    /// </summary>
    /// <param name="configurator"></param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Results<QueueInfo>> GetAll(Action<PaginationConfigurator> configurator = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns detailed information of all queues on the current RabbitMQ node.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Results<QueueDetailInfo>> GetDetails(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates specified queue on the specified RabbitMQ virtual host and node.
    /// </summary>
    /// <param name="name">Name of the RabbitMQ broker queue.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="node">Name of the RabbitMQ node.</param>
    /// <param name="configurator">Describes the attributes for which the queue is to be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Create(string name, string vhost, string node, Action<QueueConfigurator> configurator = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes specified queue on the specified RabbitMQ virtual host and node.
    /// </summary>
    /// <param name="name">Name of the RabbitMQ broker queue.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="configurator">Describes the attributes for which the queue is to be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Delete(string name, string vhost, Action<QueueDeletionConfigurator> configurator = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Purges all messages in the specified queue on the specified RabbitMQ virtual host on the current node.
    /// </summary>
    /// <param name="name">Name of the RabbitMQ broker queue.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Empty(string name, string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Syncs or cancels sync of specified RabbitMQ queue.
    /// </summary>
    /// <param name="name">Name of the RabbitMQ broker queue.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="syncAction">Sync action to be performed on RabbitMQ queue.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Sync(string name, string vhost, QueueSyncAction syncAction, CancellationToken cancellationToken = default);

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
    /// <param name="configurator">Describes how the binding will be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Unbind(string vhost, Action<UnbindingConfigurator> configurator, CancellationToken cancellationToken = default);
}