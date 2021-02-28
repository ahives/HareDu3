namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class GlobalParameterExtensions
    {
        public static async Task<ResultList<GlobalParameterInfo>> GetAllGlobalParameters(
            this IBrokerObjectFactory factory, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<GlobalParameter>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> CreateGlobalParameter(this IBrokerObjectFactory factory,
            string parameter, Action<GlobalParameterConfigurator> configurator, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<GlobalParameter>()
                .Create(parameter, configurator, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> DeleteGlobalParameter(this IBrokerObjectFactory factory,
            string parameter, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<GlobalParameter>()
                .Delete(parameter, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}