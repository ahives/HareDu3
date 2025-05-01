namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Security;
using Model;

public static class ExchangeExtensions
{
    /// <summary>
    /// Retrieves all exchanges available in the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The API factory responsible for handling broker interactions.</param>
    /// <param name="credentials">The delegate used to provide user credentials for authentication.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task containing the results of the retrieved exchanges, including metadata about the exchanges.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Results<ExchangeInfo>> GetAllExchanges(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Exchange>(credentials)
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a new exchange in the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The API factory responsible for managing broker interactions.</param>
    /// <param name="credentials">The delegate used to provide user credentials for authentication.</param>
    /// <param name="exchange">The name of the exchange to be created.</param>
    /// <param name="vhost">The virtual host in which the exchange will be created.</param>
    /// <param name="configurator">The delegate to configure additional properties for the exchange creation, such as type and durability (optional).</param>
    /// <param name="cancellationToken">Token used to cancel the operation on the current thread (optional).</param>
    /// <returns>A task containing the result of the operation, including success or failure details.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> CreateExchange(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string exchange, string vhost,
        Action<ExchangeConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Exchange>(credentials)
            .Create(exchange, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a specified exchange within the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The API factory responsible for handling broker interactions.</param>
    /// <param name="credentials">The delegate used to provide user credentials for authentication.</param>
    /// <param name="exchange">The name of the exchange to be deleted.</param>
    /// <param name="vhost">The name of the virtual host under which the exchange resides.</param>
    /// <param name="configurator">Optional configuration behavior for the deletion process.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task containing the result of the operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> DeleteExchange(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string exchange, string vhost,
        Action<ExchangeDeletionConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Exchange>(credentials)
            .Delete(exchange, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Binds the specified exchange to another exchange within the given virtual host using the provided configuration options.
    /// </summary>
    /// <param name="factory">The API factory responsible for handling broker interactions.</param>
    /// <param name="credentials">The delegate to provide user credentials for authentication.</param>
    /// <param name="vhost">The virtual host where the exchange resides.</param>
    /// <param name="exchange">The name of the source exchange to bind to.</param>
    /// <param name="configurator">The delegate to configure the binding properties.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task containing the result of the binding operation, including the binding information created.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result<BindingInfo>> BindToExchange(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string vhost, string exchange,
        Action<BindingConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Exchange>(credentials)
            .BindToExchange(vhost, exchange, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Unbinds the current exchange from another exchange in the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The factory interface used to create and manage broker resources.</param>
    /// <param name="credentials">The delegate used to provide user credentials for authentication.</param>
    /// <param name="vhost">The name of the virtual host containing the exchange to be unbound.</param>
    /// <param name="configurator">The delegate that configures the unbinding operation, such as specifying the target exchange and routing key.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task containing the result of the unbinding operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> UnbindFromExchange(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string vhost, Action<UnbindingConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Exchange>(credentials)
            .Unbind(vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }
}