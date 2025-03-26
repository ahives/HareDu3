namespace HareDu.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Extensions;
using Model;

public static class ShovelExtensions
{
    /// <summary>
    /// Creates a dynamic shovel on a specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="name">The name of the dynamic shovel.</param>
    /// <param name="uri"></param>
    /// <param name="vhost">The virtual host where the shovel resides.</param>
    /// <param name="configurator">Describes how the dynamic shovel will be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a shovel.</exception>
    public static async Task<Result> CreateShovel(this IBrokerFactory factory,
        string name, string vhost, Action<ShovelConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Shovel>()
            .Create(name, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a dynamic shovel on a specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="name">The name of the dynamic shovel.</param>
    /// <param name="vhost">The virtual host where the dynamic shovel resides.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a shovel.</exception>
    public static async Task<Result> DeleteShovel(this IBrokerFactory factory,
        string name, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
            
        return await factory
            .API<Shovel>()
            .Delete(name, vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes all dynamic shovels on a specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost">The virtual host where the dynamic shovel resides.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a shovel.</exception>
    public static async Task<IReadOnlyList<Result>> DeleteAllShovels(this IBrokerFactory factory,
        string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
            
        var result = await factory
            .API<Shovel>()
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
                .API<Shovel>()
                .Delete(shovel.Name, vhost, cancellationToken)
                .ConfigureAwait(false);
                
            results.Add(deleteResult);
        }

        return results;
    }
        
    /// <summary>
    /// Returns all dynamic shovels that have been created.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a shovel.</exception>
    public static async Task<Results<ShovelInfo>> GetAllShovels(this IBrokerFactory factory, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Shovel>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }
}