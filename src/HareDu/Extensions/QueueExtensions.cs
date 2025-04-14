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
    /// Retrieves all queues from the RabbitMQ broker using the specified configuration and cancellation token.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality to connect to the RabbitMQ broker.</param>
    /// <param name="configurator">The configuration action for setting up optional pagination parameters when retrieving the queues.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation and contains the results of type <see cref="QueueInfo"/>.</returns>
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
    /// Creates a queue on the RabbitMQ broker using the specified name, virtual host, node, and optional configuration.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality to connect to the RabbitMQ broker.</param>
    /// <param name="name">The name of the queue to be created.</param>
    /// <param name="vhost">The name of the virtual host where the queue will be created.</param>
    /// <param name="node">The name of the node on which the queue should be created.</param>
    /// <param name="configurator">The configuration action for setting up optional queue parameters.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation and contains a result indicating success or failure.</returns>
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
    /// Empties a specific RabbitMQ queue within a given virtual host.
    /// </summary>
    /// <param name="factory">The API that provides the underlying functionality to interact with the RabbitMQ broker.</param>
    /// <param name="name">The name of the queue to be emptied.</param>
    /// <param name="vhost">The virtual host where the queue resides.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation and contains the result of the operation of type <see cref="Result"/>.</returns>
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
    /// Deletes a specified queue from the RabbitMQ broker based on the provided parameters.
    /// </summary>
    /// <param name="factory">The API that implements the functionality to connect to and interact with the RabbitMQ broker.</param>
    /// <param name="name">The name of the queue to be deleted.</param>
    /// <param name="vhost">The virtual host where the queue resides.</param>
    /// <param name="configurator">Optional configuration action for providing additional parameters relevant to queue deletion.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation and contains the result of the deletion operation as type <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a queue.</exception>
    public static async Task<Result> DeleteQueue(this IBrokerFactory factory,
        string name, string vhost, Action<QueueDeletionConfigurator> configurator = null,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>()
            .Delete(name, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Synchronizes the state of the specified RabbitMQ queue for the given virtual host.
    /// </summary>
    /// <param name="factory">The API that provides access to the RabbitMQ broker's features.</param>
    /// <param name="name">The name of the queue to synchronize.</param>
    /// <param name="vhost">The virtual host within which the queue resides.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the queue synchronization.</returns>
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
    /// Cancels the synchronization operation for a specified queue in the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The API that implements the functionality to connect and make requests to the RabbitMQ broker.</param>
    /// <param name="name">The name of the queue for which the synchronization operation will be canceled.</param>
    /// <param name="vhost">The virtual host to which the queue belongs.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation and contains the operation result of type <see cref="Result"/>.</returns>
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