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
    public ConsumerImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<Results<ConsumerInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<ConsumerInfo>("api/consumers", RequestType.Consumer, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<ConsumerInfo>> GetByVirtualHost(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            return Responses.Panic<ConsumerInfo>("api/consumers/{vhost}",
                Errors.Create(e => { e.Add("The name of the virtual host is missing.", RequestType.Connection); }));

        return await GetAllRequest<ConsumerInfo>($"api/consumers/{sanitizedVHost}", RequestType.Consumer, cancellationToken).ConfigureAwait(false);
    }
}