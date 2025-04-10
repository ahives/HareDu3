namespace HareDu;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public interface GlobalParameter :
    BrokerAPI
{
    /// <summary>
    /// Returns all global parameters on the current RabbitMQ node.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
    Task<Results<GlobalParameterInfo>> GetAll(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates the specified global parameter on the current RabbitMQ node.
    /// </summary>
    /// <param name="parameter">Name of the RabbitMQ parameter.</param>
    /// <param name="configurator">Describes how the RabbitMQ parameter is to be defined.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Create(string parameter, Action<GlobalParameterConfigurator> configurator, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified global parameter on the current RabbitMQ node.
    /// </summary>
    /// <param name="parameter">Name of the RabbitMQ parameter.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result> Delete(string parameter, CancellationToken cancellationToken = default);
}