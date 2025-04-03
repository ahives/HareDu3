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

class BindingImpl :
    BaseBrokerImpl,
    Binding
{
    public BindingImpl(IHttpClientFactory clientFactory)
        : base(clientFactory)
    {
    }

    public async Task<Results<BindingInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
            
        return await GetAllRequest<BindingInfo>("api/bindings", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<BindingInfo>> Create(string vhost, Action<BindingConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator == null)
            return Panic.Result<BindingInfo>("api/bindings/{vhost}/e/{source}/{e|q}/destination", [new() {Reason = "No binding was defined."}]);

        var impl = new BindingConfiguratorImpl();
        configurator(impl);

        var request = impl.Request.Value;
        var errors = impl.Validate();
        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        string url = impl.BindingEntityType == BindingType.Exchange
            ? $"api/bindings/{sanitizedVHost}/e/{impl.SourceBinding}/e/{impl.DestinationBinding}"
            : $"api/bindings/{sanitizedVHost}/e/{impl.SourceBinding}/q/{impl.DestinationBinding}";

        if (errors.Count > 0)
            return Panic.Result<BindingInfo>(new() {URL = url, Request = request.ToJsonString(), Errors = errors});

        return await PostRequest<BindingInfo, BindingRequest>(url, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string vhost, Action<DeleteBindingConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator == null)
            return Panic.Result<BindingInfo>("api/bindings/{vhost}/e/{source}/{e|q}/destination/{properties_key}", [new() {Reason = "No binding was defined."}]);

        var impl = new DeleteBindingConfiguratorImpl();
        configurator(impl);

        var errors = impl.Validate();
        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        string Normalize(string value) => string.IsNullOrWhiteSpace(value) ? string.Empty : value;

        string url = impl.BindingEntityType == BindingType.Queue
            ? $"api/bindings/{sanitizedVHost}/e/{impl.SourceBinding}/q/{impl.DestinationBinding}/{Normalize(impl.BindingPropertiesKey)}"
            : $"api/bindings/{sanitizedVHost}/e/{impl.SourceBinding}/e/{impl.DestinationBinding}/{Normalize(impl.BindingPropertiesKey)}";

        if (errors.Count > 0)
            return Panic.Result(new() {URL = url, Errors = errors});

        return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
    }

    
    class DeleteBindingConfiguratorImpl :
        DeleteBindingConfigurator
    {
        List<Error> Errors { get; } = new();

        public string SourceBinding { get; private set; }
        public string DestinationBinding { get; private set; }
        public BindingType BindingEntityType { get; private set; }
        public string BindingPropertiesKey { get; private set; }

        public List<Error> Validate()
        {
            if (string.IsNullOrWhiteSpace(SourceBinding))
                Errors.Add(new(){Reason = "The name of the source binding (queue/exchange) is missing."});

            if (string.IsNullOrWhiteSpace(DestinationBinding))
                Errors.Add(new(){Reason = "The name of the destination binding (queue/exchange) is missing."});

            return Errors;
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

        List<Error> Errors { get; } = new();

        public string SourceBinding { get; private set; }
        public string DestinationBinding { get; private set; }
        public BindingType BindingEntityType { get; private set; }
        public Lazy<BindingRequest> Request { get; }

        public BindingConfiguratorImpl()
        {
            Request = new Lazy<BindingRequest>(
                () => new ()
                {
                    BindingKey = _bindingKeyString,
                    Arguments = _arguments
                }, LazyThreadSafetyMode.PublicationOnly);
        }

        public List<Error> Validate()
        {
            if (string.IsNullOrWhiteSpace(SourceBinding))
                Errors.Add(new(){Reason = "The name of the source binding (queue/exchange) is missing."});

            if (string.IsNullOrWhiteSpace(DestinationBinding))
                Errors.Add(new(){Reason = "The name of the destination binding (queue/exchange) is missing."});
            
            return Errors;
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
            
            Errors.AddRange(impl.Validate());
        }

        
        class BindingArgumentConfiguratorImpl :
            BindingArgumentConfigurator
        {
            readonly IDictionary<string, ArgumentValue<object>> _arguments;

            public Lazy<IDictionary<string, object>> Arguments { get; }

            public BindingArgumentConfiguratorImpl()
            {
                _arguments = new Dictionary<string, ArgumentValue<object>>();
                
                Arguments = new Lazy<IDictionary<string, object>>(() => _arguments.GetArgumentsOrEmpty(), LazyThreadSafetyMode.PublicationOnly);
            }

            public List<Error> Validate() =>
                _arguments
                    .Select(x => x.Value?.Error)
                    .Where(error => error is not null)
                    .ToList();

            public void Add<T>(string arg, T value) =>
                _arguments.Add(arg.Trim(),
                    _arguments.ContainsKey(arg)
                        ? new ArgumentValue<object>(value, $"Argument '{arg}' has already been set")
                        : new ArgumentValue<object>(value));
        }
    }
}