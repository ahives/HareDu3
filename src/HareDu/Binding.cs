namespace HareDu;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public interface Binding :
    BrokerAPI
{
    /// <summary>
    /// Returns all bindings on the current RabbitMQ node.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
    Task<ResultList<BindingInfo>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates the specified binding between source (i.e. queue/exchange) and destination (i.e. queue/exchange) on the specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="vhost">The virtual host where the binding is defined.</param>
    /// <param name="configurator">Describes how the binding will be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result<BindingInfo>> Create(
        string vhost,
        Action<BindingConfigurator> configurator,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified exchange on the specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="vhost">The virtual host where the binding is defined.</param>
    /// <param name="configurator">Describes how the binding will be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Delete(
        string vhost,
        Action<DeleteBindingConfigurator> configurator,
        CancellationToken cancellationToken = default);
}