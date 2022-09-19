namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public static class UserLimitsExtensions
{
    public static async Task<Result<UserLimitsInfo>> GetUserMaxChannels(this IBrokerObjectFactory factory, string username,
        CancellationToken cancellationToken = default)
    {
        if (factory is null)
            throw new ArgumentNullException(nameof(factory));

        return await factory
            .Object<UserLimits>()
            .GetMaxChannels(username, cancellationToken)
            .ConfigureAwait(false);
    }
    
    public static async Task<Result<UserLimitsInfo>> GetUserMaxConnections(this IBrokerObjectFactory factory, string username,
        CancellationToken cancellationToken = default)
    {
        if (factory is null)
            throw new ArgumentNullException(nameof(factory));

        return await factory
            .Object<UserLimits>()
            .GetMaxConnections(username, cancellationToken)
            .ConfigureAwait(false);
    }
}