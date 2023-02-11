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
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="shovel">The name of the dynamic shovel.</param>
    /// <param name="uri"></param>
    /// <param name="vhost">The virtual host where the shovel resides.</param>
    /// <param name="configurator">Describes how the dynamic shovel will be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result> CreateShovel(this IBrokerApiFactory factory,
        string shovel, string uri, string vhost, Action<ShovelConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Shovel>()
            .Create(shovel, uri, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a dynamic shovel on a specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="shovel">The name of the dynamic shovel.</param>
    /// <param name="vhost">The virtual host where the dynamic shovel resides.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result> DeleteShovel(this IBrokerApiFactory factory,
        string shovel, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
            
        return await factory
            .API<Shovel>()
            .Delete(shovel, vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes all dynamic shovels on a specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="vhost">The virtual host where the dynamic shovel resides.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<IReadOnlyList<Result>> DeleteAllShovels(this IBrokerApiFactory factory,
        string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
            
        var result = await factory.API<Shovel>()
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
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<ResultList<ShovelInfo>> GetAllShovels(this IBrokerApiFactory factory, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
            
        return await factory
            .API<Shovel>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }
}