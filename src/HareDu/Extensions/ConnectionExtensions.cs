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
    /// Returns all connections on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="configurator"></param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
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
    /// Returns all connections on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost"></param>
    /// <param name="configurator"></param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
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
    /// Returns all connections on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="name"></param>
    /// <param name="configurator"></param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
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
    /// Returns all connections on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username"></param>
    /// <param name="configurator"></param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
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
    /// Deletes an active connection on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="connection">The name of the RabbitMQ connection that is to be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
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
    /// Deletes an active connection on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username">The user to which the RabbitMQ connection that is to be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
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