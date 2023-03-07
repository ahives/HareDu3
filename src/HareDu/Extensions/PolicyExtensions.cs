namespace HareDu.Extensions;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class PolicyExtensions
{
    /// <summary>
    /// Returns all policies on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result<IReadOnlyList<PolicyInfo>>> GetAllPolicies(this IBrokerApiFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Policy>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates the specified policy on the target RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="name">The name of the policy.</param>
    /// <param name="vhost">The name of the virtual host.</param>
    /// <param name="configurator">Describes how the policy will be created by setting arguments through set methods.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result> CreatePolicy(this IBrokerApiFactory factory,
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
    /// Deletes the specified policy on the target RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="name">The name of the policy.</param>
    /// <param name="vhost">The name of the virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result> DeletePolicy(this IBrokerApiFactory factory,
        string name, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Policy>()
            .Delete(name, vhost, cancellationToken)
            .ConfigureAwait(false);
    }
}