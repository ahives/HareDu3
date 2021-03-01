namespace HareDu.Snapshotting
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Extensions;
    using Model;

    public static class SnapshotExtensions
    {
        public static async Task<SnapshotResult<BrokerQueuesSnapshot>> TakeQueueSnapshot(this ISnapshotFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Lens<BrokerQueuesSnapshot>()
                .TakeSnapshot(cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<SnapshotResult<ClusterSnapshot>> TakeClusterSnapshot(this ISnapshotFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Lens<ClusterSnapshot>()
                .TakeSnapshot(cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<SnapshotResult<BrokerConnectivitySnapshot>> TakeConnectivitySnapshot(
            this ISnapshotFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Lens<BrokerConnectivitySnapshot>()
                .TakeSnapshot(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}