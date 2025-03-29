namespace HareDu.Extensions;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class VirtualHostExtensions
{
    /// <summary>
    /// Returns information about each virtual host on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a virtual host.</exception>
    public static async Task<Results<VirtualHostInfo>> GetAllVirtualHosts(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates the specified RabbitMQ virtual host on the current server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="configurator">Describes how the virtual host will be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a virtual host.</exception>
    public static async Task<Result> CreateVirtualHost(this IBrokerFactory factory,
        string vhost, Action<VirtualHostConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .Create(vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the specified RabbitMQ virtual host on the current server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a virtual host.</exception>
    public static async Task<Result> DeleteVirtualHost(this IBrokerFactory factory,
        string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .Delete(vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Starts up the specified RabbitMQ virtual host on the specified node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="node">Name of the RabbitMQ server node.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a virtual host.</exception>
    public static async Task<Result> StartupVirtualHost(this IBrokerFactory factory,
        string vhost, string node, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .Startup(vhost, node, cancellationToken)
            .ConfigureAwait(false);
    }
 
    /// <summary>
    /// Returns limit information about each virtual host on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a virtual host.</exception>
    public static async Task<Results<VirtualHostLimitsInfo>> GetAllVirtualHostLimits(
        this IBrokerFactory factory, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .GetAllLimits(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Defines specified limits on the RabbitMQ virtual host on the current server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="configurator">Describes how the virtual host limits will be defined.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a virtual host.</exception>
    public static async Task<Result> DefineVirtualHostLimit(this IBrokerFactory factory, string vhost,
        Action<VirtualHostLimitsConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .DefineLimit(vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the limits for the specified virtual host on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="limit"></param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a virtual host.</exception>
    public static async Task<Result> DeleteVirtualHostLimit(this IBrokerFactory factory,
        string vhost, VirtualHostLimit limit, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .DeleteLimit(vhost, limit, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a virtual host.</exception>
    public static async Task<Results<VirtualHostPermissionInfo>> GetVirtualHostPermissions(this IBrokerFactory factory,
        string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .GetPermissions(vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// List all topic permissions on the virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost">Name of the RabbitMQ broker virtual host.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a virtual host.</exception>
    public static async Task<Results<VirtualHostTopicPermissionInfo>> GetVirtualHostTopicPermissions(this IBrokerFactory factory,
        string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<VirtualHost>()
            .GetTopicPermissions(vhost, cancellationToken)
            .ConfigureAwait(false);
    }
}