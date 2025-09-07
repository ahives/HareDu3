namespace HareDu.Internal;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Model;

class ServerImpl :
    BaseHareDuImpl,
    Server
{
    public ServerImpl(HttpClient client, [FromKeyedServices("broker")] IHareDuDeserializer deserializer)
        : base(client, deserializer)
    {
    }

    public async Task<Result<ServerInfo>> Get(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetRequest<ServerInfo>("api/definitions", RequestType.WebServer, cancellationToken).ConfigureAwait(false);
    }
}