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

        public async Task<Result<BindingInfo>> Create(Action<BindingCreateAction> action, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new BindingCreateActionImpl();
            action?.Invoke(impl);

            impl.Validate();
            
            BindingDefinition definition = impl.Definition.Value;

            Debug.Assert(definition != null);

            string sourceBinding = impl.SourceBinding.Value;
            string destinationBinding = impl.DestinationBinding.Value;
            BindingType bindingType = impl.BindingType.Value;
            string vhost = impl.VirtualHost.Value.ToSanitizedName();

            string url = bindingType == BindingType.Exchange
                ? $"api/bindings/{vhost}/e/{sourceBinding}/e/{destinationBinding}"
                : $"api/bindings/{vhost}/e/{sourceBinding}/q/{destinationBinding}";

            if (impl.Errors.Value.Any())
                return new FaultedResult<BindingInfo>{Errors = impl.Errors.Value, DebugInfo = new (){URL = url, Request = definition.ToJsonString()}};

            return await Post<BindingInfo, BindingDefinition>(url, definition, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(Action<BindingDeleteAction> action, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new BindingDeleteActionImpl();
            action?.Invoke(impl);

            impl.Validate();

            string destination = impl.BindingDestination.Value;
            string vhost = impl.VirtualHost.Value.ToSanitizedName();
            string bindingName = impl.BindingName.Value;
            string source = impl.BindingSource.Value;
            BindingType bindingType = impl.BindingType.Value;

            string url = bindingType == BindingType.Queue
                ? $"api/bindings/{vhost}/e/{source}/q/{destination}/{bindingName}"
                : $"api/bindings/{vhost}/e/{source}/e/{destination}/{bindingName}";
            
            if (impl.Errors.Value.Any())
                return new FaultedResult<BindingInfo>{Errors = impl.Errors.Value, DebugInfo = new (){URL = url, Request = null}};

            return await Delete(url, cancellationToken).ConfigureAwait(false);
        }

        
        class BindingDeleteActionImpl :
            BindingDeleteAction
        {
            BindingType _bindingType;
            string _bindingName;
            string _bindingSource;
            string _bindingDestination;
            string _vhost;
            readonly List<Error> _errors;
            bool _bindingCalled;
            bool _targetCalled;

            public Lazy<string> VirtualHost { get; }
            public Lazy<BindingType> BindingType { get; }
            public Lazy<string> BindingName { get; }
            public Lazy<string> BindingSource { get; }
            public Lazy<string> BindingDestination { get; }
            public Lazy<List<Error>> Errors { get; }

            public BindingDeleteActionImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                BindingType = new Lazy<BindingType>(() => _bindingType, LazyThreadSafetyMode.PublicationOnly);
                BindingName = new Lazy<string>(() => _bindingName, LazyThreadSafetyMode.PublicationOnly);
                BindingSource = new Lazy<string>(() => _bindingSource, LazyThreadSafetyMode.PublicationOnly);
                BindingDestination = new Lazy<string>(() => _bindingDestination, LazyThreadSafetyMode.PublicationOnly);
                VirtualHost = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Binding(Action<BindingDeleteDefinition> definition)
            {
                _bindingCalled = true;
                
                var impl = new BindingDeleteDefinitionImpl();
                definition?.Invoke(impl);

                _bindingType = impl.BindingType;
                _bindingName = impl.BindingName;
                _bindingSource = impl.BindingSource;
                _bindingDestination = impl.DestinationSource;

                impl.Verify();
                
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


            class BindingDeleteDefinitionImpl :
                BindingDeleteDefinition
            {
                readonly List<Error> _errors;
                bool _destinationCalled;
                bool _nameCalled;

                public Lazy<List<Error>> Errors { get; }

                public BindingDeleteDefinitionImpl()
                {
                    _errors = new List<Error>();
                
                    Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                }

                public string BindingName { get; private set; }
                public string BindingSource { get; private set; }
                public string DestinationSource { get; private set; }
                public BindingType BindingType { get; private set; }

                public void Name(string name)
                {
                    _nameCalled = true;
                    
                    BindingName = name;

                    if (string.IsNullOrWhiteSpace(name))
                        _errors.Add(new() {Reason = "The name of the binding is missing."});
                }

                public void Source(string binding) => BindingSource = binding;

                public void Destination(string binding)
                {
                    _destinationCalled = true;
                    
                    DestinationSource = binding;

                    if (string.IsNullOrWhiteSpace(binding))
                        _errors.Add(new() {Reason = "The name of the destination binding (queue/exchange) is missing."});
                }

                public void Type(BindingType bindingType) => BindingType = bindingType;

                public void Verify()
                {
                    if (!_nameCalled)
                        _errors.Add(new() {Reason = "The name of the binding is missing."});

                    if (!_destinationCalled)
                        _errors.Add(new() {Reason = "The name of the destination binding (queue/exchange) is missing."});
                }
            }
        }


        class BindingCreateActionImpl :
            BindingCreateAction
        {
            string _routingKey;
            IDictionary<string, ArgumentValue<object>> _arguments;
            string _vhost;
            string _sourceBinding;
            string _destinationBinding;
            BindingType _bindingType;
            readonly List<Error> _errors;
            bool _bindingCalled;
            bool _targetCalled;

            public Lazy<BindingDefinition> Definition { get; }
            public Lazy<string> SourceBinding { get; }
            public Lazy<string> DestinationBinding { get; }
            public Lazy<string> VirtualHost { get; }
            public Lazy<BindingType> BindingType { get; }
            public Lazy<List<Error>> Errors { get; }

            public BindingCreateActionImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Definition = new Lazy<BindingDefinition>(() => new()
                {
                    RoutingKey = _routingKey,
                    Arguments = _arguments.GetArguments()
                }, LazyThreadSafetyMode.PublicationOnly);
                SourceBinding = new Lazy<string>(() => _sourceBinding, LazyThreadSafetyMode.PublicationOnly);
                DestinationBinding = new Lazy<string>(() => _destinationBinding, LazyThreadSafetyMode.PublicationOnly);
                VirtualHost = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
                BindingType = new Lazy<BindingType>(() => _bindingType, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Binding(Action<BindingCreateDefinition> definition)
            {
                _bindingCalled = true;
                
                var impl = new BindingCreateDefinitionImpl();
                definition?.Invoke(impl);
                
                _sourceBinding = impl.SourceBinding;
                _destinationBinding = impl.DestinationBinding;
                _bindingType = impl.BindingType;

                if (string.IsNullOrWhiteSpace(_sourceBinding))
                    _errors.Add(new() {Reason = "The name of the source binding (queue/exchange) is missing."});

                if (string.IsNullOrWhiteSpace(_destinationBinding))
                    _errors.Add(new() {Reason = "The name of the destination binding (queue/exchange) is missing."});
            }

            public void Configure(Action<BindingConfiguration> configuration)
            {
                var impl = new BindingConfigurationImpl();
                configuration?.Invoke(impl);

                _arguments = impl.Arguments;
                _routingKey = impl.RoutingKey;
                
                _errors.AddRange(_arguments
                    .Select(x => x.Value?.Error)
                    .Where(error => error.IsNotNull())
                    .ToList());
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
                    _errors.Add(new() {Reason = "The name of the source binding (queue/exchange) is missing."});
                    _errors.Add(new() {Reason = "The name of the destination binding (queue/exchange) is missing."});
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


            class BindingCreateDefinitionImpl :
                BindingCreateDefinition
            {
                public string SourceBinding { get; private set; }
                public string DestinationBinding { get; private set; }
                public BindingType BindingType { get; private set; }

                public void Source(string binding) => SourceBinding = binding;

                public void Destination(string binding) => DestinationBinding = binding;

                public void Type(BindingType bindingType) => BindingType = bindingType;
            }


            class BindingConfigurationImpl :
                BindingConfiguration
            {
                public string RoutingKey { get; private set; }
                public IDictionary<string, ArgumentValue<object>> Arguments { get; private set; }

                public void HasRoutingKey(string routingKey) => RoutingKey = routingKey;

                public void HasArguments(Action<BindingArguments> arguments)
                {
                    var impl = new BindingArgumentsImpl();
                    arguments?.Invoke(impl);

                    Arguments = impl.Arguments;
                }
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