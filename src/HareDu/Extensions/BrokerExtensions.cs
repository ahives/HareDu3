namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class BrokerExtensions
{
    /// <summary>
    /// Retrieves the RabbitMQ broker overview information.
    /// </summary>
    /// <param name="factory">The API provider which implements the RabbitMQ functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task containing the result with detailed broker overview information.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<BrokerOverviewInfo>> GetBrokerOverview(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>()
            .GetOverview(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Rebalances all queues across all RabbitMQ virtual hosts.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the asynchronous operation result.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result> RebalanceAllQueues(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>()
            .RebalanceAllQueues(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Determines whether alarms are in effect for the RabbitMQ system.
    /// </summary>
    /// <param name="factory">An implementation of the IBrokerFactory interface used to interact with the RabbitMQ system.</param>
    /// <param name="cancellationToken">Token used to cancel the operation if necessary.</param>
    /// <returns>Returns a result containing the current state of alarms in the RabbitMQ system.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<AlarmState>> IsAlarmsInEffect(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>()
            .IsAlarmsInEffect(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Determines whether the broker is alive and responsive within the specified virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality of the broker.</param>
    /// <param name="vhost">The virtual host to verify the broker's state.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result containing the state of the broker as <see cref="BrokerState"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<BrokerState>> IsBrokerAlive(this IBrokerFactory factory,
        string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>()
            .IsBrokerAlive(vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Performs a health check on all RabbitMQ virtual hosts.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<VirtualHostState>> IsVirtualHostsRunning(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>()
            .IsVirtualHostsRunning(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Determines whether the RabbitMQ node is in a mirror synchronization critical state.
    /// </summary>
    /// <param name="factory">The API factory that communicates with the RabbitMQ broker.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>
    /// A result containing the state of the RabbitMQ node in relation to mirror synchronization,
    /// represented by <see cref="NodeMirrorSyncState"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<NodeMirrorSyncState>> IsNodeMirrorSyncCritical(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>()
            .IsNodeMirrorSyncCritical(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Determines whether the node related to the RabbitMQ cluster is in a critical quorum state.
    /// </summary>
    /// <param name="factory">The API that provides the implementation to interact with the RabbitMQ system.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Returns the state of the node's quorum, indicating whether it is critical, below minimum, or not recognized.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<NodeQuorumState>> IsNodeQuorumCritical(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>()
            .IsNodeQuorumCritical(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Determines whether a protocol is an active listener on the RabbitMQ system.
    /// </summary>
    /// <param name="factory">The API that implements the underlying broker system functionality.</param>
    /// <param name="protocol">The protocol whose listener status is being queried (e.g., AMQP091, MQTT, STOMP).</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result containing the state of the protocol listener (Active, NotActive, or NotRecognized).</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<ProtocolListenerState>> IsProtocolActiveListener(this IBrokerFactory factory,
        Protocol protocol, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>()
            .IsProtocolActiveListener(protocol, cancellationToken)
            .ConfigureAwait(false);
    }
}