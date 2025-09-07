namespace HareDu.Shovel;

using Core;
using Model;

/// <summary>
/// Represents the Shovel management functionalities within a RabbitMQ broker.
/// Provides operations to retrieve, create, and delete dynamic shovels.
/// </summary>
public interface Shovel :
    HareDuAPI
{
    /// <summary>
    /// Retrieves all the dynamic shovels configured in the RabbitMQ broker.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>
    /// An asynchronous task that returns the collection of shovels, along with the operation's result status.
    /// </returns>
    /// <exception cref="System.OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Results<ShovelInfo>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all shovels from the specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="vhost">The name of the virtual host from which all shovels will be retrieved.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>
    /// An asynchronous task that returns the operation's result, containing a collection of <see cref="ShovelInfo"/> objects that represent the retrieved shovels
    /// and information about any faults encountered during the operation.
    /// </returns>
    /// <exception cref="System.OperationCanceledException">Throws if the thread has a cancellation request during the operation.</exception>
    Task<Results<ShovelInfo>> GetAll(string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the details of a specific dynamic shovel configured in the RabbitMQ broker.
    /// </summary>
    /// <param name="name">The name of the shovel to retrieve.</param>
    /// <param name="vhost">The virtual host where the shovel is located.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>
    /// An asynchronous task that returns the details of the requested shovel, including the operation's result status.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">Throws if the name or vhost parameters are null.</exception>
    /// <exception cref="System.OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result<ShovelInfo>> Get(string name, string vhost, CancellationToken cancellationToken = default);

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
    /// <exception cref="System.OperationCanceledException">Throws if the thread has a cancellation request during the operation.</exception>
    Task<Result> Create(
        string name,
        string vhost,
        Action<ShovelConfigurator> configurator,
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
    /// <exception cref="System.OperationCanceledException">Thrown if the operation is canceled.</exception>
    Task<Result> Delete(string name, string vhost, CancellationToken cancellationToken = default);

    /// <summary>
    /// Restarts the specified dynamic shovel within a specific virtual host in the RabbitMQ broker.
    /// </summary>
    /// <param name="name">The name of the shovel to restart.</param>
    /// <param name="vhost">The name of the virtual host where the shovel is located.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>
    /// An asynchronous task that represents the result of the restart operation.
    /// </returns>
    /// <exception cref="System.OperationCanceledException">Throws if the thread has a cancellation request before or during the process.</exception>
    Task<Result> Restart(string name, string vhost, CancellationToken cancellationToken = default);
}