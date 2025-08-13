namespace HareDu.Internal;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;
using Serialization;

class ServerImpl :
    BaseBrokerImpl,
    Server
{
    public ServerImpl(HttpClient client)
        : base(client, Deserializer.Options)
    {
    }

    public async Task<Result<ServerInfo>> Get(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetRequest<ServerInfo>("api/definitions", RequestType.WebServer, cancellationToken).ConfigureAwait(false);
    }
}