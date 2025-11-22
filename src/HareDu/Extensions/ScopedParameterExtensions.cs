namespace HareDu.Extensions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Security;
using Model;

public static class ScopedParameterExtensions
{
    /// <summary>
    /// Provides extension methods for retrieving consumer information from the RabbitMQ broker via the HareDu library.
    /// </summary>
    /// <param name="factory">The broker factory that provides access to the RabbitMQ API.</param>
    extension([NotNull] IHareDuFactory factory)
    {
        /// <summary>
        /// Retrieves all scoped parameters configured on the RabbitMQ server for the given virtual hosts and components.
        /// </summary>
        /// <param name="credentials">The credential configuration for authenticating with the RabbitMQ server.</param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns>A task representing the asynchronous operation, containing the results with a list of scoped parameter information.</returns>
        /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
        /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
        /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
        [return: NotNull]
        public async Task<Results<ScopedParameterInfo>> GetAllScopedParameters(
            [NotNull] Action<HareDuCredentialProvider> credentials,
            [NotNull] CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(factory);

            return await factory
                .API<ScopedParameter>(credentials)
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a scoped parameter on the specified component and virtual host on the RabbitMQ server.
        /// </summary>
        /// <typeparam name="T">The type of the parameter value.</typeparam>
        /// <param name="credentials">The credential configuration for authenticating with the RabbitMQ server.</param>
        /// <param name="parameter">The name of the scoped parameter to be created.</param>
        /// <param name="value">The value to assign to the scoped parameter.</param>
        /// <param name="component">The component of the RabbitMQ server where the scoped parameter will be applied.</param>
        /// <param name="vhost">The virtual host within which the scoped parameter will be created.</param>
        /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
        /// <returns>A task representing the asynchronous operation, containing the result of the scoped parameter creation request.</returns>
        /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
        /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
        /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
        [return: NotNull]
        public async Task<Result> CreateScopeParameter<T>(
            [NotNull] Action<HareDuCredentialProvider> credentials,
            [NotNull] string parameter,
            [NotNull] T value,
            [NotNull] string component,
            [NotNull] string vhost,
            [NotNull] CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(factory);

            return await factory
                .API<ScopedParameter>(credentials)
                .Create(parameter, value, component, vhost, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes the specified scoped parameter from the RabbitMQ server.
        /// </summary>
        /// <param name="credentials">The action that provides the RabbitMQ credentials.</param>
        /// <param name="parameter">The name of the scoped parameter to be deleted.</param>
        /// <param name="component">The component associated with the scoped parameter.</param>
        /// <param name="vhost">The virtual host where the parameter exists.</param>
        /// <param name="cancellationToken">Token used to cancel the ongoing operation, if needed.</param>
        /// <returns>A task representing the asynchronous operation, containing the result of the scoped parameter deletion.</returns>
        /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
        /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
        /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
        [return: NotNull]
        public async Task<Result> DeleteScopedParameter(
            [NotNull] Action<HareDuCredentialProvider> credentials,
            [NotNull] string parameter,
            [NotNull] string component,
            [NotNull] string vhost,
            [NotNull] CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(factory);

            return await factory
                .API<ScopedParameter>(credentials)
                .Delete(parameter, component, vhost, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}