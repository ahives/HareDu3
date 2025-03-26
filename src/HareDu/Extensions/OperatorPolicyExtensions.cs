namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class OperatorPolicyExtensions
{
    /// <summary>
    /// Returns all operator policies on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a operator policy.</exception>
    public static async Task<Results<OperatorPolicyInfo>> GetAllOperatorPolicies(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<OperatorPolicy>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates the specified operator policy on the target RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="name">Name of the operator policy.</param>
    /// <param name="vhost">The virtual host for which the policy should be applied to.</param>
    /// <param name="configurator">Describes how the operator policy will be created by setting arguments through set methods.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a operator policy.</exception>
    public static async Task<Result> CreateOperatorPolicy(this IBrokerFactory factory, string name,
        string vhost, Action<OperatorPolicyConfigurator> configurator,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<OperatorPolicy>()
            .Create(name, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the specified operator policy on the target RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="policy">Name of the operator policy.</param>
    /// <param name="vhost">The virtual host for which the policy should be applied to.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a operator policy.</exception>
    public static async Task<Result> DeleteOperatorPolicy(this IBrokerFactory factory,
        string policy, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<OperatorPolicy>()
            .Delete(policy, vhost, cancellationToken)
            .ConfigureAwait(false);
    }
}