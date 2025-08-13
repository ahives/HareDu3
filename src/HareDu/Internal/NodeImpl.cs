namespace HareDu.Internal;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;
using Serialization;

class NodeImpl :
    BaseBrokerImpl,
    Node
{
    public NodeImpl(HttpClient client)
        : base(client, Deserializer.Options)
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