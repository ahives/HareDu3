namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class ConsumerExtensions
{
    /// <summary>
    /// Returns all consumers on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a consumer.</exception>
    /// <returns></returns>
    public static async Task<Results<ConsumerInfo>> GetAllConsumers(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Consumer>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }
    
    /// <summary>
    /// Returns all consumers on the current RabbitMQ node within the virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost">The name of the virtual host to filter the active consumers.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a consumer.</exception>
    /// <returns></returns>
    public static async Task<Results<ConsumerInfo>> GetConsumersByVirtualHost(this IBrokerFactory factory, string vhost,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Consumer>()
            .GetByVirtualHost(vhost, cancellationToken)
            .ConfigureAwait(false);
    }
}