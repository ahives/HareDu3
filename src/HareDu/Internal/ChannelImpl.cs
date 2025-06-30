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

    public async Task<Results<ChannelInfo>> GetAll(Action<PaginationConfigurator> pagination = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string @params = null;
        var errors = new List<Error>();

        if (pagination is not null)
        {
            var impl = new PaginationConfiguratorImpl();
            pagination(impl);

            @params = impl.BuildPaginationParams();
            errors = impl.Validate();

            errors.AddIfTrue(@params, string.IsNullOrWhiteSpace, Errors.Create("Pagination parameters are in valid."));
        }

        if (errors.HaveBeenFound())
            return Responses.Panic<ChannelInfo>("api/channels", errors);

        return await GetAllRequest<ChannelInfo>(
                string.IsNullOrWhiteSpace(@params) ? "api/channels" : $"api/channels?{@params}", cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Results<ChannelInfo>> GetByConnection(string connectionName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(connectionName))
            return Responses.Panic<ChannelInfo>("api/connections/{name}/channels", [Errors.Create("Name of the connection is missing.")]);

        return await GetAllRequest<ChannelInfo>($"/api/connections/{connectionName}/channels", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<ChannelInfo>> GetByVirtualHost(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            return Responses.Panic<ChannelInfo>("api/vhosts/{vhost}/channels", [Errors.Create("The name of the virtual host is missing.")]);

        return await GetAllRequest<ChannelInfo>($"/api/vhosts/{sanitizedVHost}/channels", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<ChannelInfo>> GetByName(string name, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(name))
            return Response.Panic<ChannelInfo>("api/channels/{name}", [Errors.Create("The name of the virtual host is missing.")]);

        return await GetRequest<ChannelInfo>($"/api/channels/{name}", cancellationToken).ConfigureAwait(false);
    }
}