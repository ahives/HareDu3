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
    /// Retrieves a list of all consumers on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The factory instance used to access the RabbitMQ broker functionalities.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <exception cref="ArgumentNullException">Thrown when the provided IBrokerFactory instance is null.</exception>
    /// <returns>A task containing the results of consumer information for all active consumers.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
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
    /// Retrieves all active consumers for the specified virtual host on the RabbitMQ node.
    /// </summary>
    /// <param name="factory">The factory instance used to access the RabbitMQ broker functionalities.</param>
    /// <param name="vhost">The name of the virtual host from which to retrieve the active consumers.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <exception cref="ArgumentNullException">Thrown when the provided IBrokerFactory instance is null.</exception>
    /// <returns>A task representing the asynchronous operation, containing the results of consumer information for the specified virtual host.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
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