namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class BindingExtensions
    {
        public static async Task<ResultList<BindingInfo>> GetAllBindings(this IBrokerObjectFactory factory, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Binding>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> CreateExchangeBindingToQueue(this IBrokerObjectFactory factory,
            string sourceBinding, string destinationBinding, string vhost, Action<NewBindingConfigurator> configuration = null, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Binding>()
                .Create(sourceBinding, destinationBinding, BindingType.Queue, vhost, configuration, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> CreateExchangeBinding(this IBrokerObjectFactory factory,
            string sourceBinding, string destinationBinding, string vhost, Action<NewBindingConfigurator> configuration = null, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Binding>()
                .Create(sourceBinding, destinationBinding, BindingType.Exchange, vhost, configuration, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> DeleteQueueBinding(this IBrokerObjectFactory factory,
            string sourceBinding, string destinationBinding, string propertiesKey, string vhost, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Binding>()
                .Delete(sourceBinding, destinationBinding, propertiesKey, vhost, BindingType.Queue, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> DeleteExchangeBinding(this IBrokerObjectFactory factory,
            string sourceBinding, string destinationBinding, string propertiesKey, string vhost, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Binding>()
                .Delete(sourceBinding, destinationBinding, propertiesKey, vhost, BindingType.Exchange, cancellationToken)
                .ConfigureAwait(false);
        }
        
        public static Task<ResultList<BindingInfo>> ScreenDump(this Task<ResultList<BindingInfo>> result)
        {
            var results = result
                .GetResult()
                .Select(x => x.Data);
            
            foreach (var item in results)
            {
                Console.WriteLine($"Virtual Host: {item.VirtualHost}");
                Console.WriteLine($"Source: {item.Source}");
                Console.WriteLine($"Destination: {item.Destination}");
                Console.WriteLine($"Destination Type: {item.DestinationType}");
                Console.WriteLine($"Routing Key: {item.RoutingKey}");
                Console.WriteLine($"Properties Key: {item.PropertiesKey}");
                Console.WriteLine();
            }

            return result;
        }
    }
}