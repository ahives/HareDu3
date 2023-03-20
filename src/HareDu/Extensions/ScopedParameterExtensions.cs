namespace HareDu.Extensions;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class ScopedParameterExtensions
{
    /// <summary>
    /// Returns all scoped parameters on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result<IReadOnlyList<ScopedParameterInfo>>> GetAllScopedParameters(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<ScopedParameter>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a scoped parameter for a particular RabbitMQ component and virtual host on the current server.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="parameter">Name of the RabbitMQ parameter.</param>
    /// <param name="value">Value of the RabbitMQ parameter.</param>
    /// <param name="component">Name of the RabbitMQ component.</param>
    /// <param name="vhost">Name of the RabbitMQ virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result> CreateScopeParameter<T>(this IBrokerFactory factory,
        string parameter, T value, string component, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<ScopedParameter>()
            .Create(parameter, value, component, vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the specified scoped parameter for a particular RabbitMQ component and virtual host on the current server.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="parameter">Name of the RabbitMQ parameter.</param>
    /// <param name="component">Name of the RabbitMQ component.</param>
    /// <param name="vhost">Name of the RabbitMQ virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result> DeleteScopedParameter(this IBrokerFactory factory,
        string parameter, string component, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<ScopedParameter>()
            .Delete(parameter, component, vhost, cancellationToken)
            .ConfigureAwait(false);
    }
}