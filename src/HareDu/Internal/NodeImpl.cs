namespace HareDu.Internal;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

class NodeImpl :
    BaseBrokerImpl,
    Node
{
    public NodeImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<Results<NodeInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<NodeInfo>("api/nodes", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<NodeMemoryUsageInfo>> GetMemoryUsage(string node, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(node))
            return Response.Panic<NodeMemoryUsageInfo>("api/nodes/{node}/memory", [new(){Reason = "Name of the node for which to return memory usage data is missing."}]);

        return await GetRequest<NodeMemoryUsageInfo>($"api/nodes/{node}/memory", cancellationToken).ConfigureAwait(false);
    }
}