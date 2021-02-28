namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class ExchangeExtensions
    {
        public static async Task<ResultList<ExchangeInfo>> GetAllExchanges(this IBrokerObjectFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Exchange>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> CreateExchange(this IBrokerObjectFactory factory,
            string exchange, string vhost, Action<ExchangeConfigurator> configurator = null, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Exchange>()
                .Create(exchange, vhost, configurator, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> DeleteExchange(this IBrokerObjectFactory factory,
            string exchange, string vhost, Action<ExchangeDeletionConfigurator> configurator = null, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Exchange>()
                .Delete(exchange, vhost, configurator, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}