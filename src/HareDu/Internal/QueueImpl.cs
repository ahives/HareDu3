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
using Serialization;

class QueueImpl :
    BaseBrokerObject,
    Queue
{
    public QueueImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<Result<IReadOnlyList<QueueInfo>>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<QueueInfo>("api/queues", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(string name, string vhost, string node, Action<QueueConfigurator> configurator = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var impl = new QueueConfiguratorImpl(node);
        configurator?.Invoke(impl);

        impl.Validate();

        var request = impl.Request.Value;

        var errors = impl.Errors;

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new (){Reason = "The name of the queue is missing."});

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        string url = $"api/queues/{vhost.ToSanitizedName()}/{name}";

        if (errors.Any())
            return new FaultedResult{DebugInfo = new (){URL = url, Request = request.ToJsonString(), Errors = errors}};

        return await PutRequest(url, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string name, string vhost, Action<QueueDeletionConfigurator> configurator = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var impl = new QueueDeletionConfiguratorImpl();
        configurator?.Invoke(impl);

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new (){Reason = "The name of the queue is missing."});

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        string url = string.IsNullOrWhiteSpace(impl.Query.Value)
            ? $"api/queues/{vhost.ToSanitizedName()}/{name}"
            : $"api/queues/{vhost.ToSanitizedName()}/{name}?{impl.Query.Value}";

        if (errors.Any())
            return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

        return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Empty(string name, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new (){Reason = "The name of the queue is missing."});

        string url = $"api/queues/{vhost.ToSanitizedName()}/{name}/contents";

        if (errors.Any())
            return new FaultedResult<QueueInfo> {DebugInfo = new (){URL = url, Errors = errors}};

        return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Sync(string name, string vhost, QueueSyncAction syncAction,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var request = new QueueSyncRequest
        {
            Action = syncAction
        };

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new (){Reason = "The name of the queue is missing."});

        string url = $"api/queues/{vhost.ToSanitizedName()}/{name}/actions";

        if (errors.Any())
            return new FaultedResult<QueueInfo> {DebugInfo = new (){URL = url, Errors = errors}};

        return await PostRequest(url, request, cancellationToken).ConfigureAwait(false);
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

        public void Validate()
        {
            if (_arguments is not null)
                Errors.AddRange(_arguments
                    .Select(x => x.Value?.Error)
                    .Where(error => error is not null).ToList());
        }

            
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