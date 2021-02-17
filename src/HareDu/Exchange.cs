namespace HareDu
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface Exchange :
        BrokerObject
    {
        /// <summary>
        /// Returns all exchanges on the current RabbitMQ node.
        /// </summary>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<ResultList<ExchangeInfo>> GetAll(CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Creates the specified exchange on the target virtual host.
        /// </summary>
        /// <param name="configurator">Describes how the queue will be created.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<Result> Create(string exchange, string vhost, Action<NewExchangeConfigurator> configurator = null, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Delete the specified exchange on the target virtual host.
        /// </summary>
        /// <param name="configurator">Describes how the queue will be deleted.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
        Task<Result> Delete(string exchange, string vhost, Action<DeleteExchangeConfigurator> configurator = null, CancellationToken cancellationToken = default);
    }
}