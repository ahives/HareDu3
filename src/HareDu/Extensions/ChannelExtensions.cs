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
    /// Returns all channels on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="configurator">Pagination parameters (e.g., page number, size, etc.) used to control how much data is returned.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a channel.</exception>
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
    /// Returns all channels on the current RabbitMQ node given the specified connection.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="connectionName">The name of the connection that encapsulates channels.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a channel.</exception>
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
    /// Returns all channels on the current RabbitMQ node given the specified virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a channel.</exception>
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
    /// Returns channel information on the current RabbitMQ node given the specified channel name.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="name">Name of the RabbitMQ broker channel.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a channel.</exception>
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