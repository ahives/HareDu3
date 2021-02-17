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
            
            return await GetAll<BindingInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result<BindingInfo>> Create(string sourceBinding, string destinationBinding,
            BindingType bindingType, string vhost,
            Action<NewBindingConfiguration> configuration = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new NewBindingConfigurationImpl();
            configuration?.Invoke(impl);
            
            BindingDefinition definition = impl.Definition.Value;

            Debug.Assert(definition != null);

            var errors = new List<Error>();
            
            errors.AddRange(impl.Errors.Value);
            
            if (string.IsNullOrWhiteSpace(sourceBinding))
                errors.Add(new() {Reason = "The name of the source binding (queue/exchange) is missing."});

            if (string.IsNullOrWhiteSpace(destinationBinding))
                errors.Add(new() {Reason = "The name of the destination binding (queue/exchange) is missing."});

            string virtualHost = vhost.ToSanitizedName();

            string url = bindingType == BindingType.Exchange
                ? $"api/bindings/{virtualHost}/e/{sourceBinding}/e/{destinationBinding}"
                : $"api/bindings/{virtualHost}/e/{sourceBinding}/q/{destinationBinding}";

            if (errors.Any())
                return new FaultedResult<BindingInfo>{Errors = errors, DebugInfo = new (){URL = url, Request = definition.ToJsonString()}};

            return await Post<BindingInfo, BindingDefinition>(url, definition, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(string sourceBinding, string destinationBinding, string bindingName,
            string vhost, Action<DeleteBindingConfiguration> configuration,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new DeleteBindingConfigurationImpl();
            configuration?.Invoke(impl);

            impl.Validate();

            string virtualHost = vhost.ToSanitizedName();

            string url = impl.BindingType.Value == BindingType.Queue
                ? $"api/bindings/{virtualHost}/e/{sourceBinding}/q/{destinationBinding}/{bindingName}"
                : $"api/bindings/{virtualHost}/e/{sourceBinding}/e/{destinationBinding}/{bindingName}";
            
            if (impl.Errors.Value.Any())
                return new FaultedResult<BindingInfo>{Errors = impl.Errors.Value, DebugInfo = new (){URL = url, Request = null}};

            return await Delete(url, cancellationToken).ConfigureAwait(false);
        }

        
        class DeleteBindingConfigurationImpl :
            DeleteBindingConfiguration
        {
            BindingType _bindingType;
            string _vhost;
            bool _bindingCalled;
            bool _targetCalled;
            
            readonly List<Error> _errors;

            public Lazy<BindingType> BindingType { get; }
            public Lazy<List<Error>> Errors { get; }

            public DeleteBindingConfigurationImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                BindingType = new Lazy<BindingType>(() => _bindingType, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Configure(Action<DeleteBindingConfigurator> configurator)
            {
                _bindingCalled = true;
                
                var impl = new DeleteBindingConfiguratorImpl();
                configurator?.Invoke(impl);

                _bindingType = impl.BindingType;

                impl.Validate();
                
                _errors.AddRange(impl.Errors.Value);
            }

            public void Targeting(Action<BindingTarget> target)
            {
                _targetCalled = true;
                
                var impl = new BindingTargetImpl();
                target?.Invoke(impl);

                _vhost = impl.VirtualHostName;

                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new() {Reason = "The name of the virtual host is missing."});
            }

            public void Validate()
            {
                if (!_bindingCalled)
                {
                    _errors.Add(new() {Reason = "The name of the destination binding (queue/exchange) is missing."});
                    _errors.Add(new() {Reason = "The name of the binding is missing."});
                }

                if (!_targetCalled)
                    _errors.Add(new() {Reason = "The name of the virtual host is missing."});
            }

            
            class BindingTargetImpl :
                BindingTarget
            {
                public string VirtualHostName { get; private set; }

                public void VirtualHost(string name) => VirtualHostName = name;
            }


            class DeleteBindingConfiguratorImpl :
                DeleteBindingConfigurator
            {
                bool _destinationCalled;
                bool _nameCalled;
                bool _sourceCalled;

                readonly List<Error> _errors;

                public Lazy<List<Error>> Errors { get; }
                public string BindingName { get; private set; }
                public string BindingSource { get; private set; }
                public string BindingDestination { get; private set; }
                public BindingType BindingType { get; private set; }

                public DeleteBindingConfiguratorImpl()
                {
                    _errors = new List<Error>();
                
                    Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                }

                public void Name(string name)
                {
                    _nameCalled = true;
                    
                    BindingName = name;

                    if (string.IsNullOrWhiteSpace(name))
                        _errors.Add(new() {Reason = "The name of the binding is missing."});
                }

                public void Source(string binding)
                {
                    _sourceCalled = true;
                    
                    BindingSource = binding;

                    if (string.IsNullOrWhiteSpace(binding))
                        _errors.Add(new() {Reason = "The name of the source binding (queue/exchange) is missing."});
                }

                public void Destination(string binding)
                {
                    _destinationCalled = true;
                    
                    BindingDestination = binding;

                    if (string.IsNullOrWhiteSpace(binding))
                        _errors.Add(new() {Reason = "The name of the destination binding (queue/exchange) is missing."});
                }

                public void Type(BindingType bindingType) => BindingType = bindingType;

                public void Validate()
                {
                    if (!_nameCalled)
                        _errors.Add(new() {Reason = "The name of the binding is missing."});

                    if (!_destinationCalled)
                        _errors.Add(new() {Reason = "The name of the destination binding (queue/exchange) is missing."});

                    if (!_sourceCalled)
                        _errors.Add(new() {Reason = "The name of the source binding (queue/exchange) is missing."});
                }
            }
        }


        class NewBindingConfigurationImpl :
            NewBindingConfiguration
        {
            string _routingKey;
            IDictionary<string, ArgumentValue<object>> _arguments;

            readonly List<Error> _errors;

            public Lazy<BindingDefinition> Definition { get; }
            public Lazy<List<Error>> Errors { get; }

            public NewBindingConfigurationImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Definition = new Lazy<BindingDefinition>(() =>
                    new()
                    {
                        RoutingKey = _routingKey,
                        Arguments = _arguments.GetArguments()
                    }, LazyThreadSafetyMode.PublicationOnly);
            }

            public void HasRoutingKey(string routingKey) => _routingKey = routingKey;

            public void HasArguments(Action<BindingArguments> arguments)
            {
                var impl = new BindingArgumentsImpl();
                arguments?.Invoke(impl);

                _arguments = impl.Arguments;
                
                _errors.AddRange(_arguments
                    .Select(x => x.Value?.Error)
                    .Where(error => error.IsNotNull())
                    .ToList());
            }

            
            class BindingArgumentsImpl :
                BindingArguments
            {
                public IDictionary<string, ArgumentValue<object>> Arguments { get; }

                public BindingArgumentsImpl()
                {
                    Arguments = new Dictionary<string, ArgumentValue<object>>();
                }

                public void Set<T>(string arg, T value) =>
                    Arguments.Add(arg.Trim(),
                        Arguments.ContainsKey(arg)
                            ? new ArgumentValue<object>(value, $"Argument '{arg}' has already been set")
                            : new ArgumentValue<object>(value));
            }
        }
    }
}