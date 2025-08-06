namespace HareDu.Extensions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Security;
using Model;

public static class ServerExtensions
{
    /// <summary>
    /// Retrieves information about the server including details such as RabbitMQ version, users, virtual hosts, permissions, policies, queues, exchanges, and bindings.
    /// </summary>
    /// <param name="factory">The broker factory instance to execute the API call.</param>
    /// <param name="credentials">The credential provider used for authentication.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{T}"/> where T is <see cref="ServerInfo"/> with the server information.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result<ServerInfo>> GetServerInformation(
        [NotNull] this IBrokerFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Server>(credentials)
            .Get(cancellationToken)
            .ConfigureAwait(false);
    }
}