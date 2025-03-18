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
    BaseBrokerObject,
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

        var impl = new GlobalParameterConfiguratorImpl(parameter);
        configurator?.Invoke(impl);

        impl.Validate();

        var request = impl.Request.Value;

        var errors = impl.Errors;

        if (string.IsNullOrWhiteSpace(parameter))
            errors.Add(new(){Reason = "The name of the parameter is missing."});

        string url = $"api/global-parameters/{parameter}";

        if (errors.Count > 0)
            return new FaultedResult{DebugInfo = new (){URL = url, Request = request.ToJsonString(), Errors = errors}};

        return await PutRequest(url, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string parameter, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(parameter))
            errors.Add(new (){Reason = "The name of the parameter is missing."});

        string url = $"api/global-parameters/{parameter}";

        if (errors.Count > 0)
            return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

        return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
    }


    class GlobalParameterConfiguratorImpl :
        GlobalParameterConfigurator
    {
        IDictionary<string, ArgumentValue<object>> _arguments;
        object _argument;

        public Lazy<GlobalParameterRequest> Request { get; }
        public List<Error> Errors { get; }

        public GlobalParameterConfiguratorImpl(string name)
        {
            Errors = new List<Error>();
            Request = new Lazy<GlobalParameterRequest>(
                () => new ()
                {
                    Name = name,
                    Value = _argument ?? _arguments.GetArgumentsOrNull()
                }, LazyThreadSafetyMode.PublicationOnly);
        }

        public void Value(Action<GlobalParameterArgumentConfigurator> configurator)
        {
            var impl = new GlobalParameterArgumentConfiguratorImpl();
            configurator?.Invoke(impl);

            _arguments = impl.Arguments;
        }

        public void Value<T>(T argument) => _argument = argument;

        public void Validate()
        {
            switch (_argument)
            {
                case string:
                {
                    if (string.IsNullOrWhiteSpace(_argument.ToString()))
                        Errors.Add(new(){Reason = "Parameter value is missing."});
                
                    return;
                }
                
                case null when _arguments is null:
                    Errors.Add(new(){Reason = "Parameter value is missing."});
                    break;
            }
                
            if (_arguments != null)
            {
                Errors.AddRange(_arguments
                    .Select(x => x.Value?.Error)
                    .Where(error => error is not null)
                    .ToList());
            }
        }


        class GlobalParameterArgumentConfiguratorImpl :
            GlobalParameterArgumentConfigurator
        {
            public IDictionary<string, ArgumentValue<object>> Arguments { get; } =
                new Dictionary<string, ArgumentValue<object>>();

            public void Add<T>(string arg, T value) =>
                Arguments.Add(arg.Trim(),
                    Arguments.ContainsKey(arg)
                        ? new ArgumentValue<object>(value, $"Argument '{arg}' has already been set")
                        : new ArgumentValue<object>(value));
        }
    }
}