namespace HareDu;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

/// <summary>
/// Represents a contract for managing topic permissions within a RabbitMQ broker.
/// </summary>
public interface TopicPermissions :
    BrokerAPI
{
    /// <summary>
    /// Retrieves all topic permissions configured on the RabbitMQ broker.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation, containing the list of all topic permissions.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Results<TopicPermissionsInfo>> GetAll([NotNull] CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new set of topic permissions for a specified user on a particular virtual host.
    /// </summary>
    /// <param name="username">The username for which the topic permissions will be created.</param>
    /// <param name="vhost">The virtual host where the topic permissions will be applied.</param>
    /// <param name="configurator">An action to configure the topic permissions.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the creation process.</returns>
    /// <exception cref="OperationCanceledException">Throws if the operation is canceled via the cancellation token.</exception>
    [return: NotNull]
    Task<Result> Create(
        [NotNull] string username,
        [NotNull] string vhost,
        [NotNull] Action<TopicPermissionsConfigurator> configurator,
        [NotNull] CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a specific user's topic permissions on the specified virtual host.
    /// </summary>
    /// <param name="username">The name of the user whose topic permissions are to be deleted.</param>
    /// <param name="vhost">The name of the virtual host where the topic permissions will be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the delete operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result> Delete([NotNull] string username, [NotNull] string vhost, [NotNull] CancellationToken cancellationToken = default);
}