namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Configuration;
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
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result<ServerInfo>> GetServerInformation(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<Server>(credentials)
            .Get(cancellationToken)
            .ConfigureAwait(false);
    }
}