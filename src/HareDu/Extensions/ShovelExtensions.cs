namespace HareDu.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Extensions;
using Core.Security;
using Model;

public static class ShovelExtensions
{
    /// <summary>
    /// Creates a dynamic shovel in the specified virtual host on the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The broker factory instance used to communicate with the RabbitMQ broker.</param>
    /// <param name="credentials">The credential provider used to authenticate with the RabbitMQ broker.</param>
    /// <param name="name">The name of the shovel to be created.</param>
    /// <param name="vhost">The virtual host where the shovel will be created.</param>
    /// <param name="configurator">An optional configurator to define specific shovel settings.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation and contains the result of type <see cref="Result"/> indicating the outcome of the operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> CreateShovel(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string name, string vhost,
        Action<ShovelConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Shovel>(credentials)
            .Create(name, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a specific dynamic shovel in the specified virtual host from the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The broker factory instance used to communicate with the RabbitMQ broker.</param>
    /// <param name="credentials">The credential provider used to authenticate with the RabbitMQ broker.</param>
    /// <param name="name">The name of the shovel to be deleted.</param>
    /// <param name="vhost">The virtual host where the shovel to be deleted is located.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation and contains the result of type <see cref="Result"/> indicating the outcome of the operation.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Result> DeleteShovel(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string name, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Shovel>(credentials)
            .Delete(name, vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes all dynamic shovels for the specified virtual host from the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The broker factory instance used to communicate with the RabbitMQ broker.</param>
    /// <param name="credentials">The credential provider used to authenticate with the RabbitMQ broker.</param>
    /// <param name="vhost">The virtual host on which the shovels to be deleted are located.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation and contains the results of type <see cref="IReadOnlyList{Result}"/> indicating the outcome of each shovel deletion.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<IReadOnlyList<Result>> DeleteAllShovels(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        var result = await factory
            .API<Shovel>(credentials)
            .GetAll(cancellationToken)
            .ConfigureAwait(false);

        if (result.HasFaulted)
            return new List<Result>();
            
        var shovels = result
            .Select(x => x.Data)
            .Where(x => x.VirtualHost == vhost && x.Type == ShovelType.Dynamic)
            .ToList();

        var results = new List<Result>();
            
        foreach (var shovel in shovels)
        {
            var deleteResult = await factory
                .API<Shovel>(credentials)
                .Delete(shovel.Name, vhost, cancellationToken)
                .ConfigureAwait(false);
                
            results.Add(deleteResult);
        }

        return results;
    }

    /// <summary>
    /// Retrieves all shovels from the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The broker factory instance used to communicate with the RabbitMQ broker.</param>
    /// <param name="credentials">The credential provider used to authenticate with the RabbitMQ broker.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation and contains the results of type <see cref="Results{ShovelInfo}"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Results<ShovelInfo>> GetAllShovels(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Shovel>(credentials)
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }
}