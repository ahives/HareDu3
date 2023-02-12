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
    /// Returns various bits of random information that describe the RabbitMQ system.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result<BrokerOverviewInfo>> GetBrokerOverview(this IBrokerApiFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>()
            .GetOverview(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Rebalances all queues in all RabbitMQ virtual hosts.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result> RebalanceAllQueues(this IBrokerApiFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>()
            .RebalanceAllQueues(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Checks the RabbitMQ cluster to see if there are any alarms in effect.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result<AlarmState>> IsAlarmsInEffect(this IBrokerApiFactory factory, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>()
            .IsAlarmsInEffect(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Performs a health check on the specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result<BrokerState>> IsBrokerAlive(this IBrokerApiFactory factory,
        string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>()
            .IsBrokerAlive(vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Performs a health check on the RabbitMQ cluster to see if the virtual hosts are running.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result<VirtualHostState>> IsVirtualHostsRunning(this IBrokerApiFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>()
            .IsVirtualHostsRunning(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Performs a health check on the RabbitMQ cluster to determine if there are classic mirrored queues without synchronized mirrors online.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result<NodeMirrorSyncState>> IsNodeMirrorSyncCritical(this IBrokerApiFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>()
            .IsNodeMirrorSyncCritical(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Performs a health check on the RabbitMQ cluster to determine if there are quorum queues with minimum online quorum.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result<NodeQuorumState>> IsNodeQuorumCritical(this IBrokerApiFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>()
            .IsNodeQuorumCritical(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Checks if the given protocol is an active listener on the RabbitMQ cluster.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="configurator"></param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result<ProtocolListenerState>> IsProtocolActiveListener(this IBrokerApiFactory factory,
        Action<ProtocolListenerConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>()
            .IsProtocolAnActiveListener(configurator, cancellationToken)
            .ConfigureAwait(false);
    }
}