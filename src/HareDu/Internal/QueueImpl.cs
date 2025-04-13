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

class QueueImpl :
    BaseBrokerImpl,
    Queue
{
    public QueueImpl(IHttpClientFactory clientFactory)
        : base(clientFactory)
    {
    }

    public async Task<Results<QueueInfo>> GetAll(Action<PaginationConfigurator> configurator = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string pagination = null;
        var errors = new List<Error>();

        if (configurator != null)
        {
            var impl = new PaginationConfiguratorImpl();
            configurator(impl);

            pagination = impl.BuildPaginationParams();

            if (string.IsNullOrWhiteSpace(pagination))
                errors.Add(new() {Reason = "Name of the node for which to return memory usage data is missing."});
        }

        if (errors.Count > 0)
            return Panic.Results<QueueInfo>("api/queues", errors);

        return await GetAllRequest<QueueInfo>(
                string.IsNullOrWhiteSpace(pagination) ? "api/queues" : $"api/queues?{pagination}", cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Results<QueueDetailInfo>> GetDetails(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<QueueDetailInfo>("api/queues/detailed", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(string name, string vhost, string node, Action<QueueConfigurator> configurator = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator == null)
            return Panic.Result("api/queues/{vhost}/{name}", [new() {Reason = "No queue was defined."}]);

        var impl = new QueueConfiguratorImpl(node);
        configurator(impl);

        var request = impl.Request.Value;
        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = impl.Validate();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new (){Reason = "The name of the queue is missing."});

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        if (errors.Count > 0)
            return Panic.Result("api/queues/{vhost}/{name}", errors, request.ToJsonString());

        return await PutRequest($"api/queues/{sanitizedVHost}/{name}", request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string name, string vhost, Action<QueueDeletionConfigurator> configurator = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new (){Reason = "The name of the queue is missing."});

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        if (errors.Count > 0)
            return Panic.Result("api/queues/{vhost}/{name}", errors);
        
        var impl = new QueueDeletionConfiguratorImpl();
        configurator?.Invoke(impl);
        
        string queryParams = impl.BuildQueryParams();

        string url = string.IsNullOrWhiteSpace(queryParams)
            ? $"api/queues/{sanitizedVHost}/{name}"
            : $"api/queues/{sanitizedVHost}/{name}?{queryParams}";

        return await DeleteRequest(string.IsNullOrWhiteSpace(queryParams)
            ? $"api/queues/{sanitizedVHost}/{name}"
            : $"api/queues/{sanitizedVHost}/{name}?{queryParams}", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Empty(string name, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new (){Reason = "The name of the queue is missing."});

        if (errors.Count > 0)
            return Panic.Result<QueueInfo>("api/queues/{vhost}/{name}/contents", errors);

        return await DeleteRequest($"api/queues/{sanitizedVHost}/{name}/contents", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Sync(string name, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new (){Reason = "The name of the queue is missing."});

        if (errors.Count > 0)
            return Panic.Result<QueueInfo>("api/queues/{vhost}/{name}/actions", errors);

        return await PostRequest($"api/queues/{sanitizedVHost}/{name}/actions",
            new QueueSyncRequest {Action = QueueSyncAction.Sync}, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> CancelSync(string name, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new (){Reason = "The name of the queue is missing."});

        if (errors.Count > 0)
            return Panic.Result<QueueInfo>("api/queues/{vhost}/{name}/actions", errors);

        return await PostRequest($"api/queues/{sanitizedVHost}/{name}/actions",
            new QueueSyncRequest {Action = QueueSyncAction.CancelSync}, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<BindingInfo>> BindToQueue(string vhost, string exchange, Action<BindingConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator == null)
            return Panic.Result<BindingInfo>("api/bindings/{vhost}/e/{exchange}/q/{destination}", [new() {Reason = "No binding was defined."}]);

        var impl = new BindingConfiguratorImpl();
        configurator(impl);

        var request = impl.Request.Value;
        var errors = impl.Validate();
        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        if (string.IsNullOrWhiteSpace(exchange))
            errors.Add(new(){Reason = "The name of the source binding (queue/exchange) is missing."});

        if (errors.Count > 0)
            return Panic.Result<BindingInfo>(new() {URL = "api/bindings/{vhost}/e/{exchange}/q/{destination}", Request = request.ToJsonString(), Errors = errors});

        return await PostRequest<BindingInfo, BindingRequest>($"api/bindings/{sanitizedVHost}/e/{exchange}/q/{impl.DestinationBinding}", request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Result> Unbind(string vhost, Action<UnbindingConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator == null)
            return Panic.Result("api/bindings/{vhost}/e/{exchange}/q/{destination}", [new() {Reason = "No binding configuration was provided."}]);

        var impl = new UnbindingConfiguratorImpl();
        configurator(impl);

        var errors = impl.Validate();
        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new() {Reason = "The name of the virtual host is missing."});

        if (errors.Count > 0)
            return Panic.Result(new() {URL = $"api/bindings/{sanitizedVHost}/e/{impl.SourceBinding}/q/{impl.DestinationBinding}", Errors = errors});

        return await DeleteRequest($"api/bindings/{sanitizedVHost}/e/{impl.SourceBinding}/q/{impl.DestinationBinding}", cancellationToken).ConfigureAwait(false);
    }


    class QueueDeletionConfiguratorImpl :
        QueueDeletionConfigurator
    {
        bool _hasNoConsumersCalled;
        bool _whenEmptyCalled;

        public void WhenHasNoConsumers() => _hasNoConsumersCalled = true;

        public void WhenEmpty() => _whenEmptyCalled = true;

        public string BuildQueryParams()
        {
            List<string> param = new();
            
            if (_hasNoConsumersCalled)
                param.Add("if-unused=true");

            if (_whenEmptyCalled)
                param.Add("if-empty=true");

            return string.Join('&', param);
        }
    }


    class QueueConfiguratorImpl :
        QueueConfigurator
    {
        bool _durable;
        bool _autoDelete;
        IDictionary<string, ArgumentValue<object>> _arguments;

        public Lazy<QueueRequest> Request { get; }

        public QueueConfiguratorImpl(string node)
        {
            Request = new Lazy<QueueRequest>(
                () => new ()
                {
                    Durable = _durable,
                    AutoDelete = _autoDelete,
                    Node = node,
                    Arguments = _arguments.GetArgumentsOrNull()
                }, LazyThreadSafetyMode.PublicationOnly);
        }
            
        public void IsDurable() => _durable = true;

        public void HasArguments(Action<QueueArgumentConfigurator> configurator)
        {
            var impl = new QueueArgumentConfiguratorImpl();
            configurator?.Invoke(impl);

            _arguments = impl.Arguments;
        }

        public void AutoDeleteWhenNotInUse() => _autoDelete = true;

        public List<Error> Validate() =>
            _arguments != null
                ? _arguments
                    .Select(x => x.Value?.Error)
                    .Where(error => error is not null)
                    .ToList()
                : [];


        class QueueArgumentConfiguratorImpl :
            QueueArgumentConfigurator
        {
            public IDictionary<string, ArgumentValue<object>> Arguments { get; } = new Dictionary<string, ArgumentValue<object>>();

            public void Set<T>(string arg, T value) => SetArg(arg, value);

            public void SetQueueExpiration(ulong milliseconds) =>
                SetArg("x-expires", milliseconds, milliseconds < 1 ? "x-expires cannot have a value less than 1" : null);

            public void SetPerQueuedMessageExpiration(ulong milliseconds) => SetArg("x-message-ttl", milliseconds);

            public void SetDeadLetterExchange(string exchange) => SetArg("x-dead-letter-exchange", exchange);

            public void SetDeadLetterExchangeRoutingKey(string routingKey) => SetArg("x-dead-letter-routing-key", routingKey);

            public void SetAlternateExchange(string exchange) => SetArg("alternate-exchange", exchange);

            void SetArg(string arg, object value, string errorMsg = null)
            {
                string normalizedArg = arg.Trim();
                    
                if (Arguments.ContainsKey(normalizedArg))
                    Arguments[normalizedArg] = new ArgumentValue<object>(value, errorMsg);
                else
                    Arguments.Add(normalizedArg, new ArgumentValue<object>(value, errorMsg));
            }
        }
    }
}