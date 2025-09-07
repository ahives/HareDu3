namespace HareDu.Extensions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Security;
using Model;

public static class BrokerExtensions
{
    /// <summary>
    /// Retrieves an overview of the broker system.
    /// </summary>
    /// <param name="factory">The factory responsible for creating broker-related operations.</param>
    /// <param name="credentials">The credentials used to authenticate with the broker system.</param>
    /// <param name="cancellationToken">A token used to cancel the operation if needed.</param>
    /// <returns>A task that represents the asynchronous operation, with the result containing an overview of the broker system.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result<BrokerOverviewInfo>> GetBrokerOverview(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>(credentials)
            .GetOverview(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Initiates a rebalance operation for all queues in the broker.
    /// </summary>
    /// <param name="factory">The factory responsible for creating broker-related operations.</param>
    /// <param name="credentials">The credentials used to authenticate with the broker system.</param>
    /// <param name="cancellationToken">A token used to cancel the operation if needed.</param>
    /// <returns>A task that represents the asynchronous operation, with the result indicating the success or failure of the operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result> RebalanceQueues(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>(credentials)
            .RebalanceQueues(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Checks if alarm notifications are in effect for the broker system.
    /// </summary>
    /// <param name="factory">The factory responsible for initiating broker-related operations.</param>
    /// <param name="credentials">The credentials used for authenticating with the broker system.</param>
    /// <param name="cancellationToken">A token used to cancel the operation if needed.</param>
    /// <returns>A task representing the asynchronous operation, with the result containing the current alarm state of the broker system.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result<AlarmState>> IsAlarmsInEffect(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>(credentials)
            .IsAlarmsInEffect(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Checks if the broker is alive for the specified virtual host.
    /// </summary>
    /// <param name="factory">The factory responsible for creating broker-related operations.</param>
    /// <param name="credentials">The credentials used to authenticate with the broker system.</param>
    /// <param name="vhost">The virtual host to be checked for broker availability.</param>
    /// <param name="cancellationToken">A token used to cancel the operation if needed.</param>
    /// <returns>A task representing the asynchronous operation, with the result indicating the broker state (Alive, NotAlive, NotRecognized).</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the operation is canceled.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result<BrokerState>> IsBrokerAlive(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] string vhost,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>(credentials)
            .IsBrokerAlive(vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Determines whether all virtual hosts associated with the broker are in a running state.
    /// </summary>
    /// <param name="factory">The factory used to create broker-related operations.</param>
    /// <param name="credentials">The credentials used to authenticate with the broker system.</param>
    /// <param name="cancellationToken">A token used to cancel the operation if necessary.</param>
    /// <returns>A task representing the asynchronous operation, with a result indicating the running state of the virtual hosts.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result<VirtualHostState>> IsVirtualHostsRunning(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>(credentials)
            .IsVirtualHostsRunning(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Determines whether the node is in a mirror sync critical state.
    /// </summary>
    /// <param name="factory">The factory responsible for creating broker-related operations.</param>
    /// <param name="credentials">The credentials used to authenticate with the broker system.</param>
    /// <param name="cancellationToken">A token used to cancel the operation if needed.</param>
    /// <returns>A task that represents the asynchronous operation, with the result indicating the mirror sync state of the node.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result<NodeMirrorSyncState>> IsNodeMirrorSyncCritical(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>(credentials)
            .IsNodeMirrorSyncCritical(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Determines whether the node quorum state is critical.
    /// </summary>
    /// <param name="factory">The factory responsible for creating broker-related operations.</param>
    /// <param name="credentials">The credentials used to authenticate with the broker system.</param>
    /// <param name="cancellationToken">A token used to cancel the operation if needed.</param>
    /// <returns>A task that represents the asynchronous operation, with the result providing the state of the node quorum.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result<NodeQuorumState>> IsNodeQuorumCritical(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>(credentials)
            .IsNodeQuorumCritical(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Checks if the specified protocol has an active listener within the broker system.
    /// </summary>
    /// <param name="factory">The factory responsible for creating broker-related operations.</param>
    /// <param name="credentials">The credentials used to authenticate with the broker system.</param>
    /// <param name="protocol">The protocol to verify for an active listener.</param>
    /// <param name="cancellationToken">A token used to cancel the operation if necessary.</param>
    /// <returns>A task that represents the asynchronous operation, with the result containing the state of the protocol listener.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result<ProtocolListenerState>> IsProtocolActiveListener(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] Protocol protocol, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Broker>(credentials)
            .IsProtocolActiveListener(protocol, cancellationToken)
            .ConfigureAwait(false);
    }
}