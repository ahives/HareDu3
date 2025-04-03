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

class ShovelImpl :
    BaseBrokerImpl,
    Shovel
{
    public ShovelImpl(IHttpClientFactory clientFactory)
        : base(clientFactory)
    {
    }

    public async Task<Results<ShovelInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<ShovelInfo>("api/shovels", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(string name, string vhost,
        Action<ShovelConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator == null)
            return Panic.Result("api/parameters/shovel/{vhost}/{name}", [new() {Reason = "The shovel configurator is missing."}]);

        var impl = new ShovelConfiguratorImpl();
        configurator?.Invoke(impl);

        var errors = new List<Error>();
        var request = impl.Request.Value;
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddRange(impl.Validate());

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new (){Reason = "The name of the shovel is missing."});

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        if (errors.Count > 0)
            return Panic.Result("api/parameters/shovel/{vhost}/{name}", errors, request.ToJsonString());

        return await PutRequest($"api/parameters/shovel/{sanitizedVHost}/{name}", request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string name, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new (){Reason = "The name of the shovel is missing."});

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        if (errors.Count > 0)
            return Panic.Result("api/parameters/shovel/{vhost}/{name}", errors);

        return await DeleteRequest($"api/parameters/shovel/{sanitizedVHost}/{name}", cancellationToken).ConfigureAwait(false);
    }


    class ShovelConfiguratorImpl :
        ShovelConfigurator
    {
        string _uri;
        ShovelProtocolType _destinationProtocol;
        ShovelProtocolType _sourceProtocol;
        AckMode _acknowledgeMode;
        string _destinationQueue;
        string _sourceQueue;
        string _sourceExchangeName;
        string _sourceExchangeRoutingKey;
        string _destinationExchangeName;
        string _destinationExchangeRoutingKey;
        int _reconnectDelay;
        ulong _sourcePrefetchCount;
        bool _destinationAddForwardHeaders;
        bool _destinationAddTimestampHeader;
        bool _sourceCalled;
        bool _destinationCalled;
        object _deleteShovelAfter;

        List<Error> Errors { get; } = new();

        public Lazy<ShovelRequest> Request { get; }

        public ShovelConfiguratorImpl()
        {
            Request = new Lazy<ShovelRequest>(
                () => new()
                {
                    Value = new ShovelRequestParams
                    {
                        AcknowledgeMode = _acknowledgeMode,
                        SourceExchange = _sourceExchangeName,
                        SourceProtocol = _sourceProtocol,
                        SourceQueue = _sourceQueue,
                        SourceUri = _uri,
                        SourceDeleteAfter = _deleteShovelAfter,
                        SourcePrefetchCount = _sourcePrefetchCount,
                        SourceExchangeRoutingKey = _sourceExchangeRoutingKey,
                        ReconnectDelay = _reconnectDelay,
                        DestinationExchange = _destinationExchangeName,
                        DestinationProtocol = _destinationProtocol,
                        DestinationQueue = _destinationQueue,
                        DestinationUri = _uri,
                        DestinationExchangeKey = _destinationExchangeRoutingKey,
                        DestinationAddForwardHeaders = _destinationAddForwardHeaders,
                        DestinationAddTimestampHeader = _destinationAddTimestampHeader
                    }
                }, LazyThreadSafetyMode.PublicationOnly);
        }

        public void Uri(string uri) => _uri = uri;

        public void ReconnectDelay(int delayInSeconds) => _reconnectDelay = delayInSeconds < 1 ? 1 : delayInSeconds;

        public void AcknowledgementMode(AckMode mode) => _acknowledgeMode = mode;

        public void Source(string queue, Action<ShovelSourceConfigurator> configurator)
        {
            _sourceCalled = true;
                
            var impl = new ShovelSourceConfiguratorImpl();
            configurator?.Invoke(impl);

            _sourceProtocol = impl.ShovelProtocol;
            _sourceQueue = queue;
            _sourceExchangeName = impl.ExchangeName;
            _sourceExchangeRoutingKey = impl.ExchangeRoutingKey;
            _sourcePrefetchCount = impl.PrefetchCount;
            _deleteShovelAfter = impl.DeleteAfterShovel;
        }

        public void Destination(string queue, Action<ShovelDestinationConfigurator> configurator)
        {
            _destinationCalled = true;
                
            var impl = new ShovelDestinationConfiguratorImpl();
            configurator?.Invoke(impl);

            _destinationProtocol = impl.ShovelProtocol;
            _destinationQueue = queue;
            _destinationExchangeName = impl.ExchangeName;
            _destinationExchangeRoutingKey = impl.ExchangeRoutingKey;
            _destinationAddForwardHeaders = impl.AddHeaders;
            _destinationAddTimestampHeader = impl.AddTimestampHeader;
        }

        public ShovelRequest BuildRequest()
        {
            var requestParams = new ShovelRequestParams
            {
                AcknowledgeMode = _acknowledgeMode,
                SourceExchange = _sourceExchangeName,
                SourceProtocol = _sourceProtocol,
                SourceQueue = _sourceQueue,
                SourceUri = _uri,
                SourceDeleteAfter = _deleteShovelAfter,
                SourcePrefetchCount = _sourcePrefetchCount,
                SourceExchangeRoutingKey = _sourceExchangeRoutingKey,
                ReconnectDelay = _reconnectDelay,
                DestinationExchange = _destinationExchangeName,
                DestinationProtocol = _destinationProtocol,
                DestinationQueue = _destinationQueue,
                DestinationUri = _uri,
                DestinationExchangeKey = _destinationExchangeRoutingKey,
                DestinationAddForwardHeaders = _destinationAddForwardHeaders,
                DestinationAddTimestampHeader = _destinationAddTimestampHeader
            };
            
            var request = new ShovelRequest
            {
                Value = requestParams
            };

            return request;
        }

        public List<Error> Validate()
        {
            if (_sourceCalled)
            {
                if (string.IsNullOrWhiteSpace(_sourceQueue) && string.IsNullOrWhiteSpace(_sourceExchangeName))
                    Errors.Add(new (){Reason = "Both source queue and exchange missing."});
                
                if (!string.IsNullOrWhiteSpace(_sourceQueue) && !string.IsNullOrWhiteSpace(_sourceExchangeName))
                    Errors.Add(new (){Reason = "Both source queue and exchange cannot be present."});
            }
            else
            {
                Errors.Add(new(){Reason = "The name of the source protocol is missing."});
                Errors.Add(new (){Reason = "Both source queue and exchange cannot be present."});
            }

            if (_destinationCalled)
            {
                if (string.IsNullOrWhiteSpace(_destinationQueue) && string.IsNullOrWhiteSpace(_destinationExchangeName))
                    Errors.Add(new (){Reason = "Both source queue and exchange missing."});
                
                if (!string.IsNullOrWhiteSpace(_destinationQueue) && !string.IsNullOrWhiteSpace(_destinationExchangeName))
                    Errors.Add(new (){Reason = "Both destination queue and exchange cannot be present."});
            }
            else
            {
                Errors.Add(new(){Reason = "The name of the destination protocol is missing."});
                Errors.Add(new (){Reason = "Both destination queue and exchange cannot be present."});
            }
                
            if (string.IsNullOrWhiteSpace(_uri))
                Errors.Add(new(){Reason = "The connection URI is missing."});
            
            return Errors;
        }


        class ShovelSourceConfiguratorImpl :
            ShovelSourceConfigurator
        {
            public ShovelProtocolType ShovelProtocol { get; private set; }
            public string ExchangeName { get; private set; }
            public string ExchangeRoutingKey { get; private set; }
            public ulong PrefetchCount { get; private set; }
            public object DeleteAfterShovel { get; private set; }

            public ShovelSourceConfiguratorImpl()
            {
                ShovelProtocol = ShovelProtocolType.Amqp091;
                PrefetchCount = 1000;
            }

            public void Protocol(ShovelProtocolType protocol) => ShovelProtocol = protocol;
                
            public void DeleteAfter(DeleteShovelMode mode) => DeleteAfterShovel = mode.Convert();

            public void DeleteAfter(uint messages) => DeleteAfterShovel = messages;

            public void MaxCopiedMessages(ulong messages) => PrefetchCount = messages < 1000 ? 1000 : messages;

            public void Exchange(string exchange, string routingKey = null)
            {
                ExchangeName = exchange;

                if (routingKey is null)
                    return;

                ExchangeRoutingKey = routingKey;
            }
        }

                
        class ShovelDestinationConfiguratorImpl :
            ShovelDestinationConfigurator
        {
            public ShovelProtocolType ShovelProtocol { get; private set; }
            public string ExchangeName { get; private set; }
            public string ExchangeRoutingKey { get; private set; }
            public bool AddHeaders { get; private set; }
            public bool AddTimestampHeader { get; private set; }

            public ShovelDestinationConfiguratorImpl()
            {
                ShovelProtocol = ShovelProtocolType.Amqp091;
            }

            public void Protocol(ShovelProtocolType protocol) => ShovelProtocol = protocol;

            public void Exchange(string exchange, string routingKey = null)
            {
                ExchangeName = exchange;

                if (routingKey is null)
                    return;

                ExchangeRoutingKey = routingKey;
            }

            public void AddForwardHeaders() => AddHeaders = true;

            public void AddTimestampHeaderToMessage() => AddTimestampHeader = true;
        }
    }
}