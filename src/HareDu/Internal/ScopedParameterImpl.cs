namespace HareDu.Internal;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Extensions;
using Model;
using Serialization;

class ScopedParameterImpl :
    BaseBrokerObject,
    ScopedParameter
{
    public ScopedParameterImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<ResultList<ScopedParameterInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.RequestCanceled();
            
        return await GetAllRequest<ScopedParameterInfo>("api/parameters", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create<T>(string parameter, T value, string component, string vhost,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.RequestCanceled();

        ScopedParameterRequest<T> request =
            new()
            {
                VirtualHost = vhost,
                Component = component,
                ParameterName = parameter,
                ParameterValue = value
            };

        Debug.Assert(request != null);
                
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(parameter))
            errors.Add(new(){Reason = "The name of the parameter is missing."});

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        if (string.IsNullOrWhiteSpace(component))
            errors.Add(new(){Reason = "The component name is missing."});
                    
        string url = $"api/parameters/{component}/{vhost.ToSanitizedName()}/{parameter}";

        if (errors.Any())
            return new FaultedResult{DebugInfo = new (){URL = url, Request = request.ToJsonString(Deserializer.Options), Errors = errors}};

        return await PutRequest(url, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string parameter, string component, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.RequestCanceled();
                
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(parameter))
            errors.Add(new(){Reason = "The name of the parameter is missing."});

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        if (string.IsNullOrWhiteSpace(component))
            errors.Add(new(){Reason = "The component name is missing."});

        string url = $"api/parameters/{component}/{vhost.ToSanitizedName()}/{parameter}";

        if (errors.Any())
            return new FaultedResult {DebugInfo = new (){URL = url, Errors = errors}};

        return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
    }
}