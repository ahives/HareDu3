namespace HareDu.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class ShovelExtensions
    {
        /// <summary>
        /// Create a dynamic shovel on a specified virtual host.
        /// </summary>
        /// <param name="factory">The object factory that implements the underlying functionality.</param>
        /// <param name="shovel">The name of the dynamic shovel.</param>
        /// <param name="vhost">The virtual host where the shovel resides.</param>
        /// <param name="configurator">Describes how the dynamic shovel will be created.</param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
        public static async Task<Result> CreateShovel(this IBrokerObjectFactory factory,
            string shovel, string vhost, Action<ShovelConfigurator> configurator = null, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Shovel>()
                .Create(shovel, vhost, configurator, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes a dynamic shovel on a specified virtual host.
        /// </summary>
        /// <param name="factory">The object factory that implements the underlying functionality.</param>
        /// <param name="shovel">The name of the dynamic shovel.</param>
        /// <param name="vhost">The virtual host where the dynamic shovel resides.</param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
        public static async Task<Result> DeleteShovel(this IBrokerObjectFactory factory,
            string shovel, string vhost, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));
            
            return await factory.Object<Shovel>()
                .Delete(shovel, vhost, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<IReadOnlyList<Result>> DeleteAllShovels(this IBrokerObjectFactory factory,
            string vhost, CancellationToken cancellationToken = default)
        {
            var result = await factory.Object<Shovel>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);

            var shovels = result
                .Select(x => x.Data)
                .Where(x => x.VirtualHost == vhost)
                .ToList();

            var results = new List<Result>();
            
            foreach (var shovel in shovels)
            {
                var deleteResult = await factory.Object<Shovel>()
                    .Delete(shovel.Name, vhost, cancellationToken)
                    .ConfigureAwait(false);
                
                results.Add(deleteResult);
            }

            return results;
        }
        
        /// <summary>
        /// Return all dynamic shovels that have been created.
        /// </summary>
        /// <param name="factory">The object factory that implements the underlying functionality.</param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
        public static async Task<ResultList<ShovelInfo>> GetAllShovels(this IBrokerObjectFactory factory, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));
            
            return await factory.Object<Shovel>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}