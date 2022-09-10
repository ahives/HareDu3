namespace HareDu.Internal;

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Model;

class ConnectionImpl :
    BaseBrokerObject,
    Connection
{
    public ConnectionImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<ResultList<ConnectionInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.RequestCanceled();

        string url = "api/connections";
            
        return await GetAllRequest<ConnectionInfo>(url, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string connection, CancellationToken cancellationToken = default)
    {
        cancellationToken.RequestCanceled();

        var errors = new List<Error>();
            
        if (string.IsNullOrWhiteSpace(connection))
            errors.Add(new (){Reason = "The name of the connection is missing."});

        string url = $"api/connections/{connection}";
            
        if (errors.Any())
            return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

        return await DeleteRequest(url, cancellationToken);
    }
}