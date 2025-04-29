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
    public ChannelImpl(HttpClient client) :
        base(client)
    {
    }

    public async Task<Results<ChannelInfo>> GetAll(Action<PaginationConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string pagination = null;
        var errors = new List<Error>();

        if (configurator is not null)
        {
            var impl = new PaginationConfiguratorImpl();
            configurator?.Invoke(impl);

            pagination = impl.BuildPaginationParams();
            errors = impl.Validate();

            if (string.IsNullOrWhiteSpace(pagination))
                errors.Add(new() {Reason = "Pagination parameters are invalid."});
        }

        if (errors.Count > 0)
            return Panic.Results<ChannelInfo>("api/channels", errors);

        return await GetAllRequest<ChannelInfo>(
                string.IsNullOrWhiteSpace(pagination) ? "api/channels" : $"api/channels?{pagination}", cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Results<ChannelInfo>> GetByConnection(string connectionName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(connectionName))
            return Panic.Results<ChannelInfo>("api/connections/{name}/channels", [new(){Reason = "Name of the connection is missing."}]);

        return await GetAllRequest<ChannelInfo>($"/api/connections/{connectionName}/channels", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<ChannelInfo>> GetByVirtualHost(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            return Panic.Results<ChannelInfo>("api/vhosts/{vhost}/channels", [new (){Reason = "The name of the virtual host is missing."}]);

        return await GetAllRequest<ChannelInfo>($"/api/vhosts/{sanitizedVHost}/channels", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<ChannelInfo>> GetByName(string name, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(name))
            return Panic.Result<ChannelInfo>("api/channels/{name}", [new (){Reason = "The name of the virtual host is missing."}]);

        return await GetRequest<ChannelInfo>($"/api/channels/{name}", cancellationToken).ConfigureAwait(false);
    }
}