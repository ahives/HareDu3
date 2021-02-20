namespace HareDu.Internal
{
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
            cancellationToken.RequestCanceled();

            string url = "api/exchanges";
            
            return await GetAll<ExchangeInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Create(string exchange, string vhost, Action<NewExchangeConfigurator> configurator = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new NewExchangeConfiguratorImpl();
            configurator?.Invoke(impl);
            
            ExchangeDefinition definition = impl.Definition.Value;

            Debug.Assert(definition != null);

            var errors = new List<Error>();
            
            errors.AddRange(impl.Errors.Value);
            
            if (string.IsNullOrWhiteSpace(exchange))
                errors.Add(new () {Reason = "The name of the exchange is missing."});

            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new () {Reason = "The name of the virtual host is missing."});

            string url = $"api/exchanges/{vhost.ToSanitizedName()}/{exchange}";
            
            if (errors.Any())
                return new FaultedResult{DebugInfo = new () {URL = url, Request = definition.ToJsonString(), Errors = errors}};

            return await Put(url, definition, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(string exchange, string vhost, Action<DeleteExchangeConfigurator> configurator, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new DeleteExchangeConfigurationImpl();
            configurator?.Invoke(impl);

            var errors = new List<Error>();
            
            if (string.IsNullOrWhiteSpace(exchange))
                errors.Add(new () {Reason = "The name of the exchange is missing."});

            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new () {Reason = "The name of the virtual host is missing."});

            string virtualHost = vhost.ToSanitizedName();
            
            string url = $"api/exchanges/{virtualHost}/{exchange}";
            if (!string.IsNullOrWhiteSpace(impl.Query.Value))
                url = $"api/exchanges/{virtualHost}/{exchange}?{impl.Query.Value}";

            if (errors.Any())
                return new FaultedResult {DebugInfo = new() {URL = url, Errors = errors}};

            return await Delete(url, cancellationToken).ConfigureAwait(false);
        }

        
        class DeleteExchangeConfigurationImpl :
            DeleteExchangeConfigurator
        {
            string _query;

            public Lazy<string> Query { get; }

            public DeleteExchangeConfigurationImpl()
            {
                Query = new Lazy<string>(() => _query, LazyThreadSafetyMode.PublicationOnly);
            }
            
            public void When(Action<DeleteExchangeCondition> condition)
            {
                var impl = new DeleteExchangeConditionImpl();
                condition?.Invoke(impl);

                string query = string.Empty;
                if (impl.DeleteIfUnused)
                    query = "if-unused=true";

                _query = query;
            }


            class DeleteExchangeConditionImpl :
                DeleteExchangeCondition
            {
                public bool DeleteIfUnused { get; private set; }

                public void Unused() => DeleteIfUnused = true;
            }
        }


        class NewExchangeConfiguratorImpl :
            NewExchangeConfigurator
        {
            string _routingType;
            bool _durable;
            bool _autoDelete;
            bool _internal;
            IDictionary<string, ArgumentValue<object>> _arguments;
            
            readonly List<Error> _errors;

            public Lazy<ExchangeDefinition> Definition { get; }
            public Lazy<List<Error>> Errors { get; }

            public NewExchangeConfiguratorImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Definition = new Lazy<ExchangeDefinition>(
                    () => new ExchangeDefinition
                    {
                        RoutingType = _routingType,
                        Durable = _durable,
                        AutoDelete = _autoDelete,
                        Internal = _internal,
                        Arguments = _arguments.GetArguments()
                    }, LazyThreadSafetyMode.PublicationOnly);
            }
            
            public void HasRoutingType(ExchangeRoutingType routingType) => _routingType = routingType.Convert();

            public void IsDurable() => _durable = true;

            public void IsForInternalUse() => _internal = true;

            public void HasArguments(Action<NewExchangeArguments> arguments)
            {
                var impl = new NewExchangeArgumentsImpl();
                arguments?.Invoke(impl);

                _arguments = impl.Arguments;

                if (_arguments.IsNotNull())
                    _errors.AddRange(_arguments.Select(x => x.Value?.Error).Where(error => error.IsNotNull()).ToList());
            }

            public void AutoDeleteWhenNotInUse() => _autoDelete = true;


            class NewExchangeArgumentsImpl :
                NewExchangeArguments
            {
                public IDictionary<string, ArgumentValue<object>> Arguments { get; }

                public NewExchangeArgumentsImpl()
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
}