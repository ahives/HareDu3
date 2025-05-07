namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Security;
using Model;

public static class TopicPermissionsExtensions
{
    /// <summary>
    /// Retrieves a list of all topic permissions for all users across all virtual hosts.
    /// </summary>
    /// <param name="factory">The API factory that facilitates access to the RabbitMQ management APIs.</param>
    /// <param name="credentials">The function to configure the credentials for authenticating with the RabbitMQ broker.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of topic permissions' information.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Results<TopicPermissionsInfo>> GetAllTopicPermissions(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<TopicPermissions>(credentials)
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates topic permissions for a specific user on the given virtual host.
    /// </summary>
    /// <param name="factory">The broker factory that provides access to the RabbitMQ APIs.</param>
    /// <param name="credentials">The function to configure the credentials for authenticating with the RabbitMQ broker.</param>
    /// <param name="username">The username of the user for whom the topic permissions are being created.</param>
    /// <param name="vhost">The name of the RabbitMQ virtual host where the topic permissions will be applied.</param>
    /// <param name="configurator">The configurator that defines the topic permissions to be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> CreateTopicPermission(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string username, string vhost,
        Action<TopicPermissionsConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<TopicPermissions>(credentials)
            .Create(username, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the specified user's topic permissions for the given virtual host.
    /// </summary>
    /// <param name="factory">The API factory that facilitates access to the RabbitMQ management APIs.</param>
    /// <param name="credentials">The function to configure the credentials for authenticating with the RabbitMQ broker.</param>
    /// <param name="username">The name of the user whose topic permissions are to be deleted.</param>
    /// <param name="vhost">The virtual host from which the user's permissions are to be removed.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the operation was successful or failed.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> DeleteTopicPermission(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string username, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<TopicPermissions>(credentials)
            .Delete(username, vhost, cancellationToken)
            .ConfigureAwait(false);
    }
}