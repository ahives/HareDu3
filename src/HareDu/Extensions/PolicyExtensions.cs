namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class PolicyExtensions
{
    /// <summary>
    /// Retrieves all policies from the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The API factory that provides access to the policy endpoint.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation, containing the results of all policies on the broker.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Results<PolicyInfo>> GetAllPolicies(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Policy>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a new policy on the RabbitMQ broker with the specified name, virtual host, and configuration.
    /// </summary>
    /// <param name="factory">The API factory that provides access to the policy endpoint.</param>
    /// <param name="name">The name of the policy to be created.</param>
    /// <param name="vhost">The virtual host to which the policy will be applied.</param>
    /// <param name="configurator">An action to configure the policy settings.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the policy creation request.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result> CreatePolicy(this IBrokerFactory factory,
        string name, string vhost, Action<PolicyConfigurator> configurator,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Policy>()
            .Create(name, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the policy with the specified name from the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The API factory that provides access to the policy endpoint.</param>
    /// <param name="name">The name of the policy to be deleted.</param>
    /// <param name="vhost">The virtual host from which the policy will be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the policy deletion request.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result> DeletePolicy(this IBrokerFactory factory,
        string name, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Policy>()
            .Delete(name, vhost, cancellationToken)
            .ConfigureAwait(false);
    }
}