namespace HareDu.Extensions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Security;
using Model;

public static class ConnectionExtensions
{
    /// <summary>
    /// Retrieves all connections in the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The API factory that provides the functionality to interact with RabbitMQ.</param>
    /// <param name="credentials">The action used to specify the credentials for authenticating with the RabbitMQ broker.</param>
    /// <param name="pagination">Optional pagination parameters (e.g., page number, size, etc.) used to control how much data is returned.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the results of type <see cref="Results{ConnectionInfo}"/> that include information about all connections in the broker.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Results<ConnectionInfo>> GetAllConnections(
        [NotNull] this IBrokerFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] Action<PaginationConfigurator> pagination = null,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Connection>(credentials)
            .GetAll(pagination, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves a list of connections associated with a specific virtual host in the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The API factory that provides the functionality to interact with RabbitMQ.</param>
    /// <param name="credentials">The action used to specify the credentials for authenticating with the RabbitMQ broker.</param>
    /// <param name="vhost">The name of the virtual host for which connections are to be retrieved.</param>
    /// <param name="pagination">Optional pagination parameters (e.g., page number, size, etc.) used to control how much data is returned.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the results of type <see cref="Results{ConnectionInfo}"/> that include information about the connections for the specified virtual host.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Results<ConnectionInfo>> GetConnectionsByVirtualHost(
        [NotNull] this IBrokerFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] string vhost,
        [NotNull] Action<PaginationConfigurator> pagination = null,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Connection>(credentials)
            .GetByVirtualHost(vhost, pagination, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves connection information from the RabbitMQ broker for a specific connection name.
    /// </summary>
    /// <param name="factory">The API factory that provides the functionality to interact with RabbitMQ.</param>
    /// <param name="credentials">The action used to specify the credentials for authenticating with the RabbitMQ broker.</param>
    /// <param name="name">The name of the RabbitMQ connection to retrieve information for.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains the results of type <see cref="Results{ConnectionInfo}"/> with details about the specified connection.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Results<ConnectionInfo>> GetConnectionsByName(
        [NotNull] this IBrokerFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] string name,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Connection>(credentials)
            .GetByName(name, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves all connections in the RabbitMQ broker for a specific user.
    /// </summary>
    /// <param name="factory">The API factory that provides the functionality to interact with RabbitMQ.</param>
    /// <param name="credentials">The action used to specify the credentials for authenticating with the RabbitMQ broker.</param>
    /// <param name="username">The username for which to retrieve associated connections.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the results of type <see cref="Results{ConnectionInfo}"/> that include information about all connections associated with the specified user.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Results<ConnectionInfo>> GetConnectionsByUser(
        [NotNull] this IBrokerFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] string username,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Connection>(credentials)
            .GetByUser(username, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a specific connection from the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The API factory that provides the functionality to interact with RabbitMQ.</param>
    /// <param name="credentials">The action used to specify the credentials for authenticating with the RabbitMQ broker.</param>
    /// <param name="connection">The identifier of the connection to be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Result"/> indicating the status of the delete operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result> DeleteConnection(
        [NotNull] this IBrokerFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] string connection,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Connection>(credentials)
            .Delete(connection, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes all connections for a specified user in the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The API factory that provides the functionality to interact with RabbitMQ.</param>
    /// <param name="credentials">The action used to specify the credentials for authenticating with the RabbitMQ broker.</param>
    /// <param name="username">The username for which all associated connections are to be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion succeeded or failed.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result> DeleteConnectionByUser(
        [NotNull] this IBrokerFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] string username,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Connection>(credentials)
            .DeleteByUser(username, cancellationToken)
            .ConfigureAwait(false);
    }
}