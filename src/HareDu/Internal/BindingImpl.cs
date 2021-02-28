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
    using Core.Serialization;
    using Extensions;
    using Model;

    class BindingImpl :
        BaseBrokerObject,
        Binding
    {
        public BindingImpl(HttpClient client)
            : base(client)
        {
        }

        public async Task<ResultList<BindingInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/bindings";
            
            return await GetAllRequest<BindingInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result<BindingInfo>> Create(string sourceBinding, string destinationBinding,
            BindingType bindingType, string vhost, Action<BindingConfigurator> configurator = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new BindingConfiguratorImpl();
            configurator?.Invoke(impl);
            
            BindingRequest request = impl.Request.Value;

            Debug.Assert(request != null);

            var errors = new List<Error>();
            
            errors.AddRange(impl.Errors.Value);
            
            if (string.IsNullOrWhiteSpace(sourceBinding))
                errors.Add(new(){Reason = "The name of the source binding (queue/exchange) is missing."});

            if (string.IsNullOrWhiteSpace(destinationBinding))
                errors.Add(new(){Reason = "The name of the destination binding (queue/exchange) is missing."});

            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new(){Reason = "The name of the virtual host is missing."});

            string virtualHost = vhost.ToSanitizedName();

            string url = bindingType == BindingType.Exchange
                ? $"api/bindings/{virtualHost}/e/{sourceBinding}/e/{destinationBinding}"
                : $"api/bindings/{virtualHost}/e/{sourceBinding}/q/{destinationBinding}";

            if (errors.Any())
                return new FaultedResult<BindingInfo>{DebugInfo = new (){URL = url, Request = request.ToJsonString(Deserializer.Options), Errors = errors}};

            return await PostRequest<BindingInfo, BindingRequest>(url, request, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(string sourceBinding, string destinationBinding, string propertiesKey,
            string vhost, BindingType bindingType, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var errors = new List<Error>();
            
            if (string.IsNullOrWhiteSpace(sourceBinding))
                errors.Add(new(){Reason = "The name of the source binding (queue/exchange) is missing."});

            if (string.IsNullOrWhiteSpace(destinationBinding))
                errors.Add(new(){Reason = "The name of the destination binding (queue/exchange) is missing."});

            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new(){Reason = "The name of the virtual host is missing."});

            string virtualHost = vhost.ToSanitizedName();

            string Normalize(string value) => string.IsNullOrWhiteSpace(value) ? string.Empty : value;
            
            string url = bindingType == BindingType.Queue
                ? $"api/bindings/{virtualHost}/e/{sourceBinding}/q/{destinationBinding}/{Normalize(propertiesKey)}"
                : $"api/bindings/{virtualHost}/e/{sourceBinding}/e/{destinationBinding}/{Normalize(propertiesKey)}";
            
            if (errors.Any())
                return new FaultedResult<BindingInfo>{DebugInfo = new (){URL = url, Errors = errors}};

            return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
        }


        class BindingConfiguratorImpl :
            BindingConfigurator
        {
            string _routingKey;
            IDictionary<string, ArgumentValue<object>> _arguments;

            readonly List<Error> _errors;

            public Lazy<BindingRequest> Request { get; }
            public Lazy<List<Error>> Errors { get; }

            public BindingConfiguratorImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Request = new Lazy<BindingRequest>(() =>
                    new()
                    {
                        RoutingKey = _routingKey,
                        Arguments = _arguments.GetArgumentsOrNull()
                    }, LazyThreadSafetyMode.PublicationOnly);
            }

            public void WithRoutingKey(string routingKey) => _routingKey = routingKey;

            public void WithArguments(Action<BindingArgumentConfigurator> arguments)
            {
                var impl = new BindingArgumentConfiguratorImpl();
                arguments?.Invoke(impl);

                _arguments = impl.Arguments;
                
                _errors.AddRange(_arguments
                    .Select(x => x.Value?.Error)
                    .Where(error => error.IsNotNull())
                    .ToList());
            }

            
            class BindingArgumentConfiguratorImpl :
                BindingArgumentConfigurator
            {
                public IDictionary<string, ArgumentValue<object>> Arguments { get; }

                public BindingArgumentConfiguratorImpl()
                {
                    Arguments = new Dictionary<string, ArgumentValue<object>>();
                }

                public void Add<T>(string arg, T value) =>
                    Arguments.Add(arg.Trim(),
                        Arguments.ContainsKey(arg)
                            ? new ArgumentValue<object>(value, $"Argument '{arg}' has already been set")
                            : new ArgumentValue<object>(value));
            }
        }
    }
}