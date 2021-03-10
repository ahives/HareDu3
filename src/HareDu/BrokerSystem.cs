namespace HareDu
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface BrokerSystem :
        BrokerObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken">Token used to cancel the current thread</param>
        /// <returns></returns>
        Task<Result<SystemOverviewInfo>> GetSystemOverview(CancellationToken cancellationToken = default);

        /// <summary>
        /// Rebalances all queues in all virtual hosts.
        /// </summary>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns></returns>
        Task<Result> RebalanceAllQueues(CancellationToken cancellationToken = default);
    }
}