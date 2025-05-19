namespace HareDu;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

/// <summary>
/// Represents an abstraction to perform operations on RabbitMQ exchanges via the API.
/// </summary>
public interface Exchange :
    BrokerAPI
{
    /// <summary>
    /// Retrieves all exchanges from the RabbitMQ broker.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation, containing the results of all exchanges.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Results<ExchangeInfo>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates an exchange on the RabbitMQ broker.
    /// </summary>
    /// <param name="exchange">The name of the exchange to be created.</param>
    /// <param name="vhost">The virtual host where the exchange will be created.</param>
    /// <param name="configurator">An optional delegate to configure the exchange settings.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the exchange creation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the operation is canceled through the cancellation token.</exception>
    Task<Result> Create(string exchange, string vhost, Action<ExchangeConfigurator> configurator = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified exchange on the target RabbitMQ virtual host.
    /// </summary>
    /// <param name="exchange">Name of the RabbitMQ exchange to be deleted.</param>
    /// <param name="vhost">Name of the RabbitMQ virtual host.</param>
    /// <param name="configurator">Describes how the exchange will be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Result indicating the success or failure of the delete operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result> Delete(string exchange, string vhost, Action<ExchangeDeletionConfigurator> configurator = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Binds an exchange to another exchange within the specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="vhost">The RabbitMQ virtual host where the binding will take place.</param>
    /// <param name="exchange">The name of the target RabbitMQ exchange to bind to.</param>
    /// <param name="configurator">Defines the properties of the binding, such as the destination exchange and optional arguments.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Returns the result of the binding operation, including binding information if successful.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result<BindingInfo>> BindToExchange(string vhost, string exchange, Action<BindingConfigurator> configurator, CancellationToken cancellationToken = default);

    /// <summary>
    /// Unbinds a binding between an exchange and another resource in the specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="vhost">The name of the RabbitMQ virtual host where the exchange and binding exist.</param>
    /// <param name="configurator">Defines the binding details, such as the source and destination, to be unbound.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result indicating the success or failure of the unbind operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result> Unbind(string vhost, Action<UnbindingConfigurator> configurator, CancellationToken cancellationToken = default);
}