namespace HareDu;

using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public interface Server :
    BrokerAPI
{
    /// <summary>
    /// Returns all object definitions on the current RabbitMQ node.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    Task<Result<ServerInfo>> Get(CancellationToken cancellationToken = default);
}