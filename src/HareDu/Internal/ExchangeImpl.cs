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

class ExchangeImpl :
    BaseBrokerImpl,
    Exchange
{
    public ExchangeImpl(IHttpClientFactory clientFactory)
        : base(clientFactory)
    {
    }

    public async Task<Results<ExchangeInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
            
        return await GetAllRequest<ExchangeInfo>("api/exchanges", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(string exchange, string vhost, Action<ExchangeConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator == null)
            return Panic.Result("api/exchanges/{vhost}/{exchange}", [new() {Reason = "No global parameters was defined."}]);

        var impl = new ExchangeConfiguratorImpl();
        configurator(impl);

        var request = impl.Request.Value;
        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = impl.Validate();

        if (string.IsNullOrWhiteSpace(exchange))
            errors.Add(new (){Reason = "The name of the exchange is missing."});

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        if (errors.Count > 0)
            return Panic.Result("api/exchanges/{vhost}/{exchange}", errors, request.ToJsonString());

        return await PutRequest($"api/exchanges/{sanitizedVHost}/{exchange}", request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string exchange, string vhost, Action<ExchangeDeletionConfigurator> configurator = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var impl = new ExchangeDeletionConfigurationImpl();
        configurator?.Invoke(impl);

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(exchange))
            errors.Add(new (){Reason = "The name of the exchange is missing."});

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        if (errors.Count > 0)
            return Panic.Result("api/exchanges/{vhost}/{exchange}", errors);

        string url = string.IsNullOrWhiteSpace(impl.Query.Value)
            ? $"api/exchanges/{sanitizedVHost}/{exchange}"
            : $"api/exchanges/{sanitizedVHost}/{exchange}?{impl.Query.Value}";

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
        
        IDictionary<string, ArgumentValue<object>> Args { get; } = new Dictionary<string, ArgumentValue<object>>();

        public Lazy<ExchangeRequest> Request { get; }

        public ExchangeConfiguratorImpl()
        {
            Request = new Lazy<ExchangeRequest>(
                () => new ()
                {
                    RoutingType = _routingType,
                    Durable = _durable,
                    AutoDelete = _autoDelete,
                    Internal = _internal,
                    Arguments = Args.GetArgumentsOrNull()
                }, LazyThreadSafetyMode.PublicationOnly);
        }
            
        public void HasRoutingType(ExchangeRoutingType routingType) => _routingType = routingType;

        public void IsDurable() => _durable = true;

        public void IsForInternalUse() => _internal = true;

        public void HasArguments(Action<ExchangeArgumentConfigurator> arguments)
        {
            var impl = new ExchangeArgumentConfiguratorImpl();
            arguments?.Invoke(impl);

            foreach (var arg in impl.Arguments)
                if (!Args.TryAdd(arg.Key, arg.Value))
                    Args[arg.Key] = arg.Value;
        }

        public void AutoDeleteWhenNotInUse() => _autoDelete = true;

        public List<Error> Validate() =>
            Args
                .Select(x => x.Value?.Error)
                .Where(error => error is not null)
                .ToList();


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