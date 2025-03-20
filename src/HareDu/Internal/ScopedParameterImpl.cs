namespace HareDu.Internal;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Extensions;
using Model;

class ScopedParameterImpl :
    BaseBrokerImpl,
    ScopedParameter
{
    public ScopedParameterImpl(IHttpClientFactory clientFactory)
        : base(clientFactory)
    {
    }

    public async Task<Results<ScopedParameterInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<ScopedParameterInfo>("api/parameters", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create<T>(string name, T value, string component, string vhost,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        ScopedParameterRequest<T> request =
            new()
            {
                VirtualHost = vhost,
                Component = component,
                ParameterName = name,
                ParameterValue = value
            };

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new(){Reason = "The name of the parameter is missing."});

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        if (string.IsNullOrWhiteSpace(component))
            errors.Add(new(){Reason = "The component name is missing."});

        string url = $"api/parameters/{component}/{vhost.ToSanitizedName()}/{name}";

        if (errors.Count > 0)
            return new FaultedResult{DebugInfo = new (){URL = url, Request = request.ToJsonString(), Errors = errors}};

        return await PutRequest(url, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string name, string component, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new(){Reason = "The name of the parameter is missing."});

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        if (string.IsNullOrWhiteSpace(component))
            errors.Add(new(){Reason = "The component name is missing."});

        string url = $"api/parameters/{component}/{vhost.ToSanitizedName()}/{name}";

        if (errors.Count > 0)
            return new FaultedResult {DebugInfo = new (){URL = url, Errors = errors}};

        return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
    }
}