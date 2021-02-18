namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class NodeExtensions
    {
        public static async Task<ResultList<NodeInfo>> GetAllNodes(this IBrokerObjectFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Node>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result<NodeHealthInfo>> GetNodeHealth(this IBrokerObjectFactory factory,
            string node = null, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Node>()
                .GetHealth(node, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result<NodeMemoryUsageInfo>> GetNodeMemoryUsage(this IBrokerObjectFactory factory,
            string node, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Node>()
                .GetMemoryUsage(node, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}