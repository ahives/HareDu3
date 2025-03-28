namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class GlobalParameterExtensions
{
    /// <summary>
    /// Returns all global parameters on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a global parameter.</exception>
    public static async Task<Results<GlobalParameterInfo>> GetAllGlobalParameters(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<GlobalParameter>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates the specified global parameter on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="parameter">Name of the RabbitMQ parameter.</param>
    /// <param name="configurator">Describes how the RabbitMQ parameter is to be defined.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a global parameter.</exception>
    public static async Task<Result> CreateGlobalParameter(this IBrokerFactory factory,
        string parameter, Action<GlobalParameterConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<GlobalParameter>()
            .Create(parameter, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the specified global parameter on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="parameter">Name of the RabbitMQ parameter.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a global parameter.</exception>
    public static async Task<Result> DeleteGlobalParameter(this IBrokerFactory factory,
        string parameter, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<GlobalParameter>()
            .Delete(parameter, cancellationToken)
            .ConfigureAwait(false);
    }
}