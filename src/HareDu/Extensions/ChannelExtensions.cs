namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Internal;
using Model;

public static class ChannelExtensions
{
    /// <summary>
    /// Retrieves all channels available on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API factory that provides access to the RabbitMQ broker operations.</param>
    /// <param name="configurator">Optional action for configuring pagination when retrieving channels.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running asynchronously.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result set of channel information.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Results<ChannelInfo>> GetAllChannels(this IBrokerFactory factory,
        Action<PaginationConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Channel>()
            .GetAll(configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves all channels associated with the specified connection.
    /// </summary>
    /// <param name="factory">The API factory that provides access to the RabbitMQ broker operations.</param>
    /// <param name="connectionName">The name of the connection for which the channels will be retrieved.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running asynchronously.</param>
    /// <returns>A task representing the asynchronous operation, containing the result set of channel information associated with the specified connection.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Results<ChannelInfo>> GetChannelsByConnection(this IBrokerFactory factory,
        string connectionName, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Channel>()
            .GetByConnection(connectionName, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves all channels associated with the specified virtual host in the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API factory that provides access to the RabbitMQ broker operations.</param>
    /// <param name="vhost">The name of the virtual host whose channels are to be retrieved.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running asynchronously.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result set of channel information for the virtual host.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Results<ChannelInfo>> GetChannelsByVirtualHost(this IBrokerFactory factory,
        string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Channel>()
            .GetByVirtualHost(vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves channel information based on the specified channel name.
    /// </summary>
    /// <param name="factory">The API factory that provides access to the RabbitMQ broker operations.</param>
    /// <param name="name">The name of the channel to retrieve.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running asynchronously.</param>
    /// <returns>A task that represents the asynchronous operation, containing the retrieved channel information.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<ChannelInfo>> GetChannelByName(this IBrokerFactory factory,
        string name, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        
        return await factory
            .API<Channel>()
            .GetByName(name, cancellationToken)
            .ConfigureAwait(false);
    }
}