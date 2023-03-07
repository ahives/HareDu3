namespace HareDu.Internal;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

class ChannelImpl :
    BaseBrokerObject,
    Channel
{
    public ChannelImpl(HttpClient client) :
        base(client)
    {
    }

    public async Task<Result<IReadOnlyList<ChannelInfo>>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<ChannelInfo>("api/channels", cancellationToken).ConfigureAwait(false);
    }
}