namespace HareDu;

using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public interface UserLimits :
    BrokerObject
{
    /// <summary>
    /// Returns information about user limits for connections and channels on the current RabbitMQ server.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{TResult}"/></returns>
    Task<Result<UserLimitsInfo>> GetMaxConnections(string username, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns information about user limits for connections and channels on the current RabbitMQ server.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="HareDu.Core.Result{TResult}"/></returns>
    Task<Result<UserLimitsInfo>> GetMaxChannels(string username, CancellationToken cancellationToken = default);
}