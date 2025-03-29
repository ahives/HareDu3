namespace HareDu;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public interface User :
    BrokerAPI
{
    /// <summary>
    /// Returns information about all users on the current RabbitMQ server.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{TResult}"/></returns>
    Task<Results<UserInfo>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns information about all users that do not have permissions on the current RabbitMQ server.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Results<UserInfo>> GetAllWithoutPermissions(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a user on the current RabbitMQ server.
    /// </summary>
    /// <param name="username">RabbitMQ broker username.</param>
    /// <param name="password">RabbitMQ broker password.</param>
    /// <param name="passwordHash">RabbitMQ broker password hash.</param>
    /// <param name="configurator">Describes how the user permission will be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Create(string username, string password, string passwordHash = null,
        Action<UserConfigurator> configurator = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified user on the current RabbitMQ server.
    /// </summary>
    /// <param name="username">RabbitMQ broker username.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Delete(string username, CancellationToken cancellationToken = default);
        
    /// <summary>
    /// Bulk deletes the specified user on the current RabbitMQ server.
    /// </summary>
    /// <param name="usernames">List of RabbitMQ broker usernames.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> BulkDelete(IList<string> usernames, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns information about user limits for the specified RabbitMQ user.
    /// </summary>
    /// <param name="username">RabbitMQ broker username.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{UserLimitsInfo}"/></returns>
    Task<Results<UserLimitsInfo>> GetLimitsByUser(string username, CancellationToken cancellationToken = default);

    Task<Results<UserLimitsInfo>> GetAllUserLimits(CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a limit for the specified RabbitMQ user.
    /// </summary>
    /// <param name="username">RabbitMQ broker username.</param>
    /// <param name="configurator">Describes what limit will be created for the user.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> DefineLimit(string username, Action<UserLimitConfigurator> configurator = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a limit for the specified RabbitMQ user.
    /// </summary>
    /// <param name="username">RabbitMQ broker username.</param>
    /// <param name="limit">User limit (e.g., max connections, max channels, etc.)</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> DeleteLimit(string username, UserLimit limit, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Returns information about all user permissions on the current RabbitMQ server.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Results<UserPermissionsInfo>> GetAllPermissions(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a user permission and assign it to a user on a specific virtual host on the current RabbitMQ server.
    /// </summary>
    /// <param name="username">RabbitMQ broker username.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="configurator">Describes how the user permissions will be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> ApplyPermissions(string username, string vhost, Action<UserPermissionsConfigurator> configurator,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified user permission assigned to a specified user on a specific virtual host on the current RabbitMQ server.
    /// </summary>
    /// <param name="username">RabbitMQ broker username.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> DeletePermissions(string username, string vhost, CancellationToken cancellationToken = default);
}