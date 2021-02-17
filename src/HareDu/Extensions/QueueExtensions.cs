namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class QueueExtensions
    {
        public static async Task<ResultList<QueueInfo>> GetAllQueues(this IBrokerObjectFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Queue>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> CreateQueue(this IBrokerObjectFactory factory,
            string queue, string vhost, string node, Action<NewQueueConfiguration> configuration, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Queue>()
                .Create(queue, vhost, node, configuration, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> EmptyQueue(this IBrokerObjectFactory factory,
            string queue, string vhost, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Queue>()
                .Empty(queue, vhost, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<ResultList<PeekedMessageInfo>> PeekQueue(this IBrokerObjectFactory factory,
            string queue, string vhost, Action<PeekQueueConfiguration> configuration = null, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Queue>()
                .Peek(queue, vhost, configuration, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> DeleteQueue(this IBrokerObjectFactory factory,
            string queue, string vhost, Action<DeleteQueueConfiguration> configuration = null, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Queue>()
                .Delete(queue, vhost, configuration, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}