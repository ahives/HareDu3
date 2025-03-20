namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

class ConnectionImpl :
    BaseBrokerImpl,
    Connection
{
    public ConnectionImpl(IHttpClientFactory clientFactory)
        : base(clientFactory)
    {
    }

    public async Task<Results<ConnectionInfo>> GetAll(Action<PaginationConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string pagination = null;

        if (configurator != null)
        {
            var impl = new PaginationConfiguratorImpl();
            configurator?.Invoke(impl);

            pagination = impl.BuildPaginationParams();
        }

        var errors = new List<Error>();

        if (errors.Count > 0)
            return new FaultedResults<ConnectionInfo>{DebugInfo = new (){URL = "api/connections", Errors = errors}};

        string url = string.IsNullOrWhiteSpace(pagination) ? "api/connections" : $"api/connections?{pagination}";

        if (errors.Count > 0)
            return new FaultedResults<ConnectionInfo>{DebugInfo = new (){URL = url, Errors = errors}};

        return await GetAllRequest<ConnectionInfo>(url, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<ConnectionInfo>> GetByVirtualHost(string vhost, Action<PaginationConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string pagination = null;

        if (configurator != null)
        {
            var impl = new PaginationConfiguratorImpl();
            configurator?.Invoke(impl);

            pagination = impl.BuildPaginationParams();
        }

        var errors = new List<Error>();
        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new(){Reason = "Name of the virtual host for which to return connection information is missing."});

        if (errors.Count > 0)
            return new FaultedResults<ConnectionInfo>{DebugInfo = new (){URL = "api/vhosts/{vhost}/connections", Errors = errors}};

        string url = string.IsNullOrWhiteSpace(pagination) ? $"api/vhosts/{vhost}/connections" : $"api/vhosts/{vhost}/connections?{pagination}";

        return await GetAllRequest<ConnectionInfo>(url, cancellationToken).ConfigureAwait(false);
    }

    public Task<Results<ConnectionInfo>> GetByName(string name, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }

    public Task<Results<ConnectionInfo>> GetByUser(string username, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }

    public async Task<Result> Delete(string connection, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(connection))
            errors.Add(new (){Reason = "The name of the connection is missing."});

        string url = $"api/connections/{connection}";

        if (errors.Count > 0)
            return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

        return await DeleteRequest(url, cancellationToken);
    }

    public Task<Result> DeleteByUser(string username, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }
}