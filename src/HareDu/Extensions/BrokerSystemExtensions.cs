namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class BrokerSystemExtensions
    {
        public static async Task<Result<SystemOverviewInfo>> GetBrokerSystemOverview(this IBrokerObjectFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<BrokerSystem>()
                .GetSystemOverview(cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> RebalanceAllQueues(this IBrokerObjectFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<BrokerSystem>()
                .RebalanceAllQueues(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}