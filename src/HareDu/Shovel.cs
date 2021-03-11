namespace HareDu
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface Shovel :
        BrokerObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        Task<ResultList<ShovelInfo>> GetAll(CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shovel"></param>
        /// <param name="vhost">The virtual host where the shovel resides.</param>
        /// <param name="configurator"></param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        Task<Result> Create(string shovel, string vhost, Action<ShovelConfigurator> configurator, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shovel"></param>
        /// <param name="vhost">The virtual host where the shovel resides.</param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        Task<Result> Delete(string shovel, string vhost, CancellationToken cancellationToken = default);
    }
}