namespace HareDu.Snapshotting.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Security;
using Model;

public static class SnapshotExtensions
{
    /// <summary>
    /// Captures the state of all RabbitMQ queues in the cluster into a <see cref="BrokerQueuesSnapshot"/> object, represented within a <see cref="SnapshotResult{T}"/>.
    /// </summary>
    /// <param name="factory">The snapshot factory used to create and manage lenses for capturing the queue snapshot.</param>
    /// <param name="credentials">The credential provider which specifies the authentication details for accessing the RabbitMQ cluster.</param>
    /// <param name="cancellationToken">Token used to signal cancellation of the snapshot operation.</param>
    /// <returns>A <see cref="SnapshotResult{T}"/> containing the queue snapshot and associated metadata.</returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if the provided <paramref name="factory"/> is null.</exception>
    public static async Task<SnapshotResult<BrokerQueuesSnapshot>> TakeQueueSnapshot(this ISnapshotFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        if (factory is null)
            throw new ArgumentNullException(nameof(factory));

        return await factory
            .Lens<BrokerQueuesSnapshot>()
            .TakeSnapshot(credentials, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Captures the combined state of the RabbitMQ cluster into a <see cref="ClusterSnapshot"/> object represented within a <see cref="SnapshotResult{T}"/>.
    /// </summary>
    /// <param name="factory">The snapshot factory used to manage lenses and capture snapshots.</param>
    /// <param name="credentials">The credential provider which specifies the authentication details for accessing the RabbitMQ cluster.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A <see cref="SnapshotResult{T}"/> containing the cluster snapshot and associated metadata.</returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if the provided <paramref name="factory"/> is null.</exception>
    public static async Task<SnapshotResult<ClusterSnapshot>> TakeClusterSnapshot(this ISnapshotFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        if (factory is null)
            throw new ArgumentNullException(nameof(factory));

        return await factory
            .Lens<ClusterSnapshot>()
            .TakeSnapshot(credentials, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves information regarding the current connectivity state of the RabbitMQ broker, combining data into a <see cref="BrokerConnectivitySnapshot"/>.
    /// </summary>
    /// <param name="factory">The snapshot factory used to create snapshots for different components.</param>
    /// <param name="credentials">The credential provider used to define the credentials for RabbitMQ connections.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the result of the snapshot operation containing a <see cref="BrokerConnectivitySnapshot"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if the factory is null.</exception>
    public static async Task<SnapshotResult<BrokerConnectivitySnapshot>> TakeConnectivitySnapshot(
        this ISnapshotFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        if (factory is null)
            throw new ArgumentNullException(nameof(factory));

        return await factory
            .Lens<BrokerConnectivitySnapshot>()
            .TakeSnapshot(credentials, cancellationToken)
            .ConfigureAwait(false);
    }
}