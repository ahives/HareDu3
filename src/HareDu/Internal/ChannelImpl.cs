namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Model;

class ChannelImpl :
    BaseBrokerImpl,
    Channel
{
    public ChannelImpl(IHttpClientFactory clientFactory) :
        base(clientFactory)
    {
    }

    public async Task<Results<ChannelInfo>> GetAll(Action<PaginationConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string pagination = null;

        if (configurator != null)
        {
            var impl = new PaginationConfiguratorImpl();
            configurator?.Invoke(impl);

            pagination = impl.BuildPaginationParams();
        }

        string url = string.IsNullOrWhiteSpace(pagination) ? "api/channels" : $"api/channels?{pagination}";

        return await GetAllRequest<ChannelInfo>(url, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<ChannelInfo>> GetByConnection(string connectionName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(connectionName))
            errors.Add(new(){Reason = "Name of the connection is missing."});

        if (errors.Count > 0)
            return new FaultedResults<ChannelInfo>{DebugInfo = new (){URL = "/api/connections/{name}/channels", Errors = errors}};

        return await GetAllRequest<ChannelInfo>($"/api/connections/{connectionName}/channels", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<ChannelInfo>> GetByVirtualHost(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        if (errors.Count > 0)
            return new FaultedResults<ChannelInfo>{DebugInfo = new (){URL = "/api/vhosts/{vhost}/channels", Errors = errors}};

        return await GetAllRequest<ChannelInfo>($"/api/vhosts/{sanitizedVHost}/channels", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<ChannelInfo>> GetByName(string name, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        if (errors.Count > 0)
            return new FaultedResult<ChannelInfo>{DebugInfo = new (){URL = "/api/channels/channel", Errors = errors}};

        return await GetRequest<ChannelInfo>($"/api/channels/{name}", cancellationToken).ConfigureAwait(false);
    }
}