namespace HareDu.Internal;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Model;

class BrokerSystemImpl :
    BaseBrokerObject,
    BrokerSystem
{
    public BrokerSystemImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<Result<SystemOverviewInfo>> GetSystemOverview(CancellationToken cancellationToken = default)
    {
        cancellationToken.RequestCanceled();
            
        return await GetRequest<SystemOverviewInfo>("api/overview", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> RebalanceAllQueues(CancellationToken cancellationToken = default)
    {
        cancellationToken.RequestCanceled();
            
        return await PostEmptyRequest("api/rebalance/queues", cancellationToken).ConfigureAwait(false);
    }
}