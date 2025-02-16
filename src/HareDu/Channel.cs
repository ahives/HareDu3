namespace HareDu;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Internal;
using Model;

public interface Channel :
    BrokerAPI
{
    /// <summary>
    /// Returns all channels on the current RabbitMQ node.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Results<ChannelInfo>> GetAll(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Returns all channels on the current RabbitMQ node given the specified pagination parameters.
    /// </summary>
    /// <param name="configurator"></param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Results<ChannelInfo>> GetAll(Action<PaginationConfigurator> configurator, CancellationToken cancellationToken = default);
}