namespace HareDu
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Model;

    public interface User
    {
        /// <summary>
        /// Returns information about all users on the current RabbitMQ server.
        /// </summary>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{TResult}"/></returns>
        Task<ResultList<UserInfo>> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns information about all users that do not have permissions on the current RabbitMQ server.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultList<UserInfo>> GetAllWithoutPermissions(CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a user on the current RabbitMQ server.
        /// </summary>
        /// <param name="action">Describes how the user permission will be created.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{TResult}"/></returns>
        Task<Result> Create(Action<UserCreateAction> action, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete the specified user on the current RabbitMQ server.
        /// </summary>
        /// <param name="action">Describes how the virtual host will be delete.</param>
        /// <param name="cancellationToken">Token used cancel the current thread</param>
        /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{TResult}"/></returns>
        Task<Result> Delete(Action<UserDeleteAction> action, CancellationToken cancellationToken = default);
    }
}