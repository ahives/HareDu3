namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

class ChannelImpl :
    BaseBrokerObject,
    Channel
{
    public ChannelImpl(HttpClient client) :
        base(client)
    {
    }

    public async Task<Results<ChannelInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<ChannelInfo>("api/channels", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<ChannelInfo>> GetAll(Action<PaginationConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var impl = new PaginationConfiguratorImpl();
        configurator?.Invoke(impl);

        string request = impl.GetPagination();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(request))
            errors.Add(new(){Reason = "Name of the node for which to return memory usage data is missing."});

        string url = string.IsNullOrWhiteSpace(request) ? "api/channels" : $"api/channels?{request}";

        if (errors.Count > 0)
            return new FaultedResults<ChannelInfo>{DebugInfo = new (){URL = url, Errors = errors}};

        return await GetAllRequest<ChannelInfo>(url, cancellationToken).ConfigureAwait(false);
    }
}