namespace HareDu
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface Binding :
        BrokerObject
    {
        /// <summary>
        /// Returns all bindings on the current RabbitMQ node.
        /// </summary>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<ResultList<BindingInfo>> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates the specified binding between source (i.e. queue/exchange) and target (i.e. queue/exchange) on the target virtual host.
        /// </summary>
        /// <param name="action">Describes how the queue will be created.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<Result<BindingInfo>> Create(Action<BindingCreateAction> action, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete the specified exchange on the target virtual host.
        /// </summary>
        /// <param name="action">Describes how the queue will be deleted.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<Result> Delete(Action<BindingDeleteAction> action, CancellationToken cancellationToken = default);
    }
}