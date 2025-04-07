namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Internal;
using Model;

public static class QueueExtensions
{
    /// <summary>
    /// Returns all queues on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="configurator">Pagination parameters (e.g., page number, size, etc.) used to control how much data is returned.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a queue.</exception>
    public static async Task<Results<QueueInfo>> GetAllQueues(this IBrokerFactory factory,
        Action<PaginationConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>()
            .GetAll(configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates specified queue on the specified RabbitMQ virtual host and node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="queue">Name of the RabbitMQ broker queue.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="node">Name of the RabbitMQ node.</param>
    /// <param name="configurator">Describes how the queue will be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a queue.</exception>
    public static async Task<Result> CreateQueue(this IBrokerFactory factory,
        string queue, string vhost, string node, Action<QueueConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>()
            .Create(queue, vhost, node, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Purges all messages in the specified queue on the specified RabbitMQ virtual host on the current node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="queue">Name of the RabbitMQ broker queue.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a queue.</exception>
    public static async Task<Result> EmptyQueue(this IBrokerFactory factory,
        string queue, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>()
            .Empty(queue, vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes specified queue on the specified RabbitMQ virtual host and node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="queue">Name of the RabbitMQ broker queue.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="configurator">Describes how the queue should be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a queue.</exception>
    public static async Task<Result> DeleteQueue(this IBrokerFactory factory,
        string queue, string vhost, Action<QueueDeletionConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>()
            .Delete(queue, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Syncs of specified RabbitMQ queue.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="queue">Name of the RabbitMQ broker queue.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a queue.</exception>
    public static async Task<Result> SyncQueue(this IBrokerFactory factory,
        string queue, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>()
            .Sync(queue, vhost, QueueSyncAction.Sync, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Cancels sync of specified RabbitMQ queue.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="queue">Name of the RabbitMQ broker queue.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a queue.</exception>
    public static async Task<Result> CancelQueueSync(this IBrokerFactory factory,
        string queue, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>()
            .Sync(queue, vhost, QueueSyncAction.CancelSync, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="vhost"></param>
    /// <param name="exchange"></param>
    /// <param name="configurator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<Result<BindingInfo>> BindToQueue(this IBrokerFactory factory,
        string vhost, string exchange, Action<BindingConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>()
            .Bind(vhost, exchange, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="vhost"></param>
    /// <param name="configurator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<Result> UnbindFromQueue(this IBrokerFactory factory,
        string vhost, Action<UnbindingConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>()
            .Unbind(vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }
}