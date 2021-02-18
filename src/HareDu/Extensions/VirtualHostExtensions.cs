namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class VirtualHostExtensions
    {
        public static async Task<ResultList<VirtualHostInfo>> GetAllVirtualHosts(this IBrokerObjectFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<VirtualHost>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> CreateVirtualHost(this IBrokerObjectFactory factory,
            string vhost, Action<VirtualHostConfigurator> configurator, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<VirtualHost>()
                .Create(vhost, configurator, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> DeleteVirtualHost(this IBrokerObjectFactory factory,
            string vhost, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<VirtualHost>()
                .Delete(vhost, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> StartupVirtualHost(this IBrokerObjectFactory factory,
            string vhost, string node, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<VirtualHost>()
                .Startup(vhost, node, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result<ServerHealthInfo>> GetVirtualHostHealth(this IBrokerObjectFactory factory,
            string vhost, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<VirtualHost>()
                .GetHealth(vhost, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}