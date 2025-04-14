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
    /// Retrieves all operator policies on the target RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The API that communicates with the backend RabbitMQ system to retrieve operator policies.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Returns a task that represents the result of the asynchronous operation containing the collection of operator policies as <see cref="Results{OperatorPolicyInfo}"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
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
    /// Creates a new operator policy on the target RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The API that communicates with the backend RabbitMQ system to create a new operator policy.</param>
    /// <param name="name">Name of the operator policy.</param>
    /// <param name="vhost">The virtual host for which the policy should be applied to.</param>
    /// <param name="configurator">Describes how the operator policy should be configured.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
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
    /// <param name="factory">The API that communicates with the backend RabbitMQ system to delete the operator policy.</param>
    /// <param name="policy">The name of the operator policy to delete.</param>
    /// <param name="vhost">The name of the virtual host where the operator policy is defined.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Returns a task that represents the result of the asynchronous operation as a <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
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