namespace HareDu.Snapshotting.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Security;
using Model;

public static class SnapshotExtensions
{
    /// <summary>
    /// Returns combined information from several API calls to the RabbitMQ cluster into <see cref="BrokerQueuesSnapshot"/> object accessible through <see cref="History"/>.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
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
    /// Returns combined information from several API calls to the RabbitMQ cluster into <see cref="ClusterSnapshot"/> object accessible through <see cref="History"/>.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
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
    /// Returns combined information from several API calls to the RabbitMQ cluster into <see cref="BrokerConnectivitySnapshot"/> object accessible through <see cref="History"/>.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<SnapshotResult<BrokerConnectivitySnapshot>> TakeConnectivitySnapshot(this ISnapshotFactory factory,
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