namespace HareDu;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

/// <summary>
/// Represents operations that can be performed on RabbitMQ channels.
/// </summary>
public interface Channel :
    BrokerAPI
{
    /// <summary>
    /// Retrieves all channels on the current RabbitMQ node with the ability to configure pagination.
    /// </summary>
    /// <param name="pagination">Optional action to configure the pagination for retrieving the channels.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>All channels on the current RabbitMQ node as a result set.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Results<ChannelInfo>> GetAll([AllowNull] Action<PaginationConfigurator> pagination = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all channels associated with the specified connection.
    /// </summary>
    /// <param name="connectionName">The name of the connection for which to retrieve the channels.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result set containing information about the channels associated with the specified connection.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Results<ChannelInfo>> GetByConnection([NotNull] string connectionName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all channels associated with a specific virtual host.
    /// </summary>
    /// <param name="vhost">The name of the virtual host whose channels are to be retrieved.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result set containing all channels associated with the specified virtual host.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Results<ChannelInfo>> GetByVirtualHost([NotNull] string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves details of a specific RabbitMQ channel based on its name.
    /// </summary>
    /// <param name="name">The unique name of the channel to retrieve.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result containing the details of the specified RabbitMQ channel.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    [return: NotNull]
    Task<Result<ChannelInfo>> GetByName([NotNull] string name, CancellationToken cancellationToken = default);
}