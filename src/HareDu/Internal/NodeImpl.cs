namespace HareDu.Internal;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Model;

class NodeImpl :
    BaseHareDuImpl,
    Node
{
    public NodeImpl(HttpClient client, [FromKeyedServices("broker")] IHareDuDeserializer deserializer)
        : base(client, deserializer)
    {
    }

    public async Task<Results<NodeInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<NodeInfo>("api/nodes", RequestType.Node, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<NodeMemoryUsageInfo>> GetMemoryUsage(string node, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return string.IsNullOrWhiteSpace(node)
            ? Response.Panic<NodeMemoryUsageInfo>(Debug.Info("api/nodes/{node}/memory",
                    Errors.Create(e => { e.Add("Name of the node for which to return memory usage data is missing.", RequestType.Node); })))
            : await GetRequest<NodeMemoryUsageInfo>($"api/nodes/{node}/memory", RequestType.Node, cancellationToken).ConfigureAwait(false);
    }
}