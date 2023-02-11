namespace HareDu.Extensions;

using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class UserLimitsExtensions
{
    public static async Task<Result<UserLimitsInfo>> GetUserMaxChannels(this IBrokerApiFactory factory, string username,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<UserLimits>()
            .GetMaxChannels(username, cancellationToken)
            .ConfigureAwait(false);
    }
    
    public static async Task<Result<UserLimitsInfo>> GetUserMaxConnections(this IBrokerApiFactory factory, string username,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<UserLimits>()
            .GetMaxConnections(username, cancellationToken)
            .ConfigureAwait(false);
    }
}