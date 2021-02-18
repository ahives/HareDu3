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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultList<ShovelInfo>> GetAll(CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shovel"></param>
        /// <param name="vhost"></param>
        /// <param name="configurator"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result> Create(string shovel, string vhost, Action<ShovelConfigurator> configurator, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shovel"></param>
        /// <param name="vhost"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result> Delete(string shovel, string vhost, CancellationToken cancellationToken = default);
    }
}