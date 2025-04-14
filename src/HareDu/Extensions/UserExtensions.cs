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
    /// Retrieves information about all users on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A collection of user information for all users.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not initialize the API for user operations.</exception>
    public static async Task<Results<UserInfo>> GetAllUsers(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves information about all users that do not have permissions on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A collection of user information for users without permissions.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not initialize the API for user operations.</exception>
    public static async Task<Results<UserInfo>> GetAllUsersWithoutPermissions(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
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
    /// <param name="username">The username for the RabbitMQ broker.</param>
    /// <param name="password">The password for the RabbitMQ broker.</param>
    /// <param name="passwordHash">The password hash for the RabbitMQ broker, if applicable.</param>
    /// <param name="configurator">Delegate that defines the user's configuration and permissions.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Returns the result of the user creation operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not initialize the API for user operations.</exception>
    public static async Task<Result> CreateUser(this IBrokerFactory factory,
        string username, string password, string passwordHash = null, Action<UserConfigurator> configurator = null,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>()
            .Create(username, password, passwordHash, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the specified user from the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The factory implementing the required user management functionality.</param>
    /// <param name="username">The username of the user to be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A <see cref="Result"/> indicating the outcome of the delete operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not initialize the API for user operations.</exception>
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
    /// <param name="usernames">A list of RabbitMQ broker usernames to delete.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result indicating the success or failure of the operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
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
    /// Retrieves information about user limits for connections and channels across all users on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A collection of user limits information for all users on the server.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not initialize the API for user limit operations.</exception>
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
    /// Retrieves information about user limits for connections and channels for the specified user on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username">The RabbitMQ username for which limits information is retrieved.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A collection of user limit information associated with the specified user.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if the API implementation for the user could not be accessed.</exception>
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
    /// Defines a limit for the specified RabbitMQ user.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username">The username of the RabbitMQ user for whom the limit will be defined.</param>
    /// <param name="configurator">An action to configure the details of the user limit.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>The result of the operation encapsulated in a <see cref="Result"/> object.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if the API implementation for the user could not be accessed.</exception>
    public static async Task<Result> DefineUserLimit(this IBrokerFactory factory,
        string username, Action<UserLimitConfigurator> configurator = null,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        
        return await factory
            .API<User>()
            .DefineLimit(username, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a limit for the specified RabbitMQ user.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username">The name of the RabbitMQ user for whom the limit is to be deleted.</param>
    /// <param name="limit">The specific user limit to delete (e.g., max connections or max channels).</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A <see cref="Result"/> indicating the outcome of the operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if the API implementation for the user could not be accessed.</exception>
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
    /// Retrieves information about all user permissions on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A collection of user permission information for all users.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory instance is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not initialize the API for user permissions operations.</exception>
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
    /// Creates user permissions and assigns them to a specified user on a particular virtual host on the RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username">The RabbitMQ broker username to which the permissions will be applied.</param>
    /// <param name="vhost">The name of the RabbitMQ virtual host where the permissions will be assigned.</param>
    /// <param name="configurator">Defines the configuration for user permissions, including read, write, and configuration patterns.</param>
    /// <param name="cancellationToken">Token used to cancel the running operation on the current thread.</param>
    /// <returns>A task that represents the result of the operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if the API implementation for user operations could not be initialized or resolved.</exception>
    public static async Task<Result> ApplyUserPermissions(this IBrokerFactory factory,
        string username, string vhost, Action<UserPermissionsConfigurator> configurator,
        CancellationToken cancellationToken = default)
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
    /// Deletes the permissions of the specified user on the given RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username">The username of the RabbitMQ broker user whose permissions are to be deleted.</param>
    /// <param name="vhost">The name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result indicating the success or failure of the operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not initialize the API for user permissions operations.</exception>
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