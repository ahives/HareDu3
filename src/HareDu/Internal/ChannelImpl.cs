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

        return errors.HaveBeenFound()
            ? Responses.Panic<ChannelInfo>(Debug.Info("api/channels", errors))
            : await GetAllRequest<ChannelInfo>(
                    string.IsNullOrWhiteSpace(@params) ? "api/channels" : $"api/channels?{@params}", RequestType.Channel, cancellationToken)
                .ConfigureAwait(false);
    }

    public async Task<Results<ChannelInfo>> GetByConnection(string connectionName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return string.IsNullOrWhiteSpace(connectionName)
            ? Responses.Panic<ChannelInfo>(Debug.Info("api/connections/{name}/channels",
                Errors.Create(e => { e.Add("Name of the connection is missing."); })))
            : await GetAllRequest<ChannelInfo>($"/api/connections/{connectionName}/channels", RequestType.Channel, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<ChannelInfo>> GetByVirtualHost(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();

        return string.IsNullOrWhiteSpace(sanitizedVHost)
            ? Responses.Panic<ChannelInfo>(Debug.Info("api/vhosts/{vhost}/channels",
                Errors.Create(e => { e.Add("The name of the virtual host is missing.", RequestType.Channel); })))
            : await GetAllRequest<ChannelInfo>($"/api/vhosts/{sanitizedVHost}/channels", RequestType.Channel, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<ChannelInfo>> GetByName(string name, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return string.IsNullOrWhiteSpace(name)
            ? Response.Panic<ChannelInfo>(Debug.Info("api/channels/{name}",
                Errors.Create(e => { e.Add("The name of the virtual host is missing.", RequestType.Channel); })))
            : await GetRequest<ChannelInfo>($"/api/channels/{name}", RequestType.Channel, cancellationToken).ConfigureAwait(false);
    }
}