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

        public Task<ResultList<ExchangeInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = $"api/exchanges";
            
            return GetAll<ExchangeInfo>(url, cancellationToken);
        }

        public Task<Result> Create(Action<ExchangeCreateAction> action, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.RequestCanceled();

            var impl = new ExchangeCreateActionImpl();
            action(impl);
            
            impl.Validate();
            
            ExchangeDefinition definition = impl.Definition.Value;

            Debug.Assert(definition != null);

            string url = $"api/exchanges/{impl.VirtualHost.Value.ToSanitizedName()}/{impl.ExchangeName.Value}";
            
            if (impl.Errors.Value.Any())
                return Task.FromResult<Result>(new FaultedResult{Errors = impl.Errors.Value, DebugInfo = new (){URL = url, Request = definition.ToJsonString()}});

            return Put(url, definition, cancellationToken);
        }

        public Task<Result> Delete(Action<ExchangeDeleteAction> action, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new ExchangeDeleteActionImpl();
            action(impl);
            
            impl.Validate();

            string vhost = impl.VirtualHost.Value.ToSanitizedName();
            
            string url = $"api/exchanges/{vhost}/{impl.ExchangeName.Value}";
            if (!string.IsNullOrWhiteSpace(impl.Query.Value))
                url = $"api/exchanges/{vhost}/{impl.ExchangeName.Value}?{impl.Query.Value}";

            if (impl.Errors.Value.Any())
                return Task.FromResult<Result>(new FaultedResult<ExchangeInfo> {Errors = impl.Errors.Value, DebugInfo = new() {URL = url, Request = null}});

            return Delete(url, cancellationToken);
        }

        
        class ExchangeDeleteActionImpl :
            ExchangeDeleteAction
        {
            string _vhost;
            string _exchange;
            string _query;
            readonly List<Error> _errors;

            public Lazy<string> Query { get; }
            public Lazy<string> VirtualHost { get; }
            public Lazy<string> ExchangeName { get; }
            public Lazy<List<Error>> Errors { get; }

            public ExchangeDeleteActionImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Query = new Lazy<string>(() => _query, LazyThreadSafetyMode.PublicationOnly);
                VirtualHost = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
                ExchangeName = new Lazy<string>(() => _exchange, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Exchange(string name) => _exchange = name;

            public void When(Action<ExchangeDeleteCondition> condition)
            {
                var impl = new ExchangeDeleteConditionImpl();
                condition(impl);

                string query = string.Empty;
                if (impl.DeleteIfUnused)
                    query = "if-unused=true";

                _query = query;
            }

            public void Targeting(Action<ExchangeTarget> target)
            {
                var impl = new ExchangeTargetImpl();
                target(impl);

                _vhost = impl.VirtualHostName;
            }

            public void Validate()
            {
                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new () {Reason = "The name of the virtual host is missing."});

                if (string.IsNullOrWhiteSpace(_exchange))
                    _errors.Add(new() {Reason = "The name of the exchange is missing."});
            }

            
            class ExchangeTargetImpl :
                ExchangeTarget
            {
                public string VirtualHostName { get; private set; }

                public void VirtualHost(string name) => VirtualHostName = name;
            }


            class ExchangeDeleteConditionImpl :
                ExchangeDeleteCondition
            {
                public bool DeleteIfUnused { get; private set; }

                public void Unused() => DeleteIfUnused = true;
            }
        }


        class ExchangeCreateActionImpl :
            ExchangeCreateAction
        {
            string _routingType;
            bool _durable;
            bool _autoDelete;
            bool _internal;
            IDictionary<string, ArgumentValue<object>> _arguments;
            string _vhost;
            string _exchange;
            readonly List<Error> _errors;

            public Lazy<ExchangeDefinition> Definition { get; }
            public Lazy<string> VirtualHost { get; }
            public Lazy<string> ExchangeName { get; }
            public Lazy<List<Error>> Errors { get; }

            public ExchangeCreateActionImpl()
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
                VirtualHost = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
                ExchangeName = new Lazy<string>(() => _exchange, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Exchange(string name) => _exchange = name;

            public void Configure(Action<ExchangeConfiguration> configuration)
            {
                var impl = new ExchangeConfigurationImpl();
                configuration(impl);

                _durable = impl.Durable;
                _routingType = impl.RoutingType;
                _autoDelete = impl.AutoDelete;
                _internal = impl.InternalUse;
                _arguments = impl.Arguments;
            }

            public void Targeting(Action<ExchangeTarget> target)
            {
                var impl = new ExchangeTargetImpl();
                target(impl);

                _vhost = impl.VirtualHostName;
            }

            public void Validate()
            {
                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new () {Reason = "The name of the virtual host is missing."});

                if (string.IsNullOrWhiteSpace(_routingType))
                    _errors.Add(new () {Reason = "The routing type of the exchange is missing."});

                if (!_arguments.IsNull())
                    _errors.AddRange(_arguments.Select(x => x.Value?.Error).Where(error => !error.IsNull()).ToList());

                if (string.IsNullOrWhiteSpace(_exchange))
                    _errors.Add(new () {Reason = "The name of the exchange is missing."});
            }

            
            class ExchangeTargetImpl :
                ExchangeTarget
            {
                public string VirtualHostName { get; private set; }

                public void VirtualHost(string name) => VirtualHostName = name;
            }


            class ExchangeConfigurationImpl :
                ExchangeConfiguration
            {
                public string RoutingType { get; private set; }
                public IDictionary<string, ArgumentValue<object>> Arguments { get; private set; }
                public bool Durable { get; private set; }
                public bool InternalUse { get; private set; }
                public bool AutoDelete { get; private set; }

                public void HasRoutingType(ExchangeRoutingType routingType)
                {
                    switch (routingType)
                    {
                        case ExchangeRoutingType.Fanout:
                            RoutingType = "fanout";
                            break;
                            
                        case ExchangeRoutingType.Direct:
                            RoutingType = "direct";
                            break;
                            
                        case ExchangeRoutingType.Topic:
                            RoutingType = "topic";
                            break;
                            
                        case ExchangeRoutingType.Headers:
                            RoutingType = "headers";
                            break;
                            
                        case ExchangeRoutingType.Federated:
                            RoutingType = "federated";
                            break;
                            
                        case ExchangeRoutingType.Match:
                            RoutingType = "match";
                            break;
                            
                        default:
                            throw new ArgumentOutOfRangeException(nameof(routingType), routingType, null);
                    }
                }

                public void IsDurable() => Durable = true;

                public void IsForInternalUse() => InternalUse = true;

                public void HasArguments(Action<ExchangeDefinitionArguments> arguments)
                {
                    var impl = new ExchangeDefinitionArgumentsImpl();
                    arguments(impl);

                    Arguments = impl.Arguments;
                }

                public void AutoDeleteWhenNotInUse() => AutoDelete = true;
            }


            class ExchangeDefinitionArgumentsImpl :
                ExchangeDefinitionArguments
            {
                public IDictionary<string, ArgumentValue<object>> Arguments { get; }

                public ExchangeDefinitionArgumentsImpl()
                {
                    Arguments = new Dictionary<string, ArgumentValue<object>>();
                }

                public void Set<T>(string arg, T value)
                {
                    SetArg(arg, value);
                }

                void SetArg(string arg, object value)
                {
                    Arguments.Add(arg.Trim(),
                        Arguments.ContainsKey(arg)
                            ? new ArgumentValue<object>(value, $"Argument '{arg}' has already been set")
                            : new ArgumentValue<object>(value));
                }
            }
        }
    }
}