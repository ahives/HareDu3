namespace HareDu.Extensions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Security;
using Model;

public static class PolicyExtensions
{
    /// <summary>
    /// Retrieves all policies available on the RabbitMQ broker for the specified credentials.
    /// </summary>
    /// <param name="factory">The broker factory that provides access to the RabbitMQ API.</param>
    /// <param name="credentials">An action to configure the credentials used to authenticate with the RabbitMQ API.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the policy retrieval request, including a collection of policy information.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Results<PolicyInfo>> GetAllPolicies(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Policy>(credentials)
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a policy on the RabbitMQ broker for the specified name, virtual host, and configuration.
    /// </summary>
    /// <param name="factory">The broker factory that provides access to the RabbitMQ API.</param>
    /// <param name="credentials">An action to configure the credentials used to authenticate with the RabbitMQ API.</param>
    /// <param name="name">The name of the policy to be created.</param>
    /// <param name="vhost">The virtual host where the policy should be created.</param>
    /// <param name="configurator">An action to configure the policy settings using a <see cref="PolicyConfigurator"/>.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the policy creation request.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result> CreatePolicy(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] string name,
        [NotNull] string vhost,
        [NotNull] Action<PolicyConfigurator> configurator,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Policy>(credentials)
            .Create(name, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the specified policy on the RabbitMQ broker for a given virtual host.
    /// </summary>
    /// <param name="factory">The broker factory that provides access to the RabbitMQ API.</param>
    /// <param name="credentials">An action to configure the credentials used to authenticate with the RabbitMQ API.</param>
    /// <param name="name">The name of the policy to delete.</param>
    /// <param name="vhost">The virtual host where the policy is located.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the delete policy request.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result> DeletePolicy(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] string name,
        [NotNull] string vhost,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Policy>(credentials)
            .Delete(name, vhost, cancellationToken)
            .ConfigureAwait(false);
    }
}