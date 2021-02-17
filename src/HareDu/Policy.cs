namespace HareDu
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

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
        /// <param name="configuration">Describes how the policy will be created.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<Result> Create(string policy, string vhost, Action<NewPolicyConfiguration> configuration = null, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Delete the specified policy on the target virtual host.
        /// </summary>
        /// <param name="configuration">Describes how the policy will be deleted.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<Result> Delete(string policy, string vhost, CancellationToken cancellationToken = default);
    }
}