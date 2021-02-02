namespace HareDu
{
    using System;
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
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{TResult}"/></returns>
        Task<ResultList<ScopedParameterInfo>> GetAll(CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Creates a scoped parameter for a particular component and virtual host on the current RabbitMQ server.
        /// </summary>
        /// <param name="action">Describes how the scoped parameter will be created.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{TResult}"/></returns>
        Task<Result> Create<T>(Action<ScopedParameterCreateAction<T>> action, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Delete the specified scoped parameter for a particular component and virtual host on the current RabbitMQ server.
        /// </summary>
        /// <param name="action">Describes how the scoped parameter will be delete.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{TResult}"/></returns>
        Task<Result> Delete(Action<ScopedParameterDeleteAction> action, CancellationToken cancellationToken = default);
    }
}