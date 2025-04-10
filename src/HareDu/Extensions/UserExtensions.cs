namespace HareDu.Extensions;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class UserExtensions
{
    /// <summary>
    /// Returns information about all users on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a user.</exception>
    public static async Task<Results<UserInfo>> GetAllUsers(this IBrokerFactory factory, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Returns information about all users that do not have permissions on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a user.</exception>
    public static async Task<Results<UserInfo>> GetAllUsersWithoutPermissions(this IBrokerFactory factory, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>()
            .GetAllWithoutPermissions(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a user on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username">RabbitMQ broker username.</param>
    /// <param name="password">RabbitMQ broker password.</param>
    /// <param name="passwordHash">RabbitMQ broker password hash.</param>
    /// <param name="configurator">Describes how the user permission will be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a user.</exception>
    public static async Task<Result> CreateUser(this IBrokerFactory factory,
        string username, string password, string passwordHash = null, Action<UserConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>()
            .Create(username, password, passwordHash, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the specified user on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username">RabbitMQ broker username.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a user.</exception>
    public static async Task<Result> DeleteUser(this IBrokerFactory factory,
        string username, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>()
            .Delete(username, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes specified users on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="usernames">List of RabbitMQ broker usernames.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a user.</exception>
    public static async Task<Result> DeleteUsers(this IBrokerFactory factory,
        IList<string> usernames, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>()
            .BulkDelete(usernames, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Returns information about user limits for connections and channels across all users on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a user.</exception>
    public static async Task<Results<UserLimitsInfo>> GetAllUserLimits(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>()
            .GetAllUserLimits(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Returns information about user limits for connections and channels by the specified user on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username">RabbitMQ broker username.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a user.</exception>
    public static async Task<Results<UserLimitsInfo>> GetUserLimitsByUser(this IBrokerFactory factory,
        string username, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        
        return await factory
            .API<User>()
            .GetLimitsByUser(username, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Create a limit for the specified RabbitMQ user.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username">RabbitMQ broker username.</param>
    /// <param name="configurator">Describes what limit will be created for the user.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    public static async Task<Result> DefineUserLimit(this IBrokerFactory factory,
        string username, Action<UserLimitConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        
        return await factory
            .API<User>()
            .DefineLimit(username, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Delete a limit for the specified RabbitMQ user.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username">RabbitMQ broker username.</param>
    /// <param name="limit">User limit (e.g., max connections, max channels, etc.)</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    public static async Task<Result> DeleteUserLimit(this IBrokerFactory factory,
        string username, UserLimit limit, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        
        return await factory
            .API<User>()
            .DeleteLimit(username, limit, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Returns information about all user permissions on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a user.</exception>
    public static async Task<Results<UserPermissionsInfo>> GetAllUserPermissions(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>()
            .GetAllPermissions(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a user permission and assign it to a user on a specific virtual host on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username">RabbitMQ broker username.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="configurator">Describes how the user permissions will be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a user.</exception>
    public static async Task<Result> ApplyUserPermissions(this IBrokerFactory factory,
        string username, string vhost, Action<UserPermissionsConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        configurator ??= x =>
        {
            x.UsingConfigurePattern(".*");
            x.UsingReadPattern(".*");
            x.UsingWritePattern(".*");
        };
            
        return await factory
            .API<User>()
            .ApplyPermissions(username, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the specified user on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username">RabbitMQ broker username.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a user.</exception>
    public static async Task<Result> DeleteUserPermissions(this IBrokerFactory factory,
        string username, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>()
            .DeletePermissions(username, vhost, cancellationToken)
            .ConfigureAwait(false);
    }
}