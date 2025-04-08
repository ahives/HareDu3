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

    /// <summary>
    /// Binds the specified exchange to another exchange using the provided configuration.
    /// </summary>
    /// <param name="factory">The RabbitMQ broker factory instance used to execute the binding operation.</param>
    /// <param name="vhost">The name of the RabbitMQ virtual host containing the exchanges.</param>
    /// <param name="exchange">The name of the exchange to bind.</param>
    /// <param name="configurator">An action to configure the binding operation, including destination exchange and binding properties.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task containing the result of the binding operation, including the binding information.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the factory or configurator is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a queue.</exception>
    public static async Task<Result<BindingInfo>> BindToExchange(this IBrokerFactory factory,
        string vhost, string exchange, Action<BindingConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Exchange>()
            .BindToExchange(vhost, exchange, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Unbinds an exchange from another exchange within a specified virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality of the broker.</param>
    /// <param name="vhost">The virtual host within which the exchange is bound.</param>
    /// <param name="configurator">The configuration for unbinding the exchanges.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread, if necessary.</param>
    /// <returns>Returns the result of the unbinding operation, including success or failure details.</returns>
    /// <exception cref="ArgumentNullException">Throws if the IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a queue.</exception>
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