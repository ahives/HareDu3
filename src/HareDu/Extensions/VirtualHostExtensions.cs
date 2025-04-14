namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class VirtualHostExtensions
{
    /// <summary>
    /// Returns information about each virtual host on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Task containing information about all virtual hosts as a <see cref="Results{VirtualHostInfo}"/> object.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if the API implementation for the user could not be accessed.</exception>
    public static async Task<Results<VirtualHostInfo>> GetAllVirtualHosts(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates the specified RabbitMQ virtual host on the current server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="configurator">Describes how the virtual host will be configured during its creation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Task that represents the creation operation and includes the result of the operation as a <see cref="Result"/> object.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if the API implementation for the virtual host could not be accessed.</exception>
    public static async Task<Result> CreateVirtualHost(this IBrokerFactory factory,
        string vhost, Action<VirtualHostConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .Create(vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the specified RabbitMQ virtual host on the current server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host to delete.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Task containing the result of the delete operation as a <see cref="Result"/> object.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if the API implementation for the virtual host could not be accessed.</exception>
    public static async Task<Result> DeleteVirtualHost(this IBrokerFactory factory,
        string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .Delete(vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Starts the specified virtual host on the provided node in the RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost">The name of the virtual host to start.</param>
    /// <param name="node">The name of the node where the virtual host will be started.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the asynchronous operation, containing the result of the startup operation as a <see cref="Result"/> object.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if the API implementation for the user could not be accessed.</exception>
    public static async Task<Result> StartupVirtualHost(this IBrokerFactory factory,
        string vhost, string node, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .Startup(vhost, node, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves information about all virtual host limits on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API implementation for accessing broker-related features.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Task containing the result of virtual host limits as a <see cref="Results{VirtualHostLimitsInfo}"/> object.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if the API implementation for the user could not be accessed.</exception>
    public static async Task<Results<VirtualHostLimitsInfo>> GetAllVirtualHostLimits(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .GetAllLimits(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Defines a limit for a specified virtual host on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost">The name of the virtual host being modified.</param>
    /// <param name="configurator">Configurator used to specify the details of the virtual host limit.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Task representing the operation and the result of the virtual host limit definition as a <see cref="Result"/> object.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if the API implementation for the user could not be accessed.</exception>
    public static async Task<Result> DefineVirtualHostLimit(this IBrokerFactory factory,
        string vhost, Action<VirtualHostLimitsConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .DefineLimit(vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a specific limit applied to the given virtual host.
    /// </summary>
    /// <param name="factory">The API that provides access to broker functionality.</param>
    /// <param name="vhost">The name of the virtual host for which the limit is to be deleted.</param>
    /// <param name="limit">The specific limit to be deleted (e.g., MaxConnections, MaxQueues).</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that returns a <see cref="Result"/> indicating the success or failure of the operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if the API implementation for the user could not be accessed.</exception>
    public static async Task<Result> DeleteVirtualHostLimit(this IBrokerFactory factory,
        string vhost, VirtualHostLimit limit, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .DeleteLimit(vhost, limit, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the permissions associated with a specified virtual host on the RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the necessary functionality for interacting with the RabbitMQ server.</param>
    /// <param name="vhost">The name of the virtual host for which to retrieve permissions.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task containing the permissions associated with the specified virtual host as a <see cref="Results{VirtualHostPermissionInfo}"/> object.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the provided <paramref name="factory"/> is null.</exception>
    public static async Task<Results<VirtualHostPermissionInfo>> GetVirtualHostPermissions(this IBrokerFactory factory,
        string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .GetPermissions(vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves all topic permissions for the specified virtual host from the RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API responsible for executing the required functionality.</param>
    /// <param name="vhost">The name of the RabbitMQ virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the currently running operation.</param>
    /// <returns>Task containing the result of the topic permissions as a <see cref="Results{VirtualHostTopicPermissionInfo}"/> object.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Thrown when the required API implementation for the virtual host cannot be accessed.</exception>
    public static async Task<Results<VirtualHostTopicPermissionInfo>> GetVirtualHostTopicPermissions(this IBrokerFactory factory,
        string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .GetTopicPermissions(vhost, cancellationToken)
            .ConfigureAwait(false);
    }
}