namespace HareDu
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface VirtualHost :
        BrokerObject
    {
        /// <summary>
        /// Returns information about each virtual host on the current RabbitMQ server.
        /// </summary>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns></returns>
        Task<ResultList<VirtualHostInfo>> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates the specified virtual host on the current RabbitMQ server.
        /// </summary>
        /// <param name="action">Describes how the virtual host will be created.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns></returns>
        Task<Result> Create(Action<VirtualHostCreateAction> action, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete the specified virtual host on the current RabbitMQ server.
        /// </summary>
        /// <param name="action">Describes how the virtual host will be delete.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns></returns>
        Task<Result> Delete(Action<VirtualHostDeleteAction> action, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vhost"></param>
        /// <param name="action"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result> Startup(string vhost, Action<VirtualHostStartupAction> action, CancellationToken cancellationToken = default);
    }
}