namespace HareDu;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

/// <summary>
/// Represents operations for managing RabbitMQ users including retrieval, creation, deletion, and configuration of limits and permissions.
/// </summary>
public interface User :
    BrokerAPI
{
    /// <summary>
    /// Returns information about all users on the current RabbitMQ server.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Results<UserInfo>> GetAll([NotNull] CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves information about all users on the current RabbitMQ server excluding their permissions.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="HareDu.Core.Results{T}"/> containing the user information.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Results<UserInfo>> GetAllWithoutPermissions([NotNull] CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new user on the RabbitMQ server with the specified parameters.
    /// </summary>
    /// <param name="username">The username of the new user.</param>
    /// <param name="password">The password for the user. Required if passwordHash is not provided.</param>
    /// <param name="passwordHash">The precomputed password hash. Optional if a password is provided instead.</param>
    /// <param name="configurator">Delegate to configure additional settings for the user.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>An asynchronous task of <see cref="HareDu.Core.Result"/> representing the result of the user creation operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result> Create(
        [NotNull] string username,
        [NotNull] string password,
        [AllowNull] string passwordHash = null,
        [AllowNull] Action<UserConfigurator> configurator = null,
        [NotNull] CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user from the RabbitMQ server.
    /// </summary>
    /// <param name="username">The username of the user to be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>An asynchronous task of <see cref="HareDu.Core.Result"/> representing the result of the delete operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result> Delete([NotNull] string username, [NotNull] CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple users from the RabbitMQ server.
    /// </summary>
    /// <param name="usernames">A list of usernames to be deleted from the server.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="HareDu.Core.Result"/></returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result> BulkDelete([NotNull] IList<string> usernames, [NotNull] CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns information about user limits for the specified RabbitMQ user.
    /// </summary>
    /// <param name="username">RabbitMQ broker username.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Results<UserLimitsInfo>> GetLimitsByUser([NotNull] string username, [NotNull] CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves information about all defined user limits on the current RabbitMQ server.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>An asynchronous task containing <see cref="HareDu.Core.Results{T}"/> with a collection of <see cref="HareDu.Model.UserLimitsInfo"/>.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Results<UserLimitsInfo>> GetAllUserLimits([NotNull] CancellationToken cancellationToken = default);

    /// <summary>
    /// Defines a limit for a specific user on the RabbitMQ server.
    /// </summary>
    /// <param name="username">The name of the user for whom the limit will be defined.</param>
    /// <param name="configurator">An action delegate to configure the user limit details.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>An asynchronous task of <see cref="HareDu.Core.Result"/> containing the result of the limit definition operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result> DefineLimit(
        [NotNull] string username,
        [AllowNull] Action<UserLimitConfigurator> configurator = null,
        [NotNull] CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified limit for a given user on the RabbitMQ server.
    /// </summary>
    /// <param name="username">The username for which the limit will be deleted.</param>
    /// <param name="limit">The specific user limit to delete, such as maximum connections or channels.</param>
    /// <param name="cancellationToken">Token used to cancel the deletion operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="Result"/> indicating the operation's success or failure.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result> DeleteLimit(
        [NotNull] string username,
        [NotNull] UserLimit limit,
        [NotNull] CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns information about all permissions assigned to RabbitMQ users.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="HareDu.Core.Results{HareDu.Model.UserPermissionsInfo}"/> containing user permission details.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Results<UserPermissionsInfo>> GetAllPermissions([NotNull] CancellationToken cancellationToken = default);
}