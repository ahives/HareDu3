namespace HareDu.Internal;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Model;
using Serialization;

class ScopedParameterImpl :
    BaseHareDuImpl,
    ScopedParameter
{
    public ScopedParameterImpl(HttpClient client)
        : base(client, BrokerDeserializer.Instance)
    {
    }

    public async Task<Results<ScopedParameterInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<ScopedParameterInfo>("api/parameters", RequestType.ScopeParameter, cancellationToken).ConfigureAwait(false);
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

        errors.AddIfTrue(value, IsValueNull, Errors.Create("The name of the parameter is missing."));
        errors.AddIfTrue(name, string.IsNullOrWhiteSpace, Errors.Create("The name of the parameter is missing."));
        errors.AddIfTrue(component, string.IsNullOrWhiteSpace, Errors.Create("The component name is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        bool IsValueNull<U>(U obj) =>
            obj switch
            {
                string str => string.IsNullOrWhiteSpace(str),
                _ => obj is null
            };

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("api/parameters/{component}/{vhost}/{name}", errors, request: Deserializer.ToJsonString(request)))
            : await PutRequest($"api/parameters/{component}/{sanitizedVHost}/{name}", request,
                RequestType.ScopeParameter, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string name, string component, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(name, string.IsNullOrWhiteSpace, Errors.Create("The name of the parameter is missing."));
        errors.AddIfTrue(component, string.IsNullOrWhiteSpace, Errors.Create("The component name is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("api/parameters/{component}/{vhost}/{name}", errors))
            : await DeleteRequest($"api/parameters/{component}/{sanitizedVHost}/{name}", RequestType.ScopeParameter,
                cancellationToken).ConfigureAwait(false);
    }
}