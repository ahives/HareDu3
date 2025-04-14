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
    /// Retrieves all RabbitMQ exchanges from the target broker.
    /// </summary>
    /// <param name="factory">The API that provides the implementation for retrieving exchanges.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task containing the results of the retrieved exchange information.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
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
    /// Creates a new RabbitMQ exchange on the specified virtual host.
    /// </summary>
    /// <param name="factory">The API that provides the implementation for creating exchanges.</param>
    /// <param name="exchange">The name of the exchange to be created.</param>
    /// <param name="vhost">The virtual host where the exchange will be created.</param>
    /// <param name="configurator">An optional delegate to configure the exchange properties.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task containing the result of the exchange creation operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
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
    /// Deletes the specified exchange from the specified virtual host.
    /// </summary>
    /// <param name="factory">The API that provides the implementation for deleting exchanges.</param>
    /// <param name="exchange">The name of the exchange to be deleted.</param>
    /// <param name="vhost">The virtual host from which the exchange will be deleted.</param>
    /// <param name="configurator">An optional delegate to configure the exchange deletion operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result> DeleteExchange(this IBrokerFactory factory,
        string exchange, string vhost, Action<ExchangeDeletionConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Exchange>()
            .Delete(exchange, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Binds an exchange to another exchange in the specified virtual host with the given configuration.
    /// </summary>
    /// <param name="factory">The API providing the implementation for exchange binding operations.</param>
    /// <param name="vhost">The name of the virtual host where the exchange resides.</param>
    /// <param name="exchange">The name of the source exchange to bind.</param>
    /// <param name="configurator">The configuration settings for the binding, including destination and binding key.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task containing the result of the binding operation, including binding information.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<BindingInfo>> BindToExchange(this IBrokerFactory factory,
        string vhost, string exchange, Action<BindingConfigurator> configurator,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Exchange>()
            .BindToExchange(vhost, exchange, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Unbinds an existing binding between the specified virtual host exchange and another exchange.
    /// </summary>
    /// <param name="factory">The API that provides the implementation for unbinding exchanges.</param>
    /// <param name="vhost">The virtual host where the exchange resides.</param>
    /// <param name="configurator">The configurator for defining the unbinding parameters.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task containing the result of the unbinding operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result> UnbindFromExchange(this IBrokerFactory factory,
        string vhost, Action<UnbindingConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        
        return await factory
            .API<Exchange>()
            .Unbind(vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }
}