namespace HareDu;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

/// <summary>
/// Represents operations that can be performed on broker connections.
/// </summary>
public interface Connection :
    BrokerAPI
{
    /// <summary>
    /// Retrieves all connections from the broker, with optional pagination configuration.
    /// </summary>
    /// <param name="pagination">Optional pagination parameters (e.g., page number, size, etc.) to control the scope of returned data.</param>
    /// <param name="cancellationToken">Token used to cancel the ongoing operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the results of connection information.</returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled via the provided cancellation token.</exception>
    Task<Results<ConnectionInfo>> GetAll(Action<PaginationConfigurator> pagination = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all connections associated with the specified virtual host.
    /// </summary>
    /// <param name="vhost">The name of the virtual host for which connections are to be retrieved.</param>
    /// <param name="pagination">Optional pagination parameters (e.g., page number, size, etc.) used to control how much data is returned.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the asynchronous operation, containing the results of connections information for the specified virtual host.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Results<ConnectionInfo>> GetByVirtualHost(string vhost, Action<PaginationConfigurator> pagination = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves connection information for a specific connection by its name.
    /// </summary>
    /// <param name="name">The name of the connection to retrieve information for.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Returns a collection of connection details matching the specified name.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Results<ConnectionInfo>> GetByName(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all connections associated with the specified username.
    /// </summary>
    /// <param name="username">The username for which connections will be retrieved.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the asynchronous operation, containing the results of the connections associated with the specified username.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Results<ConnectionInfo>> GetByUser(string username, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an active connection on the current RabbitMQ node.
    /// </summary>
    /// <param name="connection">The name of the RabbitMQ connection to be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result object indicating the success or failure of the delete operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result> Delete(string connection, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes all connections associated with the specified user on the current RabbitMQ node.
    /// </summary>
    /// <param name="username">The username whose associated connections need to be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A result indicating the success or failure of the deletion operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result> DeleteByUser(string username, CancellationToken cancellationToken = default);
}