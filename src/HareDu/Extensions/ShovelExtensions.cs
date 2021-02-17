namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class ShovelExtensions
    {
        public static async Task<Result> CreateShovel(this IBrokerObjectFactory factory,
            string shovel, string vhost, Action<ShovelConfiguration> configuration = null, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));
            
            return await factory.Object<Shovel>()
                .Create(shovel, vhost, configuration, cancellationToken);
        }

        public static async Task<Result> DeleteShovel(this IBrokerObjectFactory factory,
            string shovel, string vhost, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));
            
            return await factory.Object<Shovel>()
                .Delete(shovel, vhost, cancellationToken);
        }

        public static async Task<ResultList<ShovelInfo>> GetAllShovels(this IBrokerObjectFactory factory)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));
            
            return await factory.Object<Shovel>()
                .GetAll();
        }
    }
}