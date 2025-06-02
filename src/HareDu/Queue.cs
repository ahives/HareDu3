namespace HareDu;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

/// <summary>
/// Provides methods for managing and interacting with message queues in a message broker.
/// </summary>
public interface Queue :
    BrokerAPI
{
    /// <summary>
    /// Retrieves information about all queues available on the RabbitMQ broker.
    /// </summary>
    /// <param name="pagination">Specifies the pagination options for filtering the queues to retrieve, if applicable.</param>
    /// <param name="cancellationToken">Token used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation and contains the results of type <see cref="Results{QueueInfo}"/> representing the queues.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Results<QueueInfo>> GetAll(Action<PaginationConfigurator> pagination = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves detailed information for all queues available on the RabbitMQ broker.
    /// </summary>
    /// <param name="cancellationToken">Token used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation and contains the results of type <see cref="Results{QueueDetailInfo}"/> representing the detailed information for the queues.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Results<QueueDetailInfo>> GetDetails(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new queue on the RabbitMQ broker.
    /// </summary>
    /// <param name="name">Specifies the name of the queue to be created.</param>
    /// <param name="vhost">Defines the virtual host where the queue will be created.</param>
    /// <param name="node">Specifies the node where the queue will be located.</param>
    /// <param name="configurator">Optional action to configure additional properties of the queue.</param>
    /// <param name="cancellationToken">Token used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation and contains the result of type <see cref="Result"/> indicating the success or failure of the operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result> Create(string name, string vhost, string node, Action<QueueConfigurator> configurator = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a specific queue on the RabbitMQ broker.
    /// </summary>
    /// <param name="name">The name of the queue to be deleted.</param>
    /// <param name="vhost">The name of the virtual host where the queue resides.</param>
    /// <param name="configurator">Specifies additional configuration options for deleting the queue, if applicable.</param>
    /// <param name="cancellationToken">Token used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation and contains the result of the delete operation of type <see cref="Result"/>.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result> Delete(string name, string vhost, Action<QueueDeletionConfigurator> configurator = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes all messages from a specified queue in a RabbitMQ virtual host.
    /// </summary>
    /// <param name="name">The name of the queue to be emptied.</param>
    /// <param name="vhost">The name of the virtual host where the queue resides.</param>
    /// <param name="cancellationToken">Token used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation and contains the result of type <see cref="Result"/> representing the outcome of the operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result> Empty(string name, string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Synchronizes the state of a specific queue on the RabbitMQ broker.
    /// </summary>
    /// <param name="name">The name of the queue to synchronize.</param>
    /// <param name="vhost">The virtual host where the queue resides.</param>
    /// <param name="cancellationToken">Token used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation and contains a <see cref="Result"/> indicating the outcome of the synchronization process.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result> Sync(string name, string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels synchronization of the specified queue in the given virtual host.
    /// </summary>
    /// <param name="name">The name of the queue to cancel synchronization for.</param>
    /// <param name="vhost">The name of the virtual host where the queue resides.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the result of the operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result> CancelSync(string name, string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Binds a queue to a specified exchange within a virtual host on the RabbitMQ broker.
    /// </summary>
    /// <param name="vhost">The name of the virtual host where the queue and exchange are located.</param>
    /// <param name="exchange">The name of the exchange to bind the queue to.</param>
    /// <param name="configurator">Specifies the configuration options for the binding, such as arguments and binding key.</param>
    /// <param name="cancellationToken">Token used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation and contains the result of type <see cref="Result{T}"/> representing the created binding information.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result<BindingInfo>> BindToQueue(string vhost, string exchange, Action<BindingConfigurator> configurator,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a binding between queues in the specified virtual host.
    /// </summary>
    /// <param name="vhost">The virtual host where the binding exists.</param>
    /// <param name="configurator">Specifies the configuration options for unbinding queues.</param>
    /// <param name="cancellationToken">Token used to propagate notifications that the operation should be canceled.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the unbinding operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result> Unbind(string vhost, Action<UnbindingConfigurator> configurator, CancellationToken cancellationToken = default);
}