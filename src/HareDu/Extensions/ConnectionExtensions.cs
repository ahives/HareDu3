namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Internal;
using Model;

public static class ConnectionExtensions
{
    /// <summary>
    /// Retrieves all connections on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API factory that provides the functionality to interact with RabbitMQ.</param>
    /// <param name="configurator">Optional pagination parameters (e.g., page number, size, etc.) used to control how much data is returned.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the results of type <see cref="Results{ConnectionInfo}"/> that include information about all connections.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Results<ConnectionInfo>> GetAllConnections(this IBrokerFactory factory,
        Action<PaginationConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Connection>()
            .GetAll(configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves a list of connections associated with the specified virtual host.
    /// </summary>
    /// <param name="factory">The API factory that provides the functionality to interact with RabbitMQ.</param>
    /// <param name="vhost">The name of the virtual host for which to retrieve the connections.</param>
    /// <param name="configurator">Optional pagination parameters (e.g., page number, size, etc.) used to control how much data is returned.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the results of type <see cref="Results{ConnectionInfo}"/> that include information about all connections in the specified virtual host.</returns>
    /// <exception cref="ArgumentNullException">Throws if the factory is null.</exception>
    public static async Task<Results<ConnectionInfo>> GetConnectionsByVirtualHost(this IBrokerFactory factory,
        string vhost, Action<PaginationConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Connection>()
            .GetByVirtualHost(vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves connection information from RabbitMQ by its name.
    /// </summary>
    /// <param name="factory">The API factory providing the functionality to interact with RabbitMQ.</param>
    /// <param name="name">The name of the connection to retrieve.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the results of type <see cref="Results{ConnectionInfo}"/> that include details about the connection.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Results<ConnectionInfo>> GetConnectionsByName(this IBrokerFactory factory,
        string name, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Connection>()
            .GetByName(name, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves all connections associated with a specific user on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API factory that provides the functionality to interact with RabbitMQ.</param>
    /// <param name="username">The name of the user whose connections should be retrieved.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the results of type <see cref="Results{ConnectionInfo}"/> that include information about the connections associated with the specified user.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Results<ConnectionInfo>> GetConnectionsByUser(this IBrokerFactory factory,
        string username, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Connection>()
            .GetByUser(username, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a specific connection on the RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API factory that provides the functionality to interact with RabbitMQ.</param>
    /// <param name="connection">The name of the connection to be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the operation's result of type <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result> DeleteConnection(this IBrokerFactory factory,
        string connection, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Connection>()
            .Delete(connection, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes all connections associated with the specified user on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API factory that provides the functionality to interact with RabbitMQ.</param>
    /// <param name="username">The username whose associated connections need to be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result"/> object indicating the success or failure of the operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result> DeleteConnectionByUser(this IBrokerFactory factory,
        string username, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Connection>()
            .DeleteByUser(username, cancellationToken)
            .ConfigureAwait(false);
    }
}