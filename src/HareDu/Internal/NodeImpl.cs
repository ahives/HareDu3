namespace HareDu.Internal;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

class NodeImpl :
    BaseBrokerImpl,
    Node
{
    public NodeImpl(IHttpClientFactory clientFactory)
        : base(clientFactory)
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

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(node))
            errors.Add(new(){Reason = "Name of the node for which to return memory usage data is missing."});

        if (errors.Count > 0)
            return Panic.Result<NodeMemoryUsageInfo>("api/nodes/{node}/memory", errors);

        return await GetRequest<NodeMemoryUsageInfo>($"api/nodes/{node}/memory", cancellationToken).ConfigureAwait(false);
    }
}