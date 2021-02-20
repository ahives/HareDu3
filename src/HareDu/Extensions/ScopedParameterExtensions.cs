namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class ScopedParameterExtensions
    {
        public static async Task<ResultList<ScopedParameterInfo>> GetAllScopedParameters(this IBrokerObjectFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<ScopedParameter>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> CreateScopeParameter<T>(this IBrokerObjectFactory factory,
            string parameter, T value, string component, string vhost, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<ScopedParameter>()
                .Create(parameter, value, component, vhost, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> DeleteScopedParameter(this IBrokerObjectFactory factory,
            string parameter, string component, string vhost, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<ScopedParameter>()
                .Delete(parameter, component, vhost, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}