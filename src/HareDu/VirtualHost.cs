namespace HareDu;

using System;
using System.Diagnostics.CodeAnalysis;
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
    [return: NotNull]
    Task<Results<VirtualHostInfo>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves information about a specific RabbitMQ virtual host from the broker.
    /// </summary>
    /// <param name="vhost">The name of the virtual host to retrieve information for.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the virtual host details and related information.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result<VirtualHostInfo>> Get([NotNull] string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a RabbitMQ virtual host on the broker.
    /// </summary>
    /// <param name="vhost">The name of the virtual host to be created.</param>
    /// <param name="configurator">Optional configuration options for the virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the outcome of the create operation.</returns>
    /// <exception cref="OperationCanceledException">Thrown if the thread has a cancellation request during execution.</exception>
    [return: NotNull]
    Task<Result> Create(
        [NotNull] string vhost,
        [AllowNull] Action<VirtualHostConfigurator> configurator = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified RabbitMQ virtual host from the broker.
    /// </summary>
    /// <param name="vhost">The name of the virtual host to delete.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the operation result information.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result> Delete([NotNull] string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts a RabbitMQ virtual host on the specified node.
    /// </summary>
    /// <param name="vhost">The name of the virtual host to start.</param>
    /// <param name="node">The name of the node on which the virtual host will be started.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result indicates the outcome of the operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the operation is canceled through the provided cancellation token.</exception>
    [return: NotNull]
    Task<Result> Startup(
        [NotNull] string vhost,
        [NotNull] string node,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all configured limits for all RabbitMQ virtual hosts from the broker.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the list of virtual host limits and related information.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Results<VirtualHostLimitsInfo>> GetAllLimits(CancellationToken cancellationToken = default);

    /// <summary>
    /// Defines resource limits for the specified virtual host.
    /// </summary>
    /// <param name="vhost">The name of the virtual host for which resource limits are being defined.</param>
    /// <param name="configurator">An optional action used to configure the resource limits.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the outcome of the limit definition process.</returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled using the cancellation token.</exception>
    [return: NotNull]
    Task<Result> DefineLimit(
        [NotNull] string vhost,
        [AllowNull] Action<VirtualHostLimitsConfigurator> configurator = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified limit configured for the given virtual host.
    /// </summary>
    /// <param name="vhost">The name of the virtual host whose limit is to be deleted.</param>
    /// <param name="limit">The type of limit to delete (e.g., MaxConnections, MaxQueues).</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the result of the limit deletion.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result> DeleteLimit(
        [NotNull] string vhost,
        VirtualHostLimit limit,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the list of permissions for a specific virtual host.
    /// </summary>
    /// <param name="vhost">The name of the virtual host for which permissions are to be retrieved.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the list of permissions and related information for the specified virtual host.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Results<VirtualHostPermissionInfo>> GetAllPermissions([NotNull] string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a user's permissions for a specific RabbitMQ virtual host.
    /// </summary>
    /// <param name="vhost">The name of the virtual host for which the user's permissions need to be retrieved.</param>
    /// <param name="username">The name of the user whose permissions are being queried.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the user's permissions for the specified virtual host.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result<VirtualHostPermissionInfo>> GetUserPermissions(
        [NotNull] string vhost,
        [NotNull] string username,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all topic permissions for a specified virtual host.
    /// </summary>
    /// <param name="vhost">The name of the virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the list of topic permission information for the specified virtual host.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Results<VirtualHostTopicPermissionInfo>> GetTopicPermissions([NotNull] string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Applies permissions for a specified user to a virtual host.
    /// </summary>
    /// <param name="username">The name of the user the permissions will be applied to.</param>
    /// <param name="vhost">The virtual host where the permissions will be set.</param>
    /// <param name="configurator">A delegate that configures the permissions for the user.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>An asynchronous task of <see cref="HareDu.Core.Result"/>.</returns>
    /// <exception cref="OperationCanceledException">Thrown if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result> ApplyPermissions(
        [NotNull] string username,
        [NotNull] string vhost,
        [NotNull] Action<UserPermissionsConfigurator> configurator,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the permissions for the specified user on the specified virtual host (vhost).
    /// </summary>
    /// <param name="username">The username for which the permissions will be deleted.</param>
    /// <param name="vhost">The virtual host where the permissions will be removed.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>An asynchronous task of <see cref="HareDu.Core.Result"/> representing the result of the operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the operation is canceled.</exception>
    [return: NotNull]
    Task<Result> DeletePermissions(
        [NotNull] string username,
        [NotNull] string vhost,
        CancellationToken cancellationToken = default);
}