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
        /// Creates a scoped parameter for a particular component and virtual host on the current RabbitMQ server.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        /// <param name="component"></param>
        /// <param name="vhost"></param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<Result> Create<T>(string parameter, T value, string component, string vhost, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Delete the specified scoped parameter for a particular component and virtual host on the current RabbitMQ server.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="component"></param>
        /// <param name="vhost"></param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{TResult}"/></returns>
        Task<Result> Delete(string parameter, string component, string vhost, CancellationToken cancellationToken = default);
    }
}