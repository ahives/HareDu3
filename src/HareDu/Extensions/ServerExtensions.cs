namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class ServerExtensions
    {
        public static async Task<Result<ServerInfo>> GetServerInformation(this IBrokerObjectFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Server>()
                .Get(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}