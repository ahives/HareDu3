namespace HareDu
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface VirtualHostLimits :
        BrokerObject
    {
        /// <summary>
        /// Returns limit information about each virtual host on the current RabbitMQ server.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultList<VirtualHostLimitsInfo>> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// Defines specified limits on the virtual host on the current RabbitMQ server.
        /// </summary>
        /// <param name="configurator"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result> Define(string vhost, Action<VirtualHostLimitsConfigurator> configurator = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete the limits for the specified virtual host on the current RabbitMQ server.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result> Delete(string vhost, CancellationToken cancellationToken = default);
    }
}