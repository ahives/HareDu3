namespace HareDu.Internal;

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

class NodeImpl :
    BaseBrokerObject,
    Node
{
    public NodeImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<ResultList<NodeInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
            
        return await GetAllRequest<NodeInfo>("api/nodes", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<NodeHealthInfo>> GetHealth(string node = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetRequest<NodeHealthInfo>(
            string.IsNullOrWhiteSpace(node) ? "api/healthchecks/node" : $"/api/healthchecks/node/{node}",
            cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<NodeMemoryUsageInfo>> GetMemoryUsage(string node, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(node))
            errors.Add(new(){Reason = "Name of the node for which to return memory usage data is missing."});
            
        string url = $"api/nodes/{node}/memory";
            
        if (errors.Any())
            return new FaultedResult<NodeMemoryUsageInfo>{DebugInfo = new (){URL = url, Errors = errors}};
            
        return await GetRequest<NodeMemoryUsageInfo>(url, cancellationToken).ConfigureAwait(false);
    }
}