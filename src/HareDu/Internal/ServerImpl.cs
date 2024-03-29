namespace HareDu.Internal;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Model;

class ServerImpl :
    BaseBrokerObject,
    Server
{
    public ServerImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<Result<ServerInfo>> Get(CancellationToken cancellationToken = default)
    {
        cancellationToken.RequestCanceled();
            
        return await GetRequest<ServerInfo>("api/definitions", cancellationToken).ConfigureAwait(false);
    }
}