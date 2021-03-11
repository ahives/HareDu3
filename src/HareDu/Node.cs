namespace HareDu
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface Node :
        BrokerObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        Task<ResultList<NodeInfo>> GetAll(CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Runs a test to check if the current RabbitMQ node is healthy.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        Task<Result<NodeHealthInfo>> GetHealth(string node = null, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        Task<Result<NodeMemoryUsageInfo>> GetMemoryUsage(string node, CancellationToken cancellationToken = default);
    }
}