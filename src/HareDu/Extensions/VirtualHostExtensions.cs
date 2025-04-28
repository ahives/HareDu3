namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Configuration;
using Model;

public static class VirtualHostExtensions
{
    /// <summary>
    /// Retrieves information about all virtual hosts in the RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that provides access to the RabbitMQ broker.</param>
    /// <param name="credentials">The credentials used to authenticate and access the RabbitMQ server.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task containing information about all virtual hosts as a <see cref="Results{VirtualHostInfo}"/> object.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    public static async Task<Results<VirtualHostInfo>> GetAllVirtualHosts(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<VirtualHost>(credentials)
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a virtual host in the RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that provides access to the RabbitMQ broker.</param>
    /// <param name="credentials">The credentials used to authenticate and access the RabbitMQ server.</param>
    /// <param name="vhost">The name of the virtual host to be created.</param>
    /// <param name="configurator">The action to configure additional properties for the virtual host during creation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task containing the result of the virtual host creation as a <see cref="Result"/> object.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    public static async Task<Result> CreateVirtualHost(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string vhost, Action<VirtualHostConfigurator> configurator,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<VirtualHost>(credentials)
            .Create(vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the specified virtual host from the RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that provides access to the RabbitMQ broker.</param>
    /// <param name="credentials">The credentials used to authenticate and access the RabbitMQ server.</param>
    /// <param name="vhost">The name of the virtual host to delete.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task containing the result of the delete operation as a <see cref="Result"/> object.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    public static async Task<Result> DeleteVirtualHost(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<VirtualHost>(credentials)
            .Delete(vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Starts up a virtual host on a specified node in the RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that provides access to the RabbitMQ broker.</param>
    /// <param name="credentials">The credentials used to authenticate and access the RabbitMQ server.</param>
    /// <param name="vhost">The name of the virtual host to be started.</param>
    /// <param name="node">The node on which the virtual host is to be started.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the operation with the result of starting up the virtual host.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    public static async Task<Result> StartupVirtualHost(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string vhost, string node,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<VirtualHost>(credentials)
            .Startup(vhost, node, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves information about all resource limits configured on the virtual hosts on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that provides access to the RabbitMQ broker.</param>
    /// <param name="credentials">The credentials used to authenticate and access the RabbitMQ server.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task containing information about all virtual host limits as a <see cref="Results{VirtualHostLimitsInfo}"/> object.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    public static async Task<Results<VirtualHostLimitsInfo>> GetAllVirtualHostLimits(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<VirtualHost>(credentials)
            .GetAllLimits(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Defines a resource limit for the specified virtual host on the RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that provides the functionality to interact with RabbitMQ.</param>
    /// <param name="credentials">The action to provide the necessary credentials for authentication.</param>
    /// <param name="vhost">The name of the virtual host where the limit is to be set.</param>
    /// <param name="configurator">An optional action to configure the specific limit settings for the virtual host.</param>
    /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
    /// <returns>A task containing the result of the attempt to define the virtual host limit.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    public static async Task<Result> DefineVirtualHostLimit(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string vhost,
        Action<VirtualHostLimitsConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<VirtualHost>(credentials)
            .DefineLimit(vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a specific limit for a given virtual host on the RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="credentials">The credentials for authenticating the operation.</param>
    /// <param name="vhost">The name of the virtual host for which the limit will be deleted.</param>
    /// <param name="limit">The specific virtual host limit to delete (e.g., MaxConnections, MaxQueues).</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A <see cref="Result"/> object indicating the outcome of the operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    public static async Task<Result> DeleteVirtualHostLimit(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string vhost, VirtualHostLimit limit,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<VirtualHost>(credentials)
            .DeleteLimit(vhost, limit, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the permissions associated with a specific virtual host.
    /// </summary>
    /// <param name="factory">The API that provides access to RabbitMQ operations.</param>
    /// <param name="credentials">The credentials provider used for authentication with the RabbitMQ server.</param>
    /// <param name="vhost">The name of the virtual host to retrieve permissions for.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Task containing the list of permissions as a <see cref="Results{VirtualHostPermissionInfo}"/> object.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    public static async Task<Results<VirtualHostPermissionInfo>> GetVirtualHostPermissions(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<VirtualHost>(credentials)
            .GetPermissions(vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves information about topic permissions for the specified virtual host.
    /// </summary>
    /// <param name="factory">The API that provides access to the RabbitMQ broker.</param>
    /// <param name="credentials">The credentials provider for authenticating with the broker.</param>
    /// <param name="vhost">The name of the virtual host for which to retrieve topic permissions.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>Task containing the topic permissions information as a <see cref="Results{VirtualHostTopicPermissionInfo}"/> object.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    public static async Task<Results<VirtualHostTopicPermissionInfo>> GetVirtualHostTopicPermissions(
        this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<VirtualHost>(credentials)
            .GetTopicPermissions(vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves information about the specified virtual host in the RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that provides access to the RabbitMQ broker.</param>
    /// <param name="credentials">The credentials used to authenticate and access the RabbitMQ server.</param>
    /// <param name="vhost">The name of the virtual host to retrieve information for.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task containing the details of the specified virtual host as a <see cref="Result{VirtualHostInfo}"/> object.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    public static async Task<Result<VirtualHostInfo>> GetVirtualHost(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);
        
        return await factory
            .API<VirtualHost>(credentials)
            .Get(vhost, cancellationToken)
            .ConfigureAwait(false);
    }
}