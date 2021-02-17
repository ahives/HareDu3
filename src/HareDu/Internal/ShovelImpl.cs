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

    class ShovelImpl :
        BaseBrokerObject,
        Shovel
    {
        public ShovelImpl(HttpClient client)
            : base(client)
        {
        }

        public async Task<ResultList<ShovelInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/shovels";
            
            return await GetAll<ShovelInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Create(Action<ShovelConfiguration> configuration,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new ShovelConfigurationImpl();
            configuration?.Invoke(impl);

            impl.Validate();
            
            ShovelDefinition definition = impl.Definition.Value;

            Debug.Assert(definition != null);

            string vhost = impl.VirtualHost.Value.ToSanitizedName();

            string url = $"api/parameters/shovel/{vhost}/{impl.ShovelName.Value}";

            if (impl.Errors.Value.Any())
                return new FaultedResult{Errors = impl.Errors.Value, DebugInfo = new () {URL = url, Request = definition.ToJsonString()}};

            return await Put(url, definition, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(Action<ShovelDeleteConfiguration> configuration,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new ShovelDeleteConfigurationImpl();
            configuration?.Invoke(impl);

            impl.Validate();

            string vhost = impl.VirtualHost.Value.ToSanitizedName();

            string url = $"api/parameters/shovel/{vhost}/{impl.ShovelName.Value}";

            if (impl.Errors.Value.Any())
                return new FaultedResult{Errors = impl.Errors.Value, DebugInfo = new () {URL = url, Request = null}};

            return await Delete(url, cancellationToken).ConfigureAwait(false);
        }

        
        class ShovelDeleteConfigurationImpl :
            ShovelDeleteConfiguration
        {
            string _vhost;
            string _shovelName;
            bool _virtualHostCalled;
            bool _shovelNameCalled;

            readonly List<Error> _errors;

            public Lazy<string> VirtualHost { get; }
            public Lazy<string> ShovelName { get; }
            public Lazy<List<Error>> Errors { get; }

            public ShovelDeleteConfigurationImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                VirtualHost = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
                ShovelName = new Lazy<string>(() => _shovelName, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Shovel(string name)
            {
                _shovelNameCalled = true;
                
                _shovelName = name;

                if (string.IsNullOrWhiteSpace(_shovelName))
                    _errors.Add(new () {Reason = "The name of the shovel is missing."});
            }

            public void Targeting(Action<ShovelTarget> target)
            {
                _virtualHostCalled = true;

                var impl = new ShovelTargetImpl();
                target?.Invoke(impl);

                _vhost = impl.VirtualHostName;

                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new () {Reason = "The name of the virtual host is missing."});
            }

            public void Validate()
            {
                if (!_virtualHostCalled)
                    _errors.Add(new () {Reason = "The name of the virtual host is missing."});

                if (!_shovelNameCalled)
                    _errors.Add(new () {Reason = "The name of the shovel is missing."});
            }
        }


        class ShovelConfigurationImpl :
            ShovelConfiguration
        {
            string _destinationQueue;
            string _destinationUri;
            string _destinationProtocol;
            string _sourceQueue;
            string _sourceUri;
            string _sourceProtocol;
            string _vhost;
            string _shovelName;
            string _sourceExchangeName;
            string _sourceExchangeRoutingKey;
            string _acknowledgeMode;
            int _reconnectDelay;
            bool _destinationAddForwardHeaders;
            bool _destinationAddTimestampHeader;
            long _sourcePrefetchCount;
            bool _configureCalled;
            bool _virtualHostCalled;
            bool _shovelCalled;

            readonly List<Error> _errors;

            public Lazy<ShovelDefinition> Definition { get; }
            public Lazy<string> VirtualHost { get; }
            public Lazy<string> ShovelName { get; }
            public Lazy<List<Error>> Errors { get; }

            public ShovelConfigurationImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Definition = new Lazy<ShovelDefinition>(() =>
                    new()
                    {
                        Value = new()
                        {
                            AcknowledgeMode = _acknowledgeMode,
                            ReconnectDelay = _reconnectDelay,
                            SourceProtocol = _sourceProtocol,
                            SourceUri = _sourceUri,
                            SourceQueue = _sourceQueue,
                            SourceExchange = _sourceExchangeName,
                            SourceExchangeKey = _sourceExchangeRoutingKey,
                            SourcePrefetchCount = _sourcePrefetchCount,
                            // SourceDeleteAfter = 
                            DestinationProtocol = _destinationProtocol,
                            DestinationUri = _destinationUri,
                            DestinationQueue = _destinationQueue,
                            DestinationAddForwardHeaders = _destinationAddForwardHeaders,
                            DestinationAddTimestampHeader = _destinationAddTimestampHeader
                        }
                    }, LazyThreadSafetyMode.PublicationOnly);
                VirtualHost = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
                ShovelName = new Lazy<string>(() => _shovelName, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Shovel(string name)
            {
                _shovelCalled = true;

                _shovelName = name;

                if (string.IsNullOrWhiteSpace(_shovelName))
                    _errors.Add(new () {Reason = "The name of the shovel is missing."});
            }

            public void Configure(Action<ShovelTargetConfigurator> configurator)
            {
                _configureCalled = true;
                
                var impl = new ShovelTargetConfiguratorImpl();
                configurator?.Invoke(impl);

                _sourceProtocol = impl.SourceProtocol;
                _sourceUri = impl.SourceUri;
                _sourceQueue = impl.SourceQueue;
                _sourceExchangeName = impl.SourceExchangeName;
                _sourceExchangeRoutingKey = impl.SourceExchangeRoutingKey;
                _destinationProtocol = impl.DestinationProtocol;
                _destinationUri = impl.DestinationUri;
                _destinationQueue = impl.DestinationQueue;
                _acknowledgeMode = impl.AckMode;
                _reconnectDelay = impl.ReconnectDelayInSeconds;
                _destinationAddForwardHeaders = impl.DestinationAddHeaders;
                _destinationAddTimestampHeader = impl.DestinationAddTimestampHeader;
                _sourcePrefetchCount = impl.SourcePrefetchCount;

                if (string.IsNullOrWhiteSpace(_sourceProtocol))
                    _errors.Add(new() {Reason = "The name of the source protocol is missing."});

                if (string.IsNullOrWhiteSpace(_sourceUri))
                    _errors.Add(new() {Reason = "The name of the source URI is missing."});

                if (string.IsNullOrWhiteSpace(_sourceQueue))
                    _errors.Add(new() {Reason = "The name of the source queue is missing."});

                if (string.IsNullOrWhiteSpace(_destinationProtocol))
                    _errors.Add(new() {Reason = "The name of the destination protocol is missing."});

                if (string.IsNullOrWhiteSpace(_destinationUri))
                    _errors.Add(new() {Reason = "The name of the destination URI is missing."});

                if (string.IsNullOrWhiteSpace(_destinationQueue))
                    _errors.Add(new() {Reason = "The name of the destination queue is missing."});
            }

            public void Targeting(Action<ShovelTarget> target)
            {
                _virtualHostCalled = true;

                var impl = new ShovelTargetImpl();
                target?.Invoke(impl);

                _vhost = impl.VirtualHostName;
                
                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new () {Reason = "The name of the virtual host is missing."});
            }

            public void Validate()
            {
                if (!_virtualHostCalled)
                    _errors.Add(new () {Reason = "The name of the virtual host is missing."});

                if (!_configureCalled)
                {
                    _errors.Add(new () {Reason = "The name of the shovel is missing."});
                    _errors.Add(new() {Reason = "The name of the source protocol is missing."});
                    _errors.Add(new() {Reason = "The name of the source URI is missing."});
                    _errors.Add(new() {Reason = "The name of the source queue is missing."});
                    _errors.Add(new() {Reason = "The name of the destination protocol is missing."});
                    _errors.Add(new() {Reason = "The name of the destination URI is missing."});
                    _errors.Add(new() {Reason = "The name of the destination queue is missing."});
                }

                if (!_shovelCalled)
                    _errors.Add(new () {Reason = "The name of the shovel is missing."});
            }


            class ShovelTargetConfiguratorImpl :
                ShovelTargetConfigurator
            {
                public string SourceProtocol { get; private set; }
                public string SourceUri { get; private set; }
                public string SourceQueue { get; private set; }
                public string SourceExchangeName { get; private set; }
                public string SourceExchangeRoutingKey { get; private set; }
                public long SourcePrefetchCount { get; private set; }
                public string DestinationProtocol { get; private set; }
                public string DestinationUri { get; private set; }
                public string DestinationQueue { get; private set; }
                public int ReconnectDelayInSeconds { get; private set; }
                public string AckMode { get; private set; }
                public string DestinationExchangeName { get; private set; }
                public string DestinationExchangeRoutingKey { get; private set; }
                public bool DestinationAddHeaders { get; private set; }
                public bool DestinationAddTimestampHeader { get; private set; }

                public void ReconnectDelay(int delay) => ReconnectDelayInSeconds = delay;

                public void AcknowledgeMode(AcknowledgeMode mode) => AckMode = mode.Convert();

                public void Source(Action<ShovelSourceConfiguration> definition)
                {
                    var impl = new ShovelSourceConfigurationImpl();
                    definition?.Invoke(impl);

                    SourceProtocol = impl.ShovelProtocol;
                    SourceUri = impl.ShovelUri;
                    SourceQueue = impl.ShovelQueue;
                    SourceExchangeName = impl.ExchangeName;
                    SourceExchangeRoutingKey = impl.ExchangeRoutingKey;
                    SourcePrefetchCount = impl.PrefetchCount;
                }

                public void Destination(Action<ShovelDestinationConfiguration> definition)
                {
                    var impl = new ShovelDestinationConfigurationImpl();
                    definition?.Invoke(impl);

                    DestinationProtocol = impl.ShovelProtocol;
                    DestinationUri = impl.ShovelUri;
                    DestinationQueue = impl.ShovelQueue;
                    DestinationExchangeName = impl.ExchangeName;
                    DestinationExchangeRoutingKey = impl.ExchangeRoutingKey;
                    DestinationAddHeaders = impl.AddHeaders;
                    DestinationAddTimestampHeader = impl.AddTimestampHeader;
                }

                
                class ShovelDestinationConfigurationImpl :
                    ShovelDestinationConfiguration
                {
                    public string ShovelProtocol { get; private set; }
                    public string ShovelUri { get; private set; }
                    public string ShovelQueue { get; private set; }
                    public string ExchangeName { get; private set; }
                    public string ExchangeRoutingKey { get; private set; }
                    public bool AddHeaders { get; private set; }
                    public bool AddTimestampHeader { get; private set; }

                    public void Protocol(Protocol protocol) => ShovelProtocol = protocol.Convert();

                    public void Uri(string uri) => ShovelUri = uri;

                    public void Queue(string queue) => ShovelQueue = queue;

                    public void Exchange(string exchange, string routingKey)
                    {
                        ExchangeName = exchange;
                        ExchangeRoutingKey = routingKey;
                    }

                    public void AddForwardHeaders() => AddHeaders = true;

                    public void AddTimestampHeaderToMessage() => AddTimestampHeader = true;
                }


                class ShovelSourceConfigurationImpl :
                    ShovelSourceConfiguration
                {
                    public string ShovelProtocol { get; private set; }
                    public string ShovelUri { get; private set; }
                    public string ShovelQueue { get; private set; }
                    public string ExchangeName { get; private set; }
                    public string ExchangeRoutingKey { get; private set; }
                    public long PrefetchCount { get; private set; }

                    public void Protocol(Protocol protocol) => ShovelProtocol = protocol.Convert();

                    public void Uri(string uri) => ShovelUri = uri;

                    public void Queue(string queue) => ShovelQueue = queue;
                    
                    public void DeleteAfter()
                    {
                        throw new NotImplementedException();
                    }

                    public void MaxCopiedMessages(long messages) => PrefetchCount = messages < 1000 ? 1000 : messages;

                    public void Exchange(string exchange, string routingKey)
                    {
                        ExchangeName = exchange;
                        ExchangeRoutingKey = routingKey;
                    }
                }
            }
        }

        
        class ShovelTargetImpl :
            ShovelTarget
        {
            public string VirtualHostName { get; private set; }

            public void VirtualHost(string name) => VirtualHostName = name;
        }
    }
}