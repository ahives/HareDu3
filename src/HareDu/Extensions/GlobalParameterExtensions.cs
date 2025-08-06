namespace HareDu.Extensions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Security;
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
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Results<GlobalParameterInfo>> GetAllGlobalParameters(
        [NotNull] this IBrokerFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

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
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result> CreateGlobalParameter(
        [NotNull] this IBrokerFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] string parameter,
        [NotNull] Action<GlobalParameterConfigurator> configurator,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

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
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result> DeleteGlobalParameter(
        [NotNull] this IBrokerFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] string parameter,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<GlobalParameter>(credentials)
            .Delete(parameter, cancellationToken)
            .ConfigureAwait(false);
    }
}