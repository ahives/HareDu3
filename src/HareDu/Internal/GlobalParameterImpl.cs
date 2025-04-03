namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Extensions;
using Model;

class GlobalParameterImpl :
    BaseBrokerImpl,
    GlobalParameter
{
    public GlobalParameterImpl(IHttpClientFactory clientFactory)
        : base(clientFactory)
    {
    }

    public async Task<Results<GlobalParameterInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<GlobalParameterInfo>("api/global-parameters", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(string parameter, Action<GlobalParameterConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator == null)
            return Panic.Result("api/global-parameters/{parameter}", [new() {Reason = "No global parameters was defined."}]);

        var impl = new GlobalParameterConfiguratorImpl(parameter);
        configurator(impl);

        var request = impl.Request.Value;
        var errors = impl.Validate();

        if (string.IsNullOrWhiteSpace(parameter))
            errors.Add(new(){Reason = "The name of the parameter is missing."});

        if (errors.Count > 0)
            return Panic.Result("api/global-parameters/{parameter}", errors, request.ToJsonString());

        return await PutRequest($"api/global-parameters/{parameter}", request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string parameter, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(parameter))
            errors.Add(new (){Reason = "The name of the parameter is missing."});

        if (errors.Count > 0)
            return Panic.Result("api/global-parameters/{parameter}", errors);

        return await DeleteRequest($"api/global-parameters/{parameter}", cancellationToken).ConfigureAwait(false);
    }


    class GlobalParameterConfiguratorImpl :
        GlobalParameterConfigurator
    {
        IDictionary<string, ArgumentValue<object>> Arguments { get; } =
            new Dictionary<string, ArgumentValue<object>>();

        List<Error> Errors { get; } = new();

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
                    ? new ArgumentValue<object>(value, $"Argument '{arg}' has already been set")
                    : new ArgumentValue<object>(value));

        public List<Error> Validate()
        {
            if (Arguments != null)
            {
                Errors.AddRange(Arguments
                    .Select(x => x.Value?.Error)
                    .Where(error => error is not null)
                    .ToList());
            }

            return Errors;
        }
    }
}