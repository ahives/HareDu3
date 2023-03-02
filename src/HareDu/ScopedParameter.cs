namespace HareDu;

using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public interface ScopedParameter :
    BrokerAPI
{
    /// <summary>
    /// Returns all scoped parameters on the current RabbitMQ server.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{TResult}"/></returns>
    Task<ResultList<ScopedParameterInfo>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a scoped parameter for a particular RabbitMQ component and virtual host on the current server.
    /// </summary>
    /// <param name="name">Name of the RabbitMQ parameter.</param>
    /// <param name="value">Value of the RabbitMQ parameter.</param>
    /// <param name="component">Name of the RabbitMQ component.</param>
    /// <param name="vhost">Name of the RabbitMQ virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<Result> Create<T>(string name, T value, string component, string vhost, CancellationToken cancellationToken = default);
        
    /// <summary>
    /// Deletes the specified scoped parameter for a particular RabbitMQ component and virtual host on the current server.
    /// </summary>
    /// <param name="name">Name of the RabbitMQ parameter.</param>
    /// <param name="component">Name of the RabbitMQ component.</param>
    /// <param name="vhost">Name of the RabbitMQ virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{TResult}"/></returns>
    Task<Result> Delete(string name, string component, string vhost, CancellationToken cancellationToken = default);
}