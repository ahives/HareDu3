namespace HareDu.Extensions;

using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class UserLimitsExtensions
{
    /// <summary>
    /// Returns information about user limits for connections and channels on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<Result<UserLimitsInfo>> GetUserMaxChannels(this IBrokerFactory factory, string username,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<UserLimits>()
            .GetMaxChannels(username, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="username"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<Result<UserLimitsInfo>> GetUserMaxConnections(this IBrokerFactory factory, string username,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<UserLimits>()
            .GetMaxConnections(username, cancellationToken)
            .ConfigureAwait(false);
    }
}