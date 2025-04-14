namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class TopicPermissionsExtensions
{
    /// <summary>
    /// Retrieves all RabbitMQ topic permissions.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality for retrieving topic permissions.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of topic permissions data.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if the API implementation for the user could not be accessed.</exception>
    public static async Task<Results<TopicPermissionsInfo>> GetAllTopicPermissions(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<TopicPermissions>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a topic permission for a specific user and virtual host.
    /// </summary>
    /// <param name="factory">The broker factory used to access the topic permissions API.</param>
    /// <param name="username">The name of the user for whom the topic permission will be created.</param>
    /// <param name="vhost">The virtual host under which the topic permission is applied.</param>
    /// <param name="configurator">The configurator action used to define exchange details and patterns for the topic permission.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if the API implementation for the user could not be accessed.</exception>
    public static async Task<Result> CreateTopicPermission(this IBrokerFactory factory,
        string username, string vhost, Action<TopicPermissionsConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<TopicPermissions>()
            .Create(username, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the specific RabbitMQ topic permission for a user in the given virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality for deleting a topic permission.</param>
    /// <param name="username">The name of the user associated with the topic permission to delete.</param>
    /// <param name="vhost">The name of the virtual host associated with the topic permission to delete.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the status of the operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if the API implementation for the user could not be accessed.</exception>
    public static async Task<Result> DeleteTopicPermission(this IBrokerFactory factory,
        string username, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<TopicPermissions>()
            .Delete(username, vhost, cancellationToken)
            .ConfigureAwait(false);
    }
}