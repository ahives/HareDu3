namespace HareDu.Internal;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

class ConsumerImpl :
    BaseBrokerObject,
    Consumer
{
    public ConsumerImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<ResultList<ConsumerInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
            
        return await GetAllRequest<ConsumerInfo>("api/consumers", cancellationToken).ConfigureAwait(false);
    }
}