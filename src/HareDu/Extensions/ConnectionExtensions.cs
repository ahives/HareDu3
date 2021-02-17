namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class ConnectionExtensions
    {
        public static async Task<ResultList<ConnectionInfo>> GetAllConnections(this IBrokerObjectFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Connection>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}