namespace HareDu;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

/// <summary>
/// Provides a contract for managing operator policies on the message broker.
/// </summary>
public interface OperatorPolicy :
    BrokerAPI
{
    /// <summary>
    /// Retrieves all operator policies on the broker.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A <see cref="Results{OperatorPolicyInfo}"/> object containing the details of all operator policies on the broker.</returns>
    [return: NotNull]
    Task<Results<OperatorPolicyInfo>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new operator policy on the specified virtual host.
    /// </summary>
    /// <param name="name">The name of the operator policy to create.</param>
    /// <param name="vhost">The virtual host where the operator policy will be created.</param>
    /// <param name="configurator">The configuration details for the operator policy.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A <see cref="Result"/> object indicating the outcome of the operation.</returns>
    [return: NotNull]
    Task<Result> Create(
        [NotNull] string name,
        [NotNull] string vhost,
        [NotNull] Action<OperatorPolicyConfigurator> configurator,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified operator policy from the given virtual host.
    /// </summary>
    /// <param name="name">The name of the operator policy to delete.</param>
    /// <param name="vhost">The virtual host from which the operator policy should be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the delete operation.</returns>
    [return: NotNull]
    Task<Result> Delete([NotNull] string name, [NotNull] string vhost, CancellationToken cancellationToken = default);
}