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
    /// Returns all the RabbitMQ topic permissions.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a topic permission.</exception>
    public static async Task<Results<TopicPermissionsInfo>> GetAllTopicPermissions(
        this IBrokerFactory factory, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<TopicPermissions>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a new topic permission for the specified user per a particular RabbitMQ exchange and virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username">RabbitMQ broker username to apply topic permission to.</param>
    /// <param name="vhost">Name of the RabbitMQ virtual host.</param>
    /// <param name="configurator">Describes how the topic permission will be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a topic permission.</exception>
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
    /// Deletes all topic permissions associate with the specified user on the specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username">RabbitMQ broker username used to delete topic permission.</param>
    /// <param name="vhost">Name of the RabbitMQ virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a topic permission.</exception>
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