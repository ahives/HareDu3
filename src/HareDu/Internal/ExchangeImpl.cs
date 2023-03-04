namespace HareDu.Internal;

using System;
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

class ExchangeImpl :
    BaseBrokerObject,
    Exchange
{
    public ExchangeImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<ResultList<ExchangeInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
            
        return await GetAllRequest<ExchangeInfo>("api/exchanges", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(string exchange, string vhost, Action<ExchangeConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var impl = new ExchangeConfiguratorImpl();
        configurator?.Invoke(impl);

        var request = impl.Request.Value;

        var errors = impl.Errors.Value;

        if (string.IsNullOrWhiteSpace(exchange))
            errors.Add(new (){Reason = "The name of the exchange is missing."});

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        string url = $"api/exchanges/{vhost.ToSanitizedName()}/{exchange}";

        if (errors.Any())
            return new FaultedResult{DebugInfo = new (){URL = url, Request = request.ToJsonString(Deserializer.Options), Errors = errors}};

        return await PutRequest(url, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string exchange, string vhost, Action<ExchangeDeletionConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var impl = new ExchangeDeletionConfigurationImpl();
        configurator?.Invoke(impl);

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(exchange))
            errors.Add(new (){Reason = "The name of the exchange is missing."});

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        string virtualHost = vhost.ToSanitizedName();

        string url = string.IsNullOrWhiteSpace(impl.Query.Value)
            ? $"api/exchanges/{virtualHost}/{exchange}"
            : $"api/exchanges/{virtualHost}/{exchange}?{impl.Query.Value}";

        if (errors.Any())
            return new FaultedResult {DebugInfo = new (){URL = url, Errors = errors}};

        return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
    }

        
    class ExchangeDeletionConfigurationImpl :
        ExchangeDeletionConfigurator
    {
        string _query;

        public Lazy<string> Query { get; }

        public ExchangeDeletionConfigurationImpl()
        {
            Query = new Lazy<string>(() => _query, LazyThreadSafetyMode.PublicationOnly);
        }

        public void WhenUnused() => _query = "if-unused=true";
    }


    class ExchangeConfiguratorImpl :
        ExchangeConfigurator
    {
        ExchangeRoutingType _routingType;
        bool _durable;
        bool _autoDelete;
        bool _internal;
        IDictionary<string, ArgumentValue<object>> _arguments;
            
        readonly List<Error> _errors;

        public Lazy<ExchangeRequest> Request { get; }
        public Lazy<List<Error>> Errors { get; }

        public ExchangeConfiguratorImpl()
        {
            _errors = new List<Error>();
                
            Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
            Request = new Lazy<ExchangeRequest>(
                () => new ()
                {
                    RoutingType = _routingType,
                    Durable = _durable,
                    AutoDelete = _autoDelete,
                    Internal = _internal,
                    Arguments = _arguments.GetArgumentsOrNull()
                }, LazyThreadSafetyMode.PublicationOnly);
        }
            
        public void HasRoutingType(ExchangeRoutingType routingType) => _routingType = routingType;

        public void IsDurable() => _durable = true;

        public void IsForInternalUse() => _internal = true;

        public void HasArguments(Action<ExchangeArgumentConfigurator> arguments)
        {
            var impl = new ExchangeArgumentConfiguratorImpl();
            arguments?.Invoke(impl);

            _arguments = impl.Arguments;

            if (_arguments is not null)
                _errors.AddRange(_arguments
                    .Select(x => x.Value?.Error)
                    .Where(error => error is not null).ToList());
        }

        public void AutoDeleteWhenNotInUse() => _autoDelete = true;


        class ExchangeArgumentConfiguratorImpl :
            ExchangeArgumentConfigurator
        {
            public IDictionary<string, ArgumentValue<object>> Arguments { get; }

            public ExchangeArgumentConfiguratorImpl()
            {
                Arguments = new Dictionary<string, ArgumentValue<object>>();
            }

            public void Add<T>(string arg, T value) => SetArg(arg, value);

            void SetArg(string arg, object value) =>
                Arguments.Add(arg.Trim(),
                    Arguments.ContainsKey(arg)
                        ? new ArgumentValue<object>(value, $"Argument '{arg}' has already been set")
                        : new ArgumentValue<object>(value));
        }
    }
}