namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Security;
using Model;

public static class QueueExtensions
{
    /// <summary>
    /// Retrieves all queues from the RabbitMQ broker using the specified settings.
    /// </summary>
    /// <param name="factory">The API that implements the functionality to interact with the RabbitMQ broker.</param>
    /// <param name="credentials">The credential provider used to authenticate with the RabbitMQ broker.</param>
    /// <param name="pagination">The configuration action for setting up optional pagination parameters.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation and contains the result of type <see cref="Results{QueueInfo}"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Results<QueueInfo>> GetAllQueues(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, Action<PaginationConfigurator> pagination = null,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>(credentials)
            .GetAll(pagination, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a new queue on the RabbitMQ broker using the specified configuration.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality to interact with the RabbitMQ broker.</param>
    /// <param name="credentials">The credential provider to authenticate with the RabbitMQ broker.</param>
    /// <param name="name">The name of the queue to be created.</param>
    /// <param name="vhost">The name of the virtual host where the queue will be created.</param>
    /// <param name="node">The node on which the queue will exist.</param>
    /// <param name="configurator">The configuration action for setting up optional queue parameters.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation and contains the result of type <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> CreateQueue(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string name, string vhost, string node,
        Action<QueueConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>(credentials)
            .Create(name, vhost, node, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Removes all messages from the specified queue in the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The factory interface used to interact with the RabbitMQ broker.</param>
    /// <param name="credentials">The credential provider used for authenticating with the RabbitMQ broker.</param>
    /// <param name="name">The name of the queue to be emptied.</param>
    /// <param name="vhost">The virtual host containing the queue to be emptied.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation and contains the result of type <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> EmptyQueue(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string name, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>(credentials)
            .Empty(name, vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a queue from the RabbitMQ broker using the provided configuration and cancellation token.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality to connect to the RabbitMQ broker.</param>
    /// <param name="credentials">The credential provider used to authenticate and authorize the deletion operation against the RabbitMQ broker.</param>
    /// <param name="name">The name of the queue to be deleted.</param>
    /// <param name="vhost">The virtual host from which the queue will be deleted.</param>
    /// <param name="configurator">Optional configuration action for setting additional parameters when deleting the queue.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation and contains the result of the deletion operation of type <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> DeleteQueue(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string name, string vhost,
        Action<QueueDeletionConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>(credentials)
            .Delete(name, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Synchronizes the state of the specified queue within the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality to connect to the RabbitMQ broker.</param>
    /// <param name="credentials">The action to configure the credentials for authenticating with the RabbitMQ broker.</param>
    /// <param name="name">The name of the queue to be synchronized.</param>
    /// <param name="vhost">The virtual host where the queue resides.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the synchronization process.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> SyncQueue(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string name, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>(credentials)
            .Sync(name, vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Cancels synchronization of a queue in the RabbitMQ broker using the specified configuration and cancellation token.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality to connect to the RabbitMQ broker.</param>
    /// <param name="credentials">The action to configure the credentials required for authentication with the RabbitMQ broker.</param>
    /// <param name="name">The name of the queue whose synchronization is being canceled.</param>
    /// <param name="vhost">The virtual host containing the queue whose synchronization is being canceled.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation and contains the result of the cancellation action.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> CancelQueueSync(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string name, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>(credentials)
            .CancelSync(name, vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a binding between an exchange and a queue on the specified virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality to connect to the RabbitMQ broker.</param>
    /// <param name="credentials">The credentials provider used to authenticate the connection to RabbitMQ.</param>
    /// <param name="vhost">The virtual host where the queue and exchange are defined.</param>
    /// <param name="exchange">The name of the exchange to bind to the queue.</param>
    /// <param name="configurator">The configuration action for setting up the queue binding parameters.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the asynchronous operation and containing the results of type <see cref="Result{BindingInfo}"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result<BindingInfo>> BindToQueue(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string vhost, string exchange,
        Action<BindingConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>(credentials)
            .BindToQueue(vhost, exchange, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Removes the binding between a specified queue and an exchange within the given virtual host.
    /// </summary>
    /// <param name="factory">The API that provides functionality to interact with the RabbitMQ broker.</param>
    /// <param name="credentials">The credentials required to authenticate with the RabbitMQ broker.</param>
    /// <param name="vhost">The virtual host where the queue and exchange reside.</param>
    /// <param name="configurator">An action to configure the unbinding options.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation, returning an instance of <see cref="Result"/> to indicate the outcome of the unbinding operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> UnbindFromQueue(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string vhost, Action<UnbindingConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Queue>(credentials)
            .Unbind(vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }
}