namespace HareDu;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public interface OperatorPolicy :
    BrokerAPI
{
    /// <summary>
    /// Returns all operator policies on the current RabbitMQ node.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A <see cref="ResultList{T}"/> of operator policies.</returns>
    Task<ResultList<OperatorPolicyInfo>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates the specified operator policy on the target RabbitMQ virtual host.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="vhost">The virtual host for which the policy should be applied to.</param>
    /// <param name="configurator"></param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Create(string name, string vhost, Action<OperatorPolicyConfigurator> configurator,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified operator policy on the target RabbitMQ virtual host.
    /// </summary>
    /// <param name="name">Name of the operator policy.</param>
    /// <param name="vhost">The virtual host for which the policy should be applied to.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Delete(string name, string vhost, CancellationToken cancellationToken = default);
}