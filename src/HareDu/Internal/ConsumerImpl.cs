namespace HareDu.Internal;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

class ConsumerImpl :
    BaseBrokerImpl,
    Consumer
{
    public ConsumerImpl(IHttpClientFactory clientFactory)
        : base(clientFactory)
    {
    }

    public async Task<Results<ConsumerInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<ConsumerInfo>("api/consumers", cancellationToken).ConfigureAwait(false);
    }
}