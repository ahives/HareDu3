namespace HareDu.Internal;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
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

    public async Task<Results<ConsumerInfo>> GetByVirtualHost(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            return Panic.Results<ConsumerInfo>("api/consumers/{vhost}", [new (){Reason = "The name of the virtual host is missing."}]);

        return await GetAllRequest<ConsumerInfo>($"api/consumers/{sanitizedVHost}", cancellationToken).ConfigureAwait(false);
    }
}