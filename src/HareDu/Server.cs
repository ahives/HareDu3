namespace HareDu;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

/// <summary>
/// Represents the contract for interacting with the RabbitMQ server to retrieve information about its configuration and resources.
/// </summary>
public interface Server :
    BrokerAPI
{
    /// <summary>
    /// Retrieves information about the RabbitMQ server, including details on users, virtual hosts, permissions, policies, parameters, queues, exchanges, bindings, and more.
    /// </summary>
    /// <param name="cancellationToken">
    /// A token that can be used to signal the cancellation of the asynchronous operation.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="Result{T}"/> object encapsulating the server information.
    /// </returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result<ServerInfo>> Get([NotNull] CancellationToken cancellationToken = default);
}