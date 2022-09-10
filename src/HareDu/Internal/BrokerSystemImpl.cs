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

        string url = "api/overview";
            
        return await GetRequest<SystemOverviewInfo>(url, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> RebalanceAllQueues(CancellationToken cancellationToken = default)
    {
        cancellationToken.RequestCanceled();

        string url = "api/rebalance/queues";
            
        return await PostEmptyRequest(url, cancellationToken).ConfigureAwait(false);
    }
}