namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Extensions;
using Model;

class GlobalParameterImpl :
    BaseBrokerImpl,
    GlobalParameter
{
    public GlobalParameterImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<Results<GlobalParameterInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<GlobalParameterInfo>("api/global-parameters", RequestType.GlobalParameter, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(string parameter, Action<GlobalParameterConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator is null)
            return Response.Panic("api/global-parameters/{parameter}", [Errors.Create("No global parameters was defined.")]);

        var impl = new GlobalParameterConfiguratorImpl(parameter);
        configurator(impl);

        var request = impl.Request.Value;
        var errors = impl.Validate();

        errors.AddIfTrue(parameter, string.IsNullOrWhiteSpace, Errors.Create("The name of the parameter is missing."));

        if (errors.HaveBeenFound())
            return Response.Panic("api/global-parameters/{parameter}", errors, request.ToJsonString());

        return await PutRequest($"api/global-parameters/{parameter}", request, RequestType.GlobalParameter, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string parameter, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(parameter))
            return Response.Panic("api/global-parameters/{parameter}",
                Errors.Create(e => { e.Add("The name of the parameter is missing.", RequestType.GlobalParameter); }));

        return await DeleteRequest($"api/global-parameters/{parameter}", RequestType.GlobalParameter, cancellationToken).ConfigureAwait(false);
    }


    class GlobalParameterConfiguratorImpl :
        GlobalParameterConfigurator
    {
        IDictionary<string, ArgumentValue<object>> Arguments { get; } =
            new Dictionary<string, ArgumentValue<object>>();

        List<Error> InternalErrors { get; } = new();

        public Lazy<GlobalParameterRequest> Request { get; }

        public GlobalParameterConfiguratorImpl(string name)
        {
            Request = new Lazy<GlobalParameterRequest>(
                () => new ()
                {
                    Name = name,
                    Value = Arguments.GetArgumentsOrNull()
                }, LazyThreadSafetyMode.PublicationOnly);
        }

        public void Add<T>(string arg, T value) =>
            Arguments.Add(arg.Trim(),
                Arguments.ContainsKey(arg)
                    ? new ArgumentValue<object>(value, Errors.Create($"Argument '{arg}' has already been set"))
                    : new ArgumentValue<object>(value));

        public List<Error> Validate()
        {
            if (Arguments != null)
            {
                InternalErrors.AddRange(Arguments
                    .Select(x => x.Value?.Error)
                    .Where(error => error is not null)
                    .ToList());
            }

            return InternalErrors;
        }
    }
}