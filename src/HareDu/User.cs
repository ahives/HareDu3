namespace HareDu
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface User :
        BrokerObject
    {
        /// <summary>
        /// Returns information about all users on the current RabbitMQ server.
        /// </summary>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{TResult}"/></returns>
        Task<ResultList<UserInfo>> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns information about all users that do not have permissions on the current RabbitMQ server.
        /// </summary>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        Task<ResultList<UserInfo>> GetAllWithoutPermissions(CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a user on the current RabbitMQ server.
        /// </summary>
        /// <param name="username">RabbitMQ broker username.</param>
        /// <param name="password">RabbitMQ broker password.</param>
        /// <param name="passwordHash">RabbitMQ broker password hash.</param>
        /// <param name="configurator">Describes how the user permission will be created.</param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        Task<Result> Create(string username, string password, string passwordHash = null, Action<UserConfigurator> configurator = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete the specified user on the current RabbitMQ server.
        /// </summary>
        /// <param name="username">RabbitMQ broker username.</param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns></returns>
        Task<Result> Delete(string username, CancellationToken cancellationToken = default);
    }
}