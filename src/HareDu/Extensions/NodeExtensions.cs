namespace HareDu.Extensions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Security;
using Model;

public static class NodeExtensions
{
    /// <summary>
    /// Provides extension methods for retrieving consumer information from the RabbitMQ broker via the HareDu library.
    /// </summary>
    /// <param name="factory">The broker factory that provides access to the RabbitMQ API.</param>
    extension([NotNull] IHareDuFactory factory)
    {
        /// <summary>
        /// Retrieves information about all nodes in the RabbitMQ cluster.
        /// </summary>
        /// <param name="credentials">The delegate used to configure credentials for accessing the API.</param>
        /// <param name="cancellationToken">A token used to cancel the operation if needed.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Results{NodeInfo}"/> object, which includes details for all nodes in the cluster.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
        /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
        /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
        [return: NotNull]
        public async Task<Results<NodeInfo>> GetAllNodes(
            [NotNull] Action<HareDuCredentialProvider> credentials,
            [NotNull] CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(factory);

            return await factory
                .API<Node>(credentials)
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves memory usage information for a specific node in the RabbitMQ cluster.
        /// </summary>
        /// <param name="credentials">The delegate used to configure credentials for accessing the API.</param>
        /// <param name="node">The identifier of the node for which memory usage information is retrieved.</param>
        /// <param name="cancellationToken">A token used to cancel the operation if needed.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Result{T}"/> object providing memory usage details for the specified node.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
        /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
        /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
        [return: NotNull]
        public async Task<Result<NodeMemoryUsageInfo>> GetNodeMemoryUsage(
            [NotNull] Action<HareDuCredentialProvider> credentials,
            [NotNull] string node,
            [NotNull] CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(factory);

            return await factory
                .API<Node>(credentials)
                .GetMemoryUsage(node, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}