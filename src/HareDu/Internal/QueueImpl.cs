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
    using Serialization.Converters;

    class QueueImpl :
        BaseBrokerObject,
        Queue
    {
        public QueueImpl(HttpClient client)
            : base(client)
        {
        }

        public async Task<ResultList<QueueInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/queues";
            
            return await GetAllRequest<QueueInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Create(string queue, string vhost, string node, Action<QueueConfigurator> configurator = null,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new QueueConfiguratorImpl(node);
            configurator?.Invoke(impl);
            
            impl.Validate();
            
            QueueRequest request = impl.Definition.Value;

            Debug.Assert(request != null);

            var errors = new List<Error>();
            
            errors.AddRange(impl.Errors.Value);
            
            if (string.IsNullOrWhiteSpace(queue))
                errors.Add(new (){Reason = "The name of the queue is missing."});

            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new (){Reason = "The name of the virtual host is missing."});

            string url = $"api/queues/{vhost.ToSanitizedName()}/{queue}";
            
            if (errors.Any())
                return new FaultedResult{DebugInfo = new (){URL = url, Request = request.ToJsonString(Deserializer.Options), Errors = errors}};

            return await PutRequest(url, request, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(string queue, string vhost, Action<QueueDeletionConfigurator> configurator = null,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new QueueDeletionConfiguratorImpl();
            configurator?.Invoke(impl);

            var errors = new List<Error>();
            
            if (string.IsNullOrWhiteSpace(queue))
                errors.Add(new (){Reason = "The name of the queue is missing."});

            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new (){Reason = "The name of the virtual host is missing."});

            string url = string.IsNullOrWhiteSpace(impl.Query.Value)
                ? $"api/queues/{vhost.ToSanitizedName()}/{queue}"
                : $"api/queues/{vhost.ToSanitizedName()}/{queue}?{impl.Query.Value}";
            
            if (errors.Any())
                return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

            return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Empty(string queue, string vhost, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var errors = new List<Error>();
            
            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new(){Reason = "The name of the virtual host is missing."});

            if (string.IsNullOrWhiteSpace(queue))
                errors.Add(new (){Reason = "The name of the queue is missing."});

            string url = $"api/queues/{vhost.ToSanitizedName()}/{queue}/contents";

            if (errors.Any())
                return new FaultedResult<QueueInfo> {DebugInfo = new (){URL = url, Errors = errors}};

            return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Sync(string queue, string vhost, QueueSyncAction syncAction,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            QueueSyncRequest request = new()
            {
                Action = syncAction
            };

            Debug.Assert(request != null);
            
            var errors = new List<Error>();
            
            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new(){Reason = "The name of the virtual host is missing."});

            if (string.IsNullOrWhiteSpace(queue))
                errors.Add(new (){Reason = "The name of the queue is missing."});

            string url = $"api/queues/{vhost.ToSanitizedName()}/{queue}/actions";

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

            public void WhenHasNoConsumers()
            {
                _query = string.IsNullOrWhiteSpace(_query)
                    ? "if-unused=true"
                    : _query.Contains("if-unused=true") ? _query : $"{_query}&if-unused=true";
            }

            public void WhenEmpty()
            {
                _query = string.IsNullOrWhiteSpace(_query)
                    ? "if-empty=true"
                    : _query.Contains("if-empty=true") ? _query : $"{_query}&if-empty=true";
            }
        }


        class QueueConfiguratorImpl :
            QueueConfigurator
        {
            bool _durable;
            bool _autoDelete;
            IDictionary<string, ArgumentValue<object>> _arguments;
            
            readonly List<Error> _errors;

            public Lazy<QueueRequest> Definition { get; }
            public Lazy<List<Error>> Errors { get; }

            public QueueConfiguratorImpl(string node)
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Definition = new Lazy<QueueRequest>(
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
                if (_arguments.IsNotNull())
                    _errors.AddRange(_arguments.Select(x => x.Value?.Error).Where(error => error.IsNotNull()).ToList());
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
}