namespace HareDu
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface GlobalParameter :
        BrokerObject
    {
        /// <summary>
        /// Returns all global parameters on the current RabbitMQ node.
        /// </summary>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<ResultList<GlobalParameterInfo>> GetAll(CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Creates the specified global parameter on the current RabbitMQ node.
        /// </summary>
        /// <param name="configuration">Describes how the global parameter will be created.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<Result> Create(Action<NewGlobalParameterConfiguration> configuration, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Delete the specified global parqmeter on the current RabbitMQ node.
        /// </summary>
        /// <param name="configuration">Describes how the global parameter will be deleted.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<Result> Delete(Action<DeleteGlobalParameterConfiguration> configuration, CancellationToken cancellationToken = default);
    }
}