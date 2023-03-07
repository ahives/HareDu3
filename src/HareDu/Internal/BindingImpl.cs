namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Extensions;
using Extensions;
using Model;
using Serialization;

class BindingImpl :
    BaseBrokerObject,
    Binding
{
    public BindingImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<Result<IReadOnlyList<BindingInfo>>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string url = "api/bindings";
            
        return await GetAllRequest<BindingInfo>(url, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<BindingInfo>> Create(string vhost, Action<BindingConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(configurator);

        cancellationToken.ThrowIfCancellationRequested();

        var impl = new BindingConfiguratorImpl();
        configurator?.Invoke(impl);

        impl.Validate();

        var request = impl.Request.Value;

        var errors = impl.Errors;

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        string virtualHost = vhost.ToSanitizedName();

        string url = impl.BindingEntityType == BindingType.Exchange
            ? $"api/bindings/{virtualHost}/e/{impl.SourceBinding}/e/{impl.DestinationBinding}"
            : $"api/bindings/{virtualHost}/e/{impl.SourceBinding}/q/{impl.DestinationBinding}";

        if (errors.Any())
            return new FaultedResult<BindingInfo>{DebugInfo = new (){URL = url, Request = request.ToJsonString(Deserializer.Options), Errors = errors}};

        return await PostRequest<BindingInfo, BindingRequest>(url, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string vhost, Action<DeleteBindingConfigurator> configurator,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(configurator);

        cancellationToken.ThrowIfCancellationRequested();

        var binding = new DeleteBindingConfiguratorImpl();
        configurator?.Invoke(binding);

        binding.Validate();

        var errors = binding.Errors;

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        string virtualHost = vhost.ToSanitizedName();

        string Normalize(string value) => string.IsNullOrWhiteSpace(value) ? string.Empty : value;

        string url = binding.BindingEntityType == BindingType.Queue
            ? $"api/bindings/{virtualHost}/e/{binding.SourceBinding}/q/{binding.DestinationBinding}/{Normalize(binding.BindingPropertiesKey)}"
            : $"api/bindings/{virtualHost}/e/{binding.SourceBinding}/e/{binding.DestinationBinding}/{Normalize(binding.BindingPropertiesKey)}";

        if (errors.Any())
            return new FaultedResult<BindingInfo>{DebugInfo = new (){URL = url, Errors = errors}};

        return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
    }

    
    class DeleteBindingConfiguratorImpl :
        DeleteBindingConfigurator
    {
        public string SourceBinding { get; private set; }
        public string DestinationBinding { get; private set; }
        public BindingType BindingEntityType { get; private set; }
        public string BindingPropertiesKey { get; private set; }
        public List<Error> Errors { get; }

        public DeleteBindingConfiguratorImpl()
        {
            Errors = new List<Error>();
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(SourceBinding))
                Errors.Add(new(){Reason = "The name of the source binding (queue/exchange) is missing."});

            if (string.IsNullOrWhiteSpace(DestinationBinding))
                Errors.Add(new(){Reason = "The name of the destination binding (queue/exchange) is missing."});
        }

        public void Source(string source) => SourceBinding = source;

        public void Destination(string destination) => DestinationBinding = destination;

        public void BindingType(BindingType bindingType) => BindingEntityType = bindingType;

        public void PropertiesKey(string propertiesKey) => BindingPropertiesKey = propertiesKey;
    }

    class BindingConfiguratorImpl :
        BindingConfigurator
    {
        IDictionary<string, object> _arguments;
        string _bindingKeyString;

        public string SourceBinding { get; private set; }
        public string DestinationBinding { get; private set; }
        public BindingType BindingEntityType { get; private set; }
        public List<Error> Errors { get; }
        public Lazy<BindingRequest> Request { get; }

        public BindingConfiguratorImpl()
        {
            Errors = new List<Error>();
            Request = new Lazy<BindingRequest>(
                () => new ()
                {
                    BindingKey = _bindingKeyString,
                    Arguments = _arguments
                }, LazyThreadSafetyMode.PublicationOnly);
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(SourceBinding))
                Errors.Add(new(){Reason = "The name of the source binding (queue/exchange) is missing."});

            if (string.IsNullOrWhiteSpace(DestinationBinding))
                Errors.Add(new(){Reason = "The name of the destination binding (queue/exchange) is missing."});
        }

        public void Source(string source) => SourceBinding = source;

        public void Destination(string destination) => DestinationBinding = destination;

        public void BindingType(BindingType bindingType) => BindingEntityType = bindingType;

        public void BindingKey(string bindingKey) => _bindingKeyString = bindingKey;

        public void OptionalArguments(Action<BindingArgumentConfigurator> configurator)
        {
            var impl = new BindingArgumentConfiguratorImpl();
            configurator?.Invoke(impl);

            _arguments = impl.Arguments.Value;

            impl.Validate();
            
            Errors.AddRange(impl.Errors);
        }

        
        class BindingArgumentConfiguratorImpl :
            BindingArgumentConfigurator
        {
            readonly IDictionary<string, ArgumentValue<object>> _arguments;

            public Lazy<IDictionary<string, object>> Arguments { get; }
            public List<Error> Errors { get; }

            public BindingArgumentConfiguratorImpl()
            {
                _arguments = new Dictionary<string, ArgumentValue<object>>();
                
                Errors = new List<Error>();
                Arguments = new Lazy<IDictionary<string, object>>(() => _arguments.GetArgumentsOrEmpty(), LazyThreadSafetyMode.PublicationOnly);
            }

            public void Validate() =>
                Errors.AddRange(_arguments
                    .Select(x => x.Value?.Error)
                    .Where(error => error is not null)
                    .ToList());

            public void Add<T>(string arg, T value) =>
                _arguments.Add(arg.Trim(),
                    _arguments.ContainsKey(arg)
                        ? new ArgumentValue<object>(value, $"Argument '{arg}' has already been set")
                        : new ArgumentValue<object>(value));
        }
    }
}