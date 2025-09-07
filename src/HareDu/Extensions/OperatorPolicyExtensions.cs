namespace HareDu.Extensions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Security;
using Model;

public static class OperatorPolicyExtensions
{
    /// <summary>
    /// Retrieves all operator policies defined on RabbitMQ through the specified factory.
    /// </summary>
    /// <param name="factory">The API factory that provides communication with the RabbitMQ backend to manage operator policies.</param>
    /// <param name="credentials">The credentials used to authenticate with the RabbitMQ broker.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Returns a task representing the result of the asynchronous operation containing the collection of operator policies as <see cref="Results{OperatorPolicyInfo}"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Results<OperatorPolicyInfo>> GetAllOperatorPolicies(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<OperatorPolicy>(credentials)
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates an operator policy on the specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The API that provides communication with the backend RabbitMQ system to manage operator policies.</param>
    /// <param name="credentials">The credentials used to authenticate with the RabbitMQ broker.</param>
    /// <param name="name">The name of the operator policy to create.</param>
    /// <param name="vhost">The RabbitMQ virtual host where the operator policy will be created.</param>
    /// <param name="configurator">The delegate used to configure the operator policy settings.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Returns a task representing the result of the asynchronous operation as <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result> CreateOperatorPolicy(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] string name,
        [NotNull] string vhost,
        [NotNull] Action<OperatorPolicyConfigurator> configurator,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<OperatorPolicy>(credentials)
            .Create(name, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the specified operator policy on the target RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The API used to interact with the backend RabbitMQ system to delete the operator policy.</param>
    /// <param name="credentials">The action to provide the necessary credentials for authentication.</param>
    /// <param name="policy">The name of the operator policy to delete.</param>
    /// <param name="vhost">The virtual host on which the operator policy exists.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Returns a task representing the asynchronous operation result as <see cref="Result"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result> DeleteOperatorPolicy(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] string policy,
        [NotNull] string vhost,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<OperatorPolicy>(credentials)
            .Delete(policy, vhost, cancellationToken)
            .ConfigureAwait(false);
    }
}