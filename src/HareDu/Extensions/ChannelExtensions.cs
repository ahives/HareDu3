namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Configuration;
using Internal;
using Model;

public static class ChannelExtensions
{
    /// <summary>
    /// Retrieves all channel information across the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The API factory that provides access to RabbitMQ broker operations.</param>
    /// <param name="credentials">The credentials required to authenticate with the RabbitMQ API.</param>
    /// <param name="configurator">Optional configuration for pagination of channel results.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running asynchronously.</param>
    /// <returns>A task representing the asynchronous operation, containing a collection of channel information.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Results<ChannelInfo>> GetAllChannels(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, Action<PaginationConfigurator> configurator = null,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<Channel>(credentials)
            .GetAll(configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves all channel information associated with a specific connection in the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The API factory that provides access to RabbitMQ broker operations.</param>
    /// <param name="credentials">The credentials required to authenticate with the RabbitMQ API.</param>
    /// <param name="connectionName">The name of the connection for which the channel information will be retrieved.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running asynchronously.</param>
    /// <returns>A task representing the asynchronous operation, containing a collection of channel information for the specified connection.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Results<ChannelInfo>> GetChannelsByConnection(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string connectionName,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<Channel>(credentials)
            .GetByConnection(connectionName, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves all channel information for a specific virtual host from the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The API factory that provides access to RabbitMQ broker operations.</param>
    /// <param name="credentials">The credentials required to authenticate with the RabbitMQ API.</param>
    /// <param name="vhost">The name of the virtual host for which channel information is to be retrieved.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running asynchronously.</param>
    /// <returns>A task representing the asynchronous operation, containing a collection of channel information for the specified virtual host.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Results<ChannelInfo>> GetChannelsByVirtualHost(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<Channel>(credentials)
            .GetByVirtualHost(vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves information about a specific channel by its name.
    /// </summary>
    /// <param name="factory">The API factory that provides access to RabbitMQ broker operations.</param>
    /// <param name="credentials">Action to configure the credentials for authentication with the RabbitMQ broker.</param>
    /// <param name="name">The name of the channel to retrieve information for.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running asynchronously.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the channel information.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<ChannelInfo>> GetChannelByName(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string name, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<Channel>(credentials)
            .GetByName(name, cancellationToken)
            .ConfigureAwait(false);
    }
}