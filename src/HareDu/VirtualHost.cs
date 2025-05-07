namespace HareDu;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

/// <summary>
/// Represents operations that can be performed on RabbitMQ virtual hosts.
/// A virtual host provides a logical grouping of resources such as exchanges, queues, and bindings,
/// allowing for resource isolation and segregation within the broker.
/// </summary>
public interface VirtualHost :
    BrokerAPI
{
    /// <summary>
    /// Retrieves all RabbitMQ virtual hosts from the broker.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the list of virtual hosts and related information.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Results<VirtualHostInfo>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves information about a specific RabbitMQ virtual host from the broker.
    /// </summary>
    /// <param name="vhost">The name of the virtual host to retrieve information for.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the virtual host details and related information.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result<VirtualHostInfo>> Get(string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a RabbitMQ virtual host on the broker.
    /// </summary>
    /// <param name="vhost">The name of the virtual host to be created.</param>
    /// <param name="configurator">Optional configuration options for the virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the outcome of the create operation.</returns>
    /// <exception cref="OperationCanceledException">Thrown if the thread has a cancellation request during execution.</exception>
    Task<Result> Create(string vhost, Action<VirtualHostConfigurator> configurator = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified RabbitMQ virtual host from the broker.
    /// </summary>
    /// <param name="vhost">The name of the virtual host to delete.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the operation result information.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result> Delete(string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts a RabbitMQ virtual host on the specified node.
    /// </summary>
    /// <param name="vhost">The name of the virtual host to start.</param>
    /// <param name="node">The name of the node on which the virtual host will be started.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result indicates the outcome of the operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the operation is canceled through the provided cancellation token.</exception>
    Task<Result> Startup(string vhost, string node, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all configured limits for all RabbitMQ virtual hosts from the broker.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the list of virtual host limits and related information.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Results<VirtualHostLimitsInfo>> GetAllLimits(CancellationToken cancellationToken = default);

    /// <summary>
    /// Defines resource limits for the specified virtual host.
    /// </summary>
    /// <param name="vhost">The name of the virtual host for which resource limits are being defined.</param>
    /// <param name="configurator">An optional action used to configure the resource limits.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the outcome of the limit definition process.</returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled using the cancellation token.</exception>
    Task<Result> DefineLimit(string vhost, Action<VirtualHostLimitsConfigurator> configurator = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified limit configured for the given virtual host.
    /// </summary>
    /// <param name="vhost">The name of the virtual host whose limit is to be deleted.</param>
    /// <param name="limit">The type of limit to delete (e.g., MaxConnections, MaxQueues).</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the result of the limit deletion.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result> DeleteLimit(string vhost, VirtualHostLimit limit, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the list of permissions for a specific virtual host.
    /// </summary>
    /// <param name="vhost">The name of the virtual host for which permissions are to be retrieved.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the list of permissions and related information for the specified virtual host.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Results<VirtualHostPermissionInfo>> GetAllPermissions(string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all topic permissions for a specified virtual host.
    /// </summary>
    /// <param name="vhost">The name of the virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the list of topic permission information for the specified virtual host.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Results<VirtualHostTopicPermissionInfo>> GetTopicPermissions(string vhost, CancellationToken cancellationToken = default);
}