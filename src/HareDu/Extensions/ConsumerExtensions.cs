namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class ConsumerExtensions
    {
        public static async Task<ResultList<ConsumerInfo>> GetAllConsumers(this IBrokerObjectFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Consumer>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}