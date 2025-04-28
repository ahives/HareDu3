namespace HareDu;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

/// <summary>
/// Represents the Shovel management functionalities within a RabbitMQ broker.
/// Provides operations to retrieve, create, and delete dynamic shovels.
/// </summary>
public interface Shovel :
    BrokerAPI
{
    /// <summary>
    /// Retrieves all the dynamic shovels configured in the RabbitMQ broker.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>
    /// An asynchronous task that returns the collection of shovels, along with the operation's result status.
    /// </returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Results<ShovelInfo>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new dynamic shovel in the RabbitMQ broker with the specified configuration.
    /// </summary>
    /// <param name="name">The name of the shovel to be created.</param>
    /// <param name="vhost">The name of the virtual host where the shovel will be created.</param>
    /// <param name="configurator">An action that applies configuration settings to the shovel using the <see cref="ShovelConfigurator"/>.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>
    /// An asynchronous task that returns the operation's result, which contains information about any faults encountered during the operation.
    /// </returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request during the operation.</exception>
    Task<Result> Create(string name, string vhost, Action<ShovelConfigurator> configurator,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified dynamic shovel configured in the RabbitMQ broker.
    /// </summary>
    /// <param name="name">The name of the shovel to be deleted.</param>
    /// <param name="vhost">The virtual host of the shovel to be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>
    /// An asynchronous task that returns the result of the delete operation, including its status.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<Result> Delete(string name, string vhost, CancellationToken cancellationToken = default);
}