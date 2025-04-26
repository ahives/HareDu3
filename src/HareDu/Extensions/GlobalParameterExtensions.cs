namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Configuration;
using Model;

public static class GlobalParameterExtensions
{
    /// <summary>
    /// Retrieves all global parameters from the RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API factory implementing the functionality.</param>
    /// <param name="credentials">Callback to configure the credentials used to connect to the RabbitMQ node.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>An asynchronous task containing the results of the global parameters retrieved from the RabbitMQ node.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Results<GlobalParameterInfo>> GetAllGlobalParameters(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<GlobalParameter>(credentials)
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a new global parameter on the RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API factory implementing the functionality.</param>
    /// <param name="credentials">Callback to configure the credentials used to connect to the RabbitMQ node.</param>
    /// <param name="parameter">The name of the global parameter to be created.</param>
    /// <param name="configurator">Callback to define the global parameter settings.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>An asynchronous task containing the result of the global parameter creation operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result> CreateGlobalParameter(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string parameter,
        Action<GlobalParameterConfigurator> configurator,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<GlobalParameter>(credentials)
            .Create(parameter, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a global parameter from the RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API that facilitates interaction with the RabbitMQ server.</param>
    /// <param name="credentials">Delegate to specify credentials used for authentication.</param>
    /// <param name="parameter">The name of the global parameter to delete.</param>
    /// <param name="cancellationToken">Token used to signal cancellation of the operation.</param>
    /// <returns>An asynchronous task representing the result of the delete operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result> DeleteGlobalParameter(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string parameter, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<GlobalParameter>(credentials)
            .Delete(parameter, cancellationToken)
            .ConfigureAwait(false);
    }
}