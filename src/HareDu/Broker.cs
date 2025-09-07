namespace HareDu;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

/// <summary>
/// Interface that defines methods for managing RabbitMQ brokers, querying their state,
/// and performing related operations, such as queue rebalancing or checking the status of alarms.
/// </summary>
public interface Broker :
    HareDuAPI
{
    /// <summary>
    /// Retrieves an overview of the current RabbitMQ broker, including general system information.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result containing the broker overview information.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result<BrokerOverviewInfo>> GetOverview(CancellationToken cancellationToken = default);

    /// <summary>
    /// Initiates the rebalancing of all queues across the RabbitMQ cluster.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result indicating the success or failure of the rebalancing operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result> RebalanceQueues(CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks whether alarms are currently in effect on the RabbitMQ broker.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation before completion.</param>
    /// <returns>A result indicating the current state of alarms on the broker.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result<AlarmState>> IsAlarmsInEffect(CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks whether a RabbitMQ broker is alive and responsive for the specified virtual host.
    /// </summary>
    /// <param name="vhost">The virtual host to check the broker state for.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result containing the broker state (Alive, NotAlive, or NotRecognized).</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result<BrokerState>> IsBrokerAlive([NotNull] string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks the runtime status of all virtual hosts configured in the RabbitMQ broker.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result containing the current runtime state of the virtual hosts.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result<VirtualHostState>> IsVirtualHostsRunning(CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines if the RabbitMQ node is in a critical state due to unsynchronized mirrored queues.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result containing the synchronization state of the mirrored queues on the node.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result<NodeMirrorSyncState>> IsNodeMirrorSyncCritical(CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks whether the state of node quorum is critical in the RabbitMQ cluster.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result containing the state of node quorum.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result<NodeQuorumState>> IsNodeQuorumCritical(CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether the specified protocol has an active listener on the RabbitMQ broker.
    /// </summary>
    /// <param name="protocol">The protocol to be checked for active listening state.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result containing the active listener state of the specified protocol.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result<ProtocolListenerState>> IsProtocolActiveListener([NotNull] Protocol protocol, CancellationToken cancellationToken = default);
}