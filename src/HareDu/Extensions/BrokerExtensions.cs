namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Configuration;
using Model;

public static class BrokerExtensions
{
    /// <summary>
    /// Retrieves an overview of the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The IBrokerFactory instance used to interact with the broker.</param>
    /// <param name="credentials">An action to configure the credential provider for authentication.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the asynchronous operation result, containing broker overview information.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<BrokerOverviewInfo>> GetBrokerOverview(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<Broker>(credentials)
            .GetOverview(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Rebalances all queues across all RabbitMQ virtual hosts.
    /// </summary>
    /// <param name="factory">The IBrokerFactory instance used to interact with the broker.</param>
    /// <param name="credentials">An action to configure the credential provider for authentication.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the asynchronous operation result of the rebalance action.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result> RebalanceAllQueues(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<Broker>(credentials)
            .RebalanceAllQueues(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Checks whether alarms are currently in effect on the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The IBrokerFactory instance used to interact with the broker.</param>
    /// <param name="credentials">An action to configure the credential provider for authentication.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the asynchronous operation result, containing the alarm state.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<AlarmState>> IsAlarmsInEffect(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<Broker>(credentials)
            .IsAlarmsInEffect(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Checks if the RabbitMQ broker is alive for the specified virtual host.
    /// </summary>
    /// <param name="factory">The IBrokerFactory instance used to interact with the broker.</param>
    /// <param name="credentials">An action to configure the credential provider for authentication.</param>
    /// <param name="vhost">The name of the virtual host for which the broker's status is being verified.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the asynchronous operation result, containing the broker's state for the given virtual host.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<BrokerState>> IsBrokerAlive(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<Broker>(credentials)
            .IsBrokerAlive(vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Checks whether all virtual hosts in the RabbitMQ broker are running.
    /// </summary>
    /// <param name="factory">The IBrokerFactory instance used to interact with the broker.</param>
    /// <param name="credentials">An action to configure the credential provider used for authentication.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the asynchronous operation result, containing the state of the virtual hosts.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<VirtualHostState>> IsVirtualHostsRunning(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<Broker>(credentials)
            .IsVirtualHostsRunning(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Determines whether any node is in a critical state with respect to mirror sync.
    /// </summary>
    /// <param name="factory">The IBrokerFactory instance used to interact with the broker.</param>
    /// <param name="credentials">An action to configure the credential provider for authentication.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>
    /// A task representing the asynchronous operation result, containing the state of mirror sync for nodes as a <see cref="NodeMirrorSyncState"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<NodeMirrorSyncState>> IsNodeMirrorSyncCritical(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<Broker>(credentials)
            .IsNodeMirrorSyncCritical(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Determines whether the quorum status of the RabbitMQ node is critical.
    /// </summary>
    /// <param name="factory">The IBrokerFactory instance used for interacting with the broker API.</param>
    /// <param name="credentials">An action to configure the credential provider for authentication to the broker.</param>
    /// <param name="cancellationToken">Token used to cancel the current operation if required.</param>
    /// <returns>A task representing the asynchronous operation result, containing the quorum state of the node.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<NodeQuorumState>> IsNodeQuorumCritical(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<Broker>(credentials)
            .IsNodeQuorumCritical(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Checks the state of a specific protocol's listener to determine if it is active on the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The IBrokerFactory instance used to interact with the broker.</param>
    /// <param name="credentials">An action to configure the credential provider for authentication.</param>
    /// <param name="protocol">The protocol to check for an active listener.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the asynchronous operation result, containing the state of the protocol listener.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<ProtocolListenerState>> IsProtocolActiveListener(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, Protocol protocol, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<Broker>(credentials)
            .IsProtocolActiveListener(protocol, cancellationToken)
            .ConfigureAwait(false);
    }
}