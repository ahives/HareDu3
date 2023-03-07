namespace HareDu;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public interface Channel :
    BrokerAPI
{
    /// <summary>
    /// Returns all channels on the current RabbitMQ node.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result<IReadOnlyList<ChannelInfo>>> GetAll(CancellationToken cancellationToken = default);
}