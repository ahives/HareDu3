namespace HareDu.Internal;

using System;
using System.Collections.Generic;
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
    public ShovelImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<Results<ShovelInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<ShovelInfo>("api/shovels", RequestType.Shovel, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(
        string name,
        string vhost,
        Action<ShovelConfigurator> configurator,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator is null)
            return Response.Panic(Debug.Info("api/parameters/shovel/{vhost}/{name}",
                Errors.Create(e => { e.Add("The shovel configurator is missing."); })));

        var impl = new ShovelConfiguratorImpl();
        configurator(impl);

        var errors = impl.Validate();
        var request = impl.Request.Value;
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(name, string.IsNullOrWhiteSpace, Errors.Create("The name of the shovel is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("api/parameters/shovel/{vhost}/{name}", errors, request.ToJsonString()))
            : await PutRequest($"api/parameters/shovel/{sanitizedVHost}/{name}", request, RequestType.Shovel, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string name, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(name, string.IsNullOrWhiteSpace, Errors.Create("The name of the shovel is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("api/parameters/shovel/{vhost}/{name}", errors))
            : await DeleteRequest($"api/parameters/shovel/{sanitizedVHost}/{name}", RequestType.Shovel, cancellationToken).ConfigureAwait(false);
    }


    class ShovelConfiguratorImpl :
        ShovelConfigurator
    {
        string _uri;
        ShovelProtocol _destinationProtocol;
        ShovelProtocol _sourceProtocol;
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

        List<Error> InternalErrors { get; } = new();

        public Lazy<ShovelRequest> Request { get; }

        public ShovelConfiguratorImpl()
        {
            _sourceProtocol = ShovelProtocol.Amqp091;
            _destinationProtocol = ShovelProtocol.Amqp091;

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

        public void Source(string queue, ShovelProtocol protocol, Action<ShovelSourceConfigurator> configurator = null)
        {
            _sourceCalled = true;
                
            var impl = new ShovelSourceConfiguratorImpl();
            configurator?.Invoke(impl);

            _sourceProtocol = protocol;
            _sourceQueue = queue;
            _sourceExchangeName = impl.ExchangeName;
            _sourceExchangeRoutingKey = impl.ExchangeRoutingKey;
            _sourcePrefetchCount = impl.PrefetchCount;
            _deleteShovelAfter = impl.DeleteAfterShovel;
        }

        public void Destination(string queue, ShovelProtocol protocol, Action<ShovelDestinationConfigurator> configurator = null)
        {
            _destinationCalled = true;
                
            var impl = new ShovelDestinationConfiguratorImpl();
            configurator?.Invoke(impl);

            _destinationProtocol = protocol;
            _destinationQueue = queue;
            _destinationExchangeName = impl.ExchangeName;
            _destinationExchangeRoutingKey = impl.ExchangeRoutingKey;
            _destinationAddForwardHeaders = impl.AddHeaders;
            _destinationAddTimestampHeader = impl.AddTimestampHeader;
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
                InternalErrors.AddIfTrue(_sourceQueue, _sourceExchangeName,
                    (x, y) => string.IsNullOrWhiteSpace(x) && string.IsNullOrWhiteSpace(y),
                    Errors.Create("Both source queue and exchange missing."));
                
                InternalErrors.AddIfTrue(_sourceQueue, _sourceExchangeName,
                    (x, y) => !string.IsNullOrWhiteSpace(x) && !string.IsNullOrWhiteSpace(y),
                    Errors.Create("Both source queue and exchange cannot be present."));
            }
            else
            {
                InternalErrors.Add(Errors.Create("The name of the source protocol is missing."));
                InternalErrors.Add(Errors.Create("Both source queue and exchange cannot be present."));
            }

            if (_destinationCalled)
            {
                InternalErrors.AddIfTrue(_destinationQueue, _destinationExchangeName,
                    (x, y) => string.IsNullOrWhiteSpace(x) && string.IsNullOrWhiteSpace(y),
                    Errors.Create("Both source queue and exchange missing."));

                InternalErrors.AddIfTrue(_destinationQueue, _destinationExchangeName,
                    (x, y) => !string.IsNullOrWhiteSpace(x) && !string.IsNullOrWhiteSpace(y),
                    Errors.Create("Both destination queue and exchange cannot be present."));
            }
            else
            {
                InternalErrors.Add(Errors.Create("The name of the destination protocol is missing."));
                InternalErrors.Add(Errors.Create("Both destination queue and exchange cannot be present."));
            }
                
            InternalErrors.AddIfTrue(_uri, string.IsNullOrWhiteSpace, Errors.Create("The connection URI is missing."));
            
            return InternalErrors;
        }


        class ShovelSourceConfiguratorImpl :
            ShovelSourceConfigurator
        {
            public string ExchangeName { get; private set; }
            public string ExchangeRoutingKey { get; private set; }
            public ulong PrefetchCount { get; private set; }
            public object DeleteAfterShovel { get; private set; }

            public ShovelSourceConfiguratorImpl()
            {
                PrefetchCount = 1000;
            }
                
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
            public ShovelProtocol ShovelProtocol { get; private set; }
            public string ExchangeName { get; private set; }
            public string ExchangeRoutingKey { get; private set; }
            public bool AddHeaders { get; private set; }
            public bool AddTimestampHeader { get; private set; }

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