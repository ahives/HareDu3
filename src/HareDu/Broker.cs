namespace HareDu;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public interface Broker :
    BrokerAPI
{
    /// <summary>
    /// Returns various bits of random information that describe the RabbitMQ system.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result<BrokerOverviewInfo>> GetOverview(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rebalances all queues in all RabbitMQ virtual hosts.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> RebalanceAllQueues(CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks the RabbitMQ cluster to see if there are any alarms in effect.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result<AlarmState>> IsAlarmsInEffect(CancellationToken cancellationToken = default);

    /// <summary>
    /// Performs a health check on the specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result<BrokerState>> IsBrokerAlive(string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Performs a health check on the RabbitMQ cluster to see if the virtual hosts are running.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result<VirtualHostState>> IsVirtualHostsRunning(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Performs a health check on the RabbitMQ cluster to determine if there are classic mirrored queues without synchronized mirrors online.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result<NodeMirrorSyncState>> IsNodeMirrorSyncCritical(CancellationToken cancellationToken = default);

    /// <summary>
    /// Performs a health check on the RabbitMQ cluster to determine if there are quorum queues with minimum online quorum.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result<NodeQuorumState>> IsNodeQuorumCritical(CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configurator"></param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result<ProtocolListenerState>> IsProtocolActiveListener(Action<ProtocolListenerConfigurator> configurator, CancellationToken cancellationToken = default);
}