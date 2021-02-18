namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class SystemOverviewExtensions
    {
        public static async Task<Result<SystemOverviewInfo>> GetSystemOverview(this IBrokerObjectFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<SystemOverview>()
                .Get(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}