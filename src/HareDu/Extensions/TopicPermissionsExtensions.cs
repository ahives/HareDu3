namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class TopicPermissionsExtensions
    {
        public static async Task<ResultList<TopicPermissionsInfo>> GetAllTopicPermissions(
            this IBrokerObjectFactory factory, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<TopicPermissions>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> CreateTopicPermission(this IBrokerObjectFactory factory,
            string username, string exchange, string vhost, Action<TopicPermissionsConfigurator> configurator, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<TopicPermissions>()
                .Create(username, exchange, vhost, configurator, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> DeleteTopicPermission(this IBrokerObjectFactory factory,
            string username, string vhost, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<TopicPermissions>()
                .Delete(username, vhost, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}