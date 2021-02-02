namespace HareDu
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Internal;

    public interface Policy :
        BrokerObject
    {
        /// <summary>
        /// Returns all policies on the current RabbitMQ node.
        /// </summary>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<ResultList<PolicyInfo>> GetAll(CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Creates the specified policy on the target virtual host.
        /// </summary>
        /// <param name="action">Describes how the policy will be created.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<Result> Create(Action<PolicyCreateAction> action, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Delete the specified policy on the target virtual host.
        /// </summary>
        /// <param name="action">Describes how the policy will be deleted.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<Result> Delete(Action<PolicyDeleteAction> action, CancellationToken cancellationToken = default);
    }
}