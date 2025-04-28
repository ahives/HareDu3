namespace HareDu;

using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

/// <summary>
/// An interface for managing scoped parameters on a RabbitMQ server, providing functionality for retrieval, creation, and deletion.
/// </summary>
public interface ScopedParameter :
    BrokerAPI
{
    /// <summary>
    /// Returns all scoped parameters on the current RabbitMQ server.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{TResult}"/></returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Results<ScopedParameterInfo>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a scoped parameter on the RabbitMQ server with the specified properties.
    /// </summary>
    /// <typeparam name="T">The data type of the parameter value.</typeparam>
    /// <param name="name">The name of the scoped parameter being created.</param>
    /// <param name="value">The value assigned to the scoped parameter.</param>
    /// <param name="component">The component to which the scoped parameter belongs.</param>
    /// <param name="vhost">The virtual host where the scoped parameter will reside.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous operation result of type <see cref="HareDu.Core.Result"/>.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result> Create<T>(string name, T value, string component, string vhost,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified scoped parameter for a particular RabbitMQ component and virtual host on the current server.
    /// </summary>
    /// <param name="name">Name of the RabbitMQ parameter.</param>
    /// <param name="component">Name of the RabbitMQ component.</param>
    /// <param name="vhost">Name of the RabbitMQ virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{TResult}"/></returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result> Delete(string name, string component, string vhost, CancellationToken cancellationToken = default);
}