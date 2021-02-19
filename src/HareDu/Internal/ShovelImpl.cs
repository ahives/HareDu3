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

        public async Task<Result> Create(string shovel, string vhost, Action<ShovelConfigurator> configurator = null,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new ShovelConfiguratorImpl();
            configurator?.Invoke(impl);

            impl.Validate();
            
            ShovelDefinition definition = impl.Definition.Value;

            Debug.Assert(definition != null);

            var errors = new List<Error>();
            
            errors.AddRange(impl.Errors.Value);
            
            if (string.IsNullOrWhiteSpace(shovel))
                errors.Add(new () {Reason = "The name of the shovel is missing."});
            
            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new () {Reason = "The name of the virtual host is missing."});

            string url = $"api/parameters/shovel/{vhost.ToSanitizedName()}/{shovel}";

            if (errors.Any())
                return new FaultedResult{DebugInfo = new () {URL = url, Request = definition.ToJsonString(), Errors = errors}};

            return await Put(url, definition, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(string shovel, string vhost, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var errors = new List<Error>();
            
            if (string.IsNullOrWhiteSpace(shovel))
                errors.Add(new () {Reason = "The name of the shovel is missing."});

            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new () {Reason = "The name of the virtual host is missing."});

            string url = $"api/parameters/shovel/{vhost.ToSanitizedName()}/{shovel}";

            if (errors.Any())
                return new FaultedResult{DebugInfo = new () {URL = url, Errors = errors}};

            return await Delete(url, cancellationToken).ConfigureAwait(false);
        }


        class ShovelConfiguratorImpl :
            ShovelConfigurator
        {
            string _destinationQueue;
            string _destinationUri;
            string _destinationProtocol;
            string _sourceQueue;
            string _sourceUri;
            string _sourceProtocol;
            string _sourceExchangeName;
            string _sourceExchangeRoutingKey;
            string _acknowledgeMode;
            string _destinationExchangeName;
            string _destinationExchangeRoutingKey;
            int _reconnectDelay;
            bool _destinationAddForwardHeaders;
            bool _destinationAddTimestampHeader;
            long _sourcePrefetchCount;
            bool _sourceCalled;
            bool _destinationCalled;

            readonly List<Error> _errors;

            public Lazy<ShovelDefinition> Definition { get; }
            public Lazy<string> VirtualHost { get; }
            public Lazy<string> ShovelName { get; }
            public Lazy<List<Error>> Errors { get; }

            public ShovelConfiguratorImpl()
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
                            SourceExchangeRoutingKey = _sourceExchangeRoutingKey,
                            SourcePrefetchCount = _sourcePrefetchCount,
                            // SourceDeleteAfter = 
                            DestinationProtocol = _destinationProtocol,
                            DestinationExchange = _destinationExchangeName,
                            DestinationExchangeKey = _destinationExchangeRoutingKey,
                            DestinationUri = _destinationUri,
                            DestinationQueue = _destinationQueue,
                            DestinationAddForwardHeaders = _destinationAddForwardHeaders,
                            DestinationAddTimestampHeader = _destinationAddTimestampHeader
                        }
                    }, LazyThreadSafetyMode.PublicationOnly);
            }

            public void ReconnectDelay(int delay) => _reconnectDelay = delay;

            public void AcknowledgeMode(AcknowledgeMode mode) => _acknowledgeMode = mode.Convert();

            public void Source(string queue, string uri, Action<ShovelSourceConfigurator> configurator)
            {
                _sourceCalled = true;
                
                var impl = new ShovelSourceConfiguratorImpl();
                configurator?.Invoke(impl);

                _sourceProtocol = impl.ShovelProtocol;
                _sourceUri = uri;
                _sourceQueue = queue;
                _sourceExchangeName = impl.ExchangeName;
                _sourceExchangeRoutingKey = impl.ExchangeRoutingKey;
                _sourcePrefetchCount = impl.PrefetchCount;
            }

            public void Destination(string queue, string uri, Action<ShovelDestinationConfigurator> configurator)
            {
                _destinationCalled = true;
                
                var impl = new ShovelDestinationConfiguratorImpl();
                configurator?.Invoke(impl);

                _destinationProtocol = impl.ShovelProtocol;
                _destinationUri = uri;
                _destinationQueue = queue;
                _destinationExchangeName = impl.ExchangeName;
                _destinationExchangeRoutingKey = impl.ExchangeRoutingKey;
                _destinationAddForwardHeaders = impl.AddHeaders;
                _destinationAddTimestampHeader = impl.AddTimestampHeader;
            }

            public void Validate()
            {
                if (!_sourceCalled)
                {
                    _errors.Add(new() {Reason = "The name of the source protocol is missing."});
                    _errors.Add(new() {Reason = "The name of the source URI is missing."});
                    _errors.Add(new() {Reason = "The name of the source queue is missing."});
                }

                if (!_destinationCalled)
                {
                    _errors.Add(new() {Reason = "The name of the destination protocol is missing."});
                    _errors.Add(new() {Reason = "The name of the destination URI is missing."});
                    _errors.Add(new() {Reason = "The name of the destination queue is missing."});
                }
            }


            class ShovelSourceConfiguratorImpl :
                ShovelSourceConfigurator
            {
                public string ShovelProtocol { get; private set; }
                public string ExchangeName { get; private set; }
                public string ExchangeRoutingKey { get; private set; }
                public long PrefetchCount { get; private set; }

                public void Protocol(Protocol protocol) => ShovelProtocol = protocol.Convert();
                    
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

                
            class ShovelDestinationConfiguratorImpl :
                ShovelDestinationConfigurator
            {
                public string ShovelProtocol { get; private set; }
                public string ExchangeName { get; private set; }
                public string ExchangeRoutingKey { get; private set; }
                public bool AddHeaders { get; private set; }
                public bool AddTimestampHeader { get; private set; }

                public void Protocol(Protocol protocol) => ShovelProtocol = protocol.Convert();

                public void Exchange(string exchange, string routingKey)
                {
                    ExchangeName = exchange;
                    ExchangeRoutingKey = routingKey;
                }

                public void AddForwardHeaders() => AddHeaders = true;

                public void AddTimestampHeaderToMessage() => AddTimestampHeader = true;
            }
        }
    }
}