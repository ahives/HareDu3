namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class ChannelExtensions
    {
        public static async Task<ResultList<ChannelInfo>> GetAllChannels(this IBrokerObjectFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Channel>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}