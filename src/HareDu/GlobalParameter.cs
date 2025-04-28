namespace HareDu;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

/// <summary>
/// Provides functionality for managing global parameters in RabbitMQ.
/// </summary>
public interface GlobalParameter :
    BrokerAPI
{
    /// <summary>
    /// Returns all global parameters on the current RabbitMQ node.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Results<GlobalParameterInfo>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a global parameter on the RabbitMQ node with the specified name and configuration.
    /// </summary>
    /// <param name="parameter">The name of the global parameter to be created.</param>
    /// <param name="configurator">The configuration action to define the parameter settings.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>An asynchronous task of <see cref="Result"/> indicating the status of the operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    Task<Result> Create(string parameter, Action<GlobalParameterConfigurator> configurator, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified global parameter from the RabbitMQ node.
    /// </summary>
    /// <param name="parameter">The name of the global parameter to be deleted.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task that represents the asynchronous operation and contains the result of the delete operation.</returns>
    /// <exception cref="OperationCanceledException">Throws if the operation is canceled.</exception>
    Task<Result> Delete(string parameter, CancellationToken cancellationToken = default);
}