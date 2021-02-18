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
        /// <param name="configurator">Describes how the virtual host will be created.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns></returns>
        Task<Result> Create(string vhost, Action<VirtualHostConfigurator> configurator, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete the specified virtual host on the current RabbitMQ server.
        /// </summary>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns></returns>
        Task<Result> Delete(string vhost, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vhost"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result> Startup(string vhost, string node, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Perform a health check on a virtual host or node.
        /// </summary>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns></returns>
        Task<Result<ServerHealthInfo>> GetHealth(string vhost, CancellationToken cancellationToken = default);
    }
}