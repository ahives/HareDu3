namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public static class BrokerSystemExtensions
{
    /// <summary>
    /// Returns various bits of random information that describe the RabbitMQ system.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result<BrokerOverviewInfo>> GetBrokerOverview(this IBrokerObjectFactory factory,
        CancellationToken cancellationToken = default)
    {
        if (factory is null)
            throw new ArgumentNullException(nameof(factory));

        return await factory
            .Object<Broker>()
            .GetOverview(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Rebalances all queues in all RabbitMQ virtual hosts.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result> RebalanceAllQueues(this IBrokerObjectFactory factory,
        CancellationToken cancellationToken = default)
    {
        if (factory is null)
            throw new ArgumentNullException(nameof(factory));

        return await factory
            .Object<Broker>()
            .RebalanceAllQueues(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Checks the RabbitMQ cluster to see if there are any alarms in effect.
    /// </summary>
    /// <param name="factory">The object factory that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if BrokerObjectFactory is null.</exception>
    public static async Task<Result<AlarmState>> IsAlarmsInEffect(this IBrokerObjectFactory factory, CancellationToken cancellationToken = default)
    {
        if (factory is null)
            throw new ArgumentNullException(nameof(factory));

        return await factory
            .Object<Broker>()
            .IsAlarmsInEffect(cancellationToken)
            .ConfigureAwait(false);
    }
}