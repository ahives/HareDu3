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
    /// <param name="name">Name of the RabbitMQ broker queue.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="node">Name of the RabbitMQ node.</param>
    /// <param name="configurator">Describes how the queue will be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a queue.</exception>
    public static async Task<Result> CreateQueue(this IBrokerFactory factory,
        string name, string vhost, string node, Action<QueueConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>()
            .Create(name, vhost, node, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Purges all messages in the specified queue on the specified RabbitMQ virtual host on the current node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="name">Name of the RabbitMQ broker queue.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a queue.</exception>
    public static async Task<Result> EmptyQueue(this IBrokerFactory factory,
        string name, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>()
            .Empty(name, vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes specified queue on the specified RabbitMQ virtual host and node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="name">Name of the RabbitMQ broker queue.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="configurator">Describes how the queue should be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a queue.</exception>
    public static async Task<Result> DeleteQueue(this IBrokerFactory factory,
        string name, string vhost, Action<QueueDeletionConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>()
            .Delete(name, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Syncs of specified RabbitMQ queue.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="name">Name of the RabbitMQ broker queue.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a queue.</exception>
    public static async Task<Result> SyncQueue(this IBrokerFactory factory,
        string name, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>()
            .Sync(name, vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Cancels sync of specified RabbitMQ queue.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="name">Name of the RabbitMQ broker queue.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a queue.</exception>
    public static async Task<Result> CancelQueueSync(this IBrokerFactory factory,
        string name, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>()
            .CancelSync(name, vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Binds an exchange to a queue within the specified virtual host using the provided configuration.
    /// </summary>
    /// <param name="factory">The API interface for interacting with the RabbitMQ broker.</param>
    /// <param name="vhost">The virtual host in RabbitMQ where the exchange and queue reside.</param>
    /// <param name="exchange">The name of the exchange to bind to the queue.</param>
    /// <param name="configurator">The configuration action for specifying binding details (e.g., destination and binding key).</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A <see cref="Result{BindingInfo}"/> instance containing details of the binding if the operation succeeds.</returns>
    /// <exception cref="ArgumentNullException">Throws if the IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a queue.</exception>
    public static async Task<Result<BindingInfo>> BindToQueue(this IBrokerFactory factory,
        string vhost, string exchange, Action<BindingConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>()
            .BindToQueue(vhost, exchange, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Unbinds a queue from a source in the specified virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost">The virtual host where the binding is defined.</param>
    /// <param name="configurator">Describes how the unbinding operation will be configured, including source and destination.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result indicating the success or failure of the unbinding operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a queue.</exception>
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