namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class UserPermissionsExtensions
    {
        public static async Task<ResultList<UserPermissionsInfo>> GetAllUserPermissions(this IBrokerObjectFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<UserPermissions>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> CreateUserPermissions(this IBrokerObjectFactory factory,
            string username, string vhost, Action<UserPermissionsConfigurator> configurator, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            if (configurator.IsNull())
            {
                configurator = x =>
                {
                    x.UsingConfigurePattern(".*");
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                };
            }
            
            return await factory.Object<UserPermissions>()
                .Create(username, vhost, configurator, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> DeleteUserPermissions(this IBrokerObjectFactory factory, string username,
            string vhost, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<UserPermissions>()
                .Delete(username, vhost, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}