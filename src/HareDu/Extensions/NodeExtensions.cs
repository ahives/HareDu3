namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class NodeExtensions
{
    /// <summary>
    /// Retrieves information for all RabbitMQ nodes available in the cluster.
    /// </summary>
    /// <param name="factory">The factory interface used to access the underlying RabbitMQ node API.</param>
    /// <param name="cancellationToken">A token used to cancel the operation if needed.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="Results{NodeInfo}"/> object, which includes the retrieved data for all nodes in the cluster.
    /// </returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Results<NodeInfo>> GetAllNodes(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Node>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves memory usage information for a specific RabbitMQ node in the cluster.
    /// </summary>
    /// <param name="factory">The factory interface used to access the underlying RabbitMQ node API.</param>
    /// <param name="node">The name of the RabbitMQ node for which memory usage information is being requested.</param>
    /// <param name="cancellationToken">A token used to cancel the operation if needed.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="Result{NodeMemoryUsageInfo}"/> object, which includes memory usage details for the specified node.
    /// </returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    public static async Task<Result<NodeMemoryUsageInfo>> GetNodeMemoryUsage(this IBrokerFactory factory,
        string node, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Node>()
            .GetMemoryUsage(node, cancellationToken)
            .ConfigureAwait(false);
    }
}