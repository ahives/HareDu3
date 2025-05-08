namespace HareDu.Extensions;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Security;
using Model;

public static class UserExtensions
{
    /// <summary>
    /// Retrieves all users from the broker.
    /// </summary>
    /// <param name="factory">The broker factory used to interface with the broker API.</param>
    /// <param name="credentials">The credentials used to authenticate with the broker.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing a result wrapper with information about all users.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Results<UserInfo>> GetAllUsers(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>(credentials)
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves all users from the broker without their associated permissions.
    /// </summary>
    /// <param name="factory">The broker factory used to interface with the broker API.</param>
    /// <param name="credentials">The credentials used to authenticate with the broker.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing a result wrapper with information about all users without their permissions.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Results<UserInfo>> GetAllUsersWithoutPermissions(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>(credentials)
            .GetAllWithoutPermissions(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a user in the broker with the specified parameters.
    /// </summary>
    /// <param name="factory">The broker factory used to interface with the broker API.</param>
    /// <param name="credentials">The credentials used to authenticate with the broker.</param>
    /// <param name="username">The username for the new user.</param>
    /// <param name="password">The password for the new user.</param>
    /// <param name="passwordHash">The password hash for the new user. Optional if a plain password is provided.</param>
    /// <param name="configurator">An optional delegate to configure additional user properties.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the result of the user creation process.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> CreateUser(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string username, string password, string passwordHash = null,
        Action<UserConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>(credentials)
            .Create(username, password, passwordHash, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a user from the broker.
    /// </summary>
    /// <param name="factory">The broker factory used to interface with the broker API.</param>
    /// <param name="credentials">The credentials used to authenticate with the broker.</param>
    /// <param name="username">The username of the user to delete.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the result of the delete action.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> DeleteUser(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string username, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>(credentials)
            .Delete(username, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes multiple users from the broker.
    /// </summary>
    /// <param name="factory">The broker factory used to interface with the broker API.</param>
    /// <param name="credentials">The credentials used to authenticate with the broker.</param>
    /// <param name="usernames">A list of usernames to be deleted from the broker.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the result of the delete operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> DeleteUsers(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, IList<string> usernames, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>(credentials)
            .BulkDelete(usernames, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves all user limits from the broker.
    /// </summary>
    /// <param name="factory">The broker factory used to interface with the broker API.</param>
    /// <param name="credentials">The credentials used to authenticate with the broker.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing a result wrapper with information about all user limits.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Results<UserLimitsInfo>> GetAllUserLimits(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>(credentials)
            .GetAllUserLimits(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves user limits for a specific user in the broker.
    /// </summary>
    /// <param name="factory">The broker factory used to interface with the broker API.</param>
    /// <param name="credentials">The credentials used to authenticate with the broker.</param>
    /// <param name="username">The username of the user whose limits are being retrieved.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing results with user limits information for the specified user.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Results<UserLimitsInfo>> GetUserLimitsByUser(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string username, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>(credentials)
            .GetLimitsByUser(username, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Defines a user limit for a specific RabbitMQ user.
    /// </summary>
    /// <param name="factory">The broker factory used to interface with the broker API.</param>
    /// <param name="credentials">The credentials used to authenticate with the broker.</param>
    /// <param name="username">The username associated with the desired user limit.</param>
    /// <param name="configurator">The configurator used to define the user limit settings.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the result of defining the user limit.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> DefineUserLimit(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string username,
        Action<UserLimitConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>(credentials)
            .DefineLimit(username, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a specific user limit for a given user.
    /// </summary>
    /// <param name="factory">The broker factory used to access the broker API.</param>
    /// <param name="credentials">The credentials used to authenticate with the broker.</param>
    /// <param name="username">The username of the user whose limit will be deleted.</param>
    /// <param name="limit">The type of limit to delete (e.g., MaxConnections, MaxChannels).</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the result of the delete operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> DeleteUserLimit(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string username, UserLimit limit, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>(credentials)
            .DeleteLimit(username, limit, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves all user permissions from the broker.
    /// </summary>
    /// <param name="factory">The broker factory used to interface with the broker API.</param>
    /// <param name="credentials">The credentials used to authenticate with the broker.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, containing a result wrapper with information about all user permissions.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Results<UserPermissionsInfo>> GetAllUserPermissions(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<User>(credentials)
            .GetAllPermissions(cancellationToken)
            .ConfigureAwait(false);
    }
}