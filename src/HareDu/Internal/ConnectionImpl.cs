namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Model;
using Serialization;

class ConnectionImpl :
    BaseBrokerImpl,
    Connection
{
    public ConnectionImpl(HttpClient client)
        : base(client, new BrokerDeserializer())
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

        return errors.HaveBeenFound()
            ? Responses.Panic<ConnectionInfo>(Debug.Info("api/connections", errors))
            : await GetAllRequest<ConnectionInfo>(
                string.IsNullOrWhiteSpace(@params) ? "api/connections" : $"api/connections?{@params}", RequestType.Connection, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<ConnectionInfo>> GetByVirtualHost(
        string vhost,
        Action<PaginationConfigurator> pagination = null,
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

        return errors.HaveBeenFound()
            ? Responses.Panic<ConnectionInfo>(Debug.Info("api/vhosts/{vhost}/connections", errors))
            : await GetAllRequest<ConnectionInfo>(
                string.IsNullOrWhiteSpace(@params)
                    ? $"api/vhosts/{sanitizedVHost}/connections"
                    : $"api/vhosts/{sanitizedVHost}/connections?{@params}",
                RequestType.Connection, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<ConnectionInfo>> GetByName(string name, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return string.IsNullOrWhiteSpace(name)
            ? Responses.Panic<ConnectionInfo>(Debug.Info("api/vhosts/connections/{name}",
                Errors.Create(e => { e.Add("Name of the connection to filter on is missing.", RequestType.Connection); })))
            : await GetAllRequest<ConnectionInfo>($"/api/connections/{name}", RequestType.Connection, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<ConnectionInfo>> GetByUser(string username, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return string.IsNullOrWhiteSpace(username)
            ? Responses.Panic<ConnectionInfo>(Debug.Info("api/vhosts/connections/username/{username}",
                Errors.Create(e => { e.Add("Name of the connection to filter on is missing.", RequestType.Connection); })))
            : await GetAllRequest<ConnectionInfo>($"/api/connections/username/{username}", RequestType.Connection, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string connection, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return string.IsNullOrWhiteSpace(connection)
            ? Response.Panic(Debug.Info("api/connections/{connection}",
                Errors.Create(e => { e.Add("The name of the connection is missing.", RequestType.Connection); })))
            : await DeleteRequest($"api/connections/{connection}", RequestType.Connection, cancellationToken);
    }

    public async Task<Result> DeleteByUser(string username, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return string.IsNullOrWhiteSpace(username)
            ? Response.Panic(Debug.Info("api/connections/username/{username}",
                Errors.Create(e => { e.Add("The username associated with the connection is missing.", RequestType.Connection); })))
            : await DeleteRequest($"/api/connections/username/{username}", RequestType.Connection, cancellationToken);
    }
}