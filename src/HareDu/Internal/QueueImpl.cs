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

        string url = string.IsNullOrWhiteSpace(pagination) ? "api/queues" : $"api/queues?{pagination}";

        if (errors.Count > 0)
            return new FaultedResults<QueueInfo> {DebugInfo = new() {URL = url, Errors = errors}};

        return await GetAllRequest<QueueInfo>(url, cancellationToken).ConfigureAwait(false);
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

        var impl = new QueueConfiguratorImpl(node);
        configurator?.Invoke(impl);

        var request = impl.Request.Value;

        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = impl.Validate();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new (){Reason = "The name of the queue is missing."});

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        if (errors.Count > 0)
            return new FaultedResult {DebugInfo = new() {URL = "api/queues/{vhost}/{name}", Request = request.ToJsonString(), Errors = errors}};

        return await PutRequest($"api/queues/{sanitizedVHost}/{name}", request, cancellationToken)
            .ConfigureAwait(false);
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
            return new FaultedResult {DebugInfo = new() {URL = "api/queues/{vhost}/{name}", Errors = errors}};
        
        var impl = new QueueDeletionConfiguratorImpl();
        configurator?.Invoke(impl);

        string url = string.IsNullOrWhiteSpace(impl.Query.Value)
            ? $"api/queues/{sanitizedVHost}/{name}"
            : $"api/queues/{sanitizedVHost}/{name}?{impl.Query.Value}";

        return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
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

        if (errors.Any())
            return new FaultedResult<QueueInfo>
                {DebugInfo = new() {URL = "api/queues/{vhost}/{name}/contents", Errors = errors}};

        return await DeleteRequest($"api/queues/{sanitizedVHost}/{name}/contents", cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Result> Sync(string name, string vhost, QueueSyncAction syncAction,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new (){Reason = "The name of the queue is missing."});

        if (errors.Any())
            return new FaultedResult<QueueInfo>
                {DebugInfo = new() {URL = "api/queues/{vhost}/{name}/actions", Errors = errors}};

        return await PostRequest($"api/queues/{sanitizedVHost}/{name}/actions",
            new QueueSyncRequest {Action = syncAction}, cancellationToken).ConfigureAwait(false);
    }


    class QueueDeletionConfiguratorImpl :
        QueueDeletionConfigurator
    {
        string _query;

        public Lazy<string> Query { get; }

        public QueueDeletionConfiguratorImpl()
        {
            Query = new Lazy<string>(() => _query, LazyThreadSafetyMode.PublicationOnly);
        }

        public void WhenHasNoConsumers() =>
            _query = string.IsNullOrWhiteSpace(_query)
                ? "if-unused=true"
                : _query.Contains("if-unused=true") ? _query : $"{_query}&if-unused=true";

        public void WhenEmpty() =>
            _query = string.IsNullOrWhiteSpace(_query)
                ? "if-empty=true"
                : _query.Contains("if-empty=true") ? _query : $"{_query}&if-empty=true";
    }


    class QueueConfiguratorImpl :
        QueueConfigurator
    {
        bool _durable;
        bool _autoDelete;
        IDictionary<string, ArgumentValue<object>> _arguments;

        public Lazy<QueueRequest> Request { get; }
        public List<Error> Errors { get; }

        public QueueConfiguratorImpl(string node)
        {
            Errors = new List<Error>();
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
            public IDictionary<string, ArgumentValue<object>> Arguments { get; }

            public QueueArgumentConfiguratorImpl()
            {
                Arguments = new Dictionary<string, ArgumentValue<object>>();
            }

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