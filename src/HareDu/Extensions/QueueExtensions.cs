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
            string queue, string vhost, string node, Action<NewQueueConfigurator> configuration, CancellationToken cancellationToken = default)
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
            string queue, string vhost, Action<PeekQueueConfigurator> configurator = null, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            if (configurator.IsNull())
            {
                configurator = x =>
                {
                    x.Take(1);
                    x.Encoding(MessageEncoding.Auto);
                    x.AckMode(RequeueMode.AckRequeue);
                };
            }
            
            return await factory.Object<Queue>()
                .Peek(queue, vhost, configurator, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> DeleteQueue(this IBrokerObjectFactory factory,
            string queue, string vhost, Action<DeleteQueueConfigurator> configuration = null, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Queue>()
                .Delete(queue, vhost, configuration, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}