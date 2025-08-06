namespace HareDu;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

/// <summary>
/// Represents an interface for interacting with RabbitMQ cluster nodes through the RabbitMQ Management API.
/// Allows retrieval of information on nodes and their respective memory usage.
/// </summary>
public interface Node :
    BrokerAPI
{
    /// <summary>
    /// Retrieves information for all nodes in the RabbitMQ cluster.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Results containing a collection of node information.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Results<NodeInfo>> GetAll([NotNull] CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves memory usage information for a specific node in the RabbitMQ cluster.
    /// </summary>
    /// <param name="node">The name of the node for which memory usage information is being requested.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result containing detailed memory usage information of the specified node.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result<NodeMemoryUsageInfo>> GetMemoryUsage([NotNull] string node, [NotNull] CancellationToken cancellationToken = default);
}
