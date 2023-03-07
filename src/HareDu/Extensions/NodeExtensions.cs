namespace HareDu.Extensions;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class NodeExtensions
{
    /// <summary>
    /// Returns all nodes on the current RabbitMQ cluster.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result<IReadOnlyList<NodeInfo>>> GetAllNodes(this IBrokerApiFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Node>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Returns the memory usage information on the specified RabbitMQ node.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="node">Name of the RabbitMQ node.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result<NodeMemoryUsageInfo>> GetNodeMemoryUsage(this IBrokerApiFactory factory,
        string node, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Node>()
            .GetMemoryUsage(node, cancellationToken)
            .ConfigureAwait(false);
    }
}