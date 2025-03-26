namespace HareDu.Internal;

using System.Collections.Generic;
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

        var errors = new List<Error>();

        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        if (errors.Count > 0)
            return new FaultedResults<ConsumerInfo>{DebugInfo = new (){URL = "api/consumers/{vhost}", Errors = errors}};

        return await GetAllRequest<ConsumerInfo>($"api/consumers/{sanitizedVHost}", cancellationToken).ConfigureAwait(false);
    }
}