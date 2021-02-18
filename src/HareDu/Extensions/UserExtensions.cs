namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class UserExtensions
    {
        public static async Task<ResultList<UserInfo>> GetAllUsers(this IBrokerObjectFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<User>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<ResultList<UserInfo>> GetAllUsersWithoutPermissions(this IBrokerObjectFactory factory,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<User>()
                .GetAllWithoutPermissions(cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> CreateUser(this IBrokerObjectFactory factory,
            string username, string password, string passwordHash = null, Action<NewUserConfigurator> configurator = null, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<User>()
                .Create(username, password, passwordHash, configurator, cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<Result> DeleteUser(this IBrokerObjectFactory factory, string username,
            CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<User>()
                .Delete(username, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}