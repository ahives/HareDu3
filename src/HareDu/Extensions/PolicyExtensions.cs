namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class PolicyExtensions
    {
        public static async Task<Result> CreatePolicy(this IBrokerObjectFactory factory,
            string policy, string vhost, Action<NewPolicyConfigurator> configuration = null, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Policy>()
                .Create(policy, vhost, configuration, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<ResultList<PolicyInfo>> GetAllPolicies(this IBrokerObjectFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Policy>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> DeletePolicy(this IBrokerObjectFactory factory,
            string policy, string vhost, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Policy>()
                .Delete(policy, vhost, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}