namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;

    public static class ConsumerExtensions
    {
        public static async Task<Result> GetAllConsumers(this IBrokerObjectFactory factory,
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