namespace HareDu;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

/// <summary>
/// Represents operations for managing policies in RabbitMQ.
/// </summary>
public interface Policy :
    BrokerAPI
{
    /// <summary>
    /// Returns all policies on the current RabbitMQ node.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Results<PolicyInfo>> GetAll([NotNull] CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new policy on the specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="name">The name of the policy to be created.</param>
    /// <param name="vhost">The virtual host on which the policy will be created.</param>
    /// <param name="configurator">The actions to configure the policy definition, pattern, priority, and application scope.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>An asynchronous task containing the result of the policy creation operation.</returns>
    /// <exception cref="OperationCanceledException">Thrown if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result> Create(
        [NotNull] string name,
        [NotNull] string vhost,
        [NotNull] Action<PolicyConfigurator> configurator,
        [NotNull] CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a specified policy from the provided virtual host.
    /// </summary>
    /// <param name="name">The name of the policy to delete.</param>
    /// <param name="vhost">The name of the virtual host where the policy resides.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="Result"/> indicating the success or failure of the operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result> Delete([NotNull] string name, [NotNull] string vhost, [NotNull] CancellationToken cancellationToken = default);
}