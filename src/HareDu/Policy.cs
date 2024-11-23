namespace HareDu;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public interface Policy :
    BrokerAPI
{
    /// <summary>
    /// Returns all policies on the current RabbitMQ node.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
    Task<Results<PolicyInfo>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates the specified policy on the target RabbitMQ virtual host.
    /// </summary>
    /// <param name="name">The name of the policy.</param>
    /// <param name="vhost">The name of the virtual host.</param>
    /// <param name="configurator">Describes how the policy will be created by setting arguments through set methods.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Create(string name, string vhost, Action<PolicyConfigurator> configurator,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified policy on the target RabbitMQ virtual host.
    /// </summary>
    /// <param name="name">The name of the policy.</param>
    /// <param name="vhost">The name of the virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Delete(string name, string vhost, CancellationToken cancellationToken = default);
}