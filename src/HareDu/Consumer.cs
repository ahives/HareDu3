namespace HareDu;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public interface Consumer :
    BrokerAPI
{
    /// <summary>
    /// Returns all consumers on the current RabbitMQ node.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result<IReadOnlyList<ConsumerInfo>>> GetAll(CancellationToken cancellationToken = default);
}