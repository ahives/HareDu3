namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class ExchangeExtensions
{
    /// <summary>
    /// Returns all exchanges on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a exchange.</exception>
    public static async Task<Results<ExchangeInfo>> GetAllExchanges(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Exchange>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates the specified exchange on the target RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="exchange">Name of the RabbitMQ exchange.</param>
    /// <param name="vhost">Name of the RabbitMQ virtual host.</param>
    /// <param name="configurator">Describes how the exchange will be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a exchange.</exception>
    public static async Task<Result> CreateExchange(this IBrokerFactory factory,
        string exchange, string vhost, Action<ExchangeConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Exchange>()
            .Create(exchange, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the specified exchange on the target RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="exchange">Name of the RabbitMQ exchange.</param>
    /// <param name="vhost">Name of the RabbitMQ virtual host.</param>
    /// <param name="configurator">Describes how the exchange will be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a exchange.</exception>
    public static async Task<Result> DeleteExchange(this IBrokerFactory factory,
        string exchange, string vhost, Action<ExchangeDeletionConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Exchange>()
            .Delete(exchange, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }
}