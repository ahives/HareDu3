namespace HareDu
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface ScopedParameter :
        BrokerObject
    {
        /// <summary>
        /// Returns all scoped parameters on the current RabbitMQ server.
        /// </summary>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{TResult}"/></returns>
        Task<ResultList<ScopedParameterInfo>> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a scoped parameter for a particular RabbitMQ component and virtual host on the current server.
        /// </summary>
        /// <param name="parameter">Name of the RabbitMQ parameter.</param>
        /// <param name="value">Value of the RabbitMQ parameter.</param>
        /// <param name="component">Name of the RabbitMQ component.</param>
        /// <param name="vhost">Name of the RabbitMQ virtual host.</param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<Result> Create<T>(string parameter, T value, string component, string vhost, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Delete the specified scoped parameter for a particular RabbitMQ component and virtual host on the current server.
        /// </summary>
        /// <param name="parameter">Name of the RabbitMQ parameter.</param>
        /// <param name="component">Name of the RabbitMQ component.</param>
        /// <param name="vhost">Name of the RabbitMQ virtual host.</param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{TResult}"/></returns>
        Task<Result> Delete(string parameter, string component, string vhost, CancellationToken cancellationToken = default);
    }
}