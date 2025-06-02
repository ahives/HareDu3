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

            if (string.IsNullOrWhiteSpace(@params))
                errors.Add("Pagination parameters are in valid.");
        }

        if (errors.Count > 0)
            return Panic.Results<ConnectionInfo>("api/queues", errors);

        string url = string.IsNullOrWhiteSpace(@params) ? "api/connections" : $"api/connections?{@params}";

        return await GetAllRequest<ConnectionInfo>(url, cancellationToken).ConfigureAwait(false);
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

            if (string.IsNullOrWhiteSpace(@params))
                errors.Add("Pagination parameters are in valid.");
        }

        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add("Name of the virtual host for which to return connection information is missing.");

        if (errors.Count > 0)
            return Panic.Results<ConnectionInfo>("api/vhosts/{vhost}/connections", errors);

        string url = string.IsNullOrWhiteSpace(@params)
            ? $"api/vhosts/{sanitizedVHost}/connections"
            : $"api/vhosts/{sanitizedVHost}/connections?{@params}";

        return await GetAllRequest<ConnectionInfo>(url, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<ConnectionInfo>> GetByName(string name, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(name))
            return Panic.Results<ConnectionInfo>("api/vhosts/connections/{name}", [new(){Reason = "Name of the connection to filter on is missing."}]);

        return await GetAllRequest<ConnectionInfo>($"/api/connections/{name}", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<ConnectionInfo>> GetByUser(string username, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(username))
            return Panic.Results<ConnectionInfo>("api/vhosts/connections/username/{username}", [new(){Reason = "Name of the connection to filter on is missing."}]);

        return await GetAllRequest<ConnectionInfo>($"/api/connections/username/{username}", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string connection, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(connection))
            return Panic.Result("api/connections/{connection}", [new (){Reason = "The name of the connection is missing."}]);

        return await DeleteRequest($"api/connections/{connection}", cancellationToken);
    }

    public async Task<Result> DeleteByUser(string username, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(username))
            return Panic.Result("api/connections/username/{username}", [new (){Reason = "The username associated with the connection is missing."}]);

        return await DeleteRequest($"/api/connections/username/{username}", cancellationToken);
    }
}