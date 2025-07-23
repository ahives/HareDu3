namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Model;

class ConnectionImpl :
    BaseBrokerImpl,
    Connection
{
    public ConnectionImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<Results<ConnectionInfo>> GetAll(Action<PaginationConfigurator> pagination = null, CancellationToken cancellationToken = default)
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
            return Responses.Panic<ConnectionInfo>("api/queues", errors);

        string url = string.IsNullOrWhiteSpace(@params) ? "api/connections" : $"api/connections?{@params}";

        return await GetAllRequest<ConnectionInfo>(url, RequestType.Connection, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<ConnectionInfo>> GetByVirtualHost(string vhost, Action<PaginationConfigurator> pagination = null,
        CancellationToken cancellationToken = default)
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

        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        if (errors.HaveBeenFound())
            return Responses.Panic<ConnectionInfo>("api/vhosts/{vhost}/connections", errors);

        string url = string.IsNullOrWhiteSpace(@params)
            ? $"api/vhosts/{sanitizedVHost}/connections"
            : $"api/vhosts/{sanitizedVHost}/connections?{@params}";

        return await GetAllRequest<ConnectionInfo>(url, RequestType.Connection, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<ConnectionInfo>> GetByName(string name, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(name))
            return Responses.Panic<ConnectionInfo>("api/vhosts/connections/{name}",
                Errors.Create(e => { e.Add("Name of the connection to filter on is missing.", RequestType.Connection); }));

        return await GetAllRequest<ConnectionInfo>($"/api/connections/{name}", RequestType.Connection, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<ConnectionInfo>> GetByUser(string username, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(username))
            return Responses.Panic<ConnectionInfo>("api/vhosts/connections/username/{username}",
                Errors.Create(e => { e.Add("Name of the connection to filter on is missing.", RequestType.Connection); }));

        return await GetAllRequest<ConnectionInfo>($"/api/connections/username/{username}", RequestType.Connection, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string connection, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(connection))
            return Response.Panic("api/connections/{connection}", Errors.Create(e => {e.Add("The name of the connection is missing.", RequestType.Connection);}));

        return await DeleteRequest($"api/connections/{connection}", RequestType.Connection, cancellationToken);
    }

    public async Task<Result> DeleteByUser(string username, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(username))
            return Response.Panic("api/connections/username/{username}",
                Errors.Create(e => { e.Add("The username associated with the connection is missing.", RequestType.Connection); }));

        return await DeleteRequest($"/api/connections/username/{username}", RequestType.Connection, cancellationToken);
    }
}