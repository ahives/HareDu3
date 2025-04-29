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
    public ScopedParameterImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<Results<ScopedParameterInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<ScopedParameterInfo>("api/parameters", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create<T>(string name, T value, string component, string vhost, CancellationToken cancellationToken = default)
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
        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new(){Reason = "The name of the parameter is missing."});

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        if (string.IsNullOrWhiteSpace(component))
            errors.Add(new(){Reason = "The component name is missing."});

        if (errors.Count > 0)
            return Panic.Result("api/parameters/{component}/{vhost}/{name}", errors, request.ToJsonString());

        return await PutRequest($"api/parameters/{component}/{sanitizedVHost}/{name}", request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string name, string component, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new(){Reason = "The name of the parameter is missing."});

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        if (string.IsNullOrWhiteSpace(component))
            errors.Add(new(){Reason = "The component name is missing."});

        if (errors.Count > 0)
            return Panic.Result("api/parameters/{component}/{vhost}/{name}", errors);

        return await DeleteRequest($"api/parameters/{component}/{sanitizedVHost}/{name}", cancellationToken).ConfigureAwait(false);
    }
}