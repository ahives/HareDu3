namespace HareDu.Internal;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

class UserLimitsImpl :
    BaseBrokerImpl,
    UserLimits
{
    public UserLimitsImpl(IHttpClientFactory clientFactory) : base(clientFactory)
    {
    }

    public async Task<Result<UserLimitsInfo>> GetMaxConnections(string username, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(username))
            errors.Add(new (){Reason = "The username is missing."});

        string url = $"api/user-limits/user/{username}/max-connections";

        if (errors.Count > 0)
            return new FaultedResult<UserLimitsInfo> {DebugInfo = new (){URL = url, Errors = errors}};

        return await GetRequest<UserLimitsInfo>(url, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<UserLimitsInfo>> GetMaxChannels(string username, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(username))
            errors.Add(new (){Reason = "The username is missing."});

        string url = $"api/user-limits/user/{username}/max-channels";

        if (errors.Count > 0)
            return new FaultedResult<UserLimitsInfo> {DebugInfo = new (){URL = url, Errors = errors}};

        return await GetRequest<UserLimitsInfo>(url, cancellationToken).ConfigureAwait(false);
    }
}