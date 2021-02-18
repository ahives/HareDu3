namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class VirtualHostLimitsExtensions
    {
        public static async Task<ResultList<VirtualHostLimitsInfo>> GetAllVirtualHostLimits(
            this IBrokerObjectFactory factory, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<VirtualHostLimits>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> DefineVirtualHostLimits(this IBrokerObjectFactory factory, string vhost,
            Action<VirtualHostLimitsConfigurator> configurator = null, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<VirtualHostLimits>()
                .Define(vhost, configurator, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> DeleteVirtualHostLimits(this IBrokerObjectFactory factory, string vhost,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<VirtualHostLimits>()
                .Delete(vhost, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}