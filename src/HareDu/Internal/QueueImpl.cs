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
            
            return await GetAll<QueueInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Create(string queue, string vhost, string node, Action<NewQueueConfigurator> configuration = null,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new NewQueueConfiguratorImpl(node);
            configuration?.Invoke(impl);
            
            impl.Validate();
            
            QueueDefinition definition = impl.Definition.Value;

            Debug.Assert(definition != null);

            var errors = new List<Error>();
            
            errors.AddRange(impl.Errors.Value);
            
            if (string.IsNullOrWhiteSpace(queue))
                errors.Add(new () {Reason = "The name of the queue is missing."});

            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new () {Reason = "The name of the virtual host is missing."});

            string url = $"api/queues/{vhost.ToSanitizedName()}/{queue}";
            
            if (errors.Any())
                return new FaultedResult{DebugInfo = new (){URL = url, Request = definition.ToJsonString(), Errors = errors}};

            return await Put(url, definition, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(string queue, string vhost, Action<DeleteQueueConfigurator> configurator = null,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new DeleteQueueConfiguratorImpl();
            configurator?.Invoke(impl);

            var errors = new List<Error>();
            
            if (string.IsNullOrWhiteSpace(queue))
                errors.Add(new () {Reason = "The name of the queue is missing."});

            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new () {Reason = "The name of the virtual host is missing."});
            
            string url = $"api/queues/{vhost.ToSanitizedName()}/{queue}";
            if (!string.IsNullOrWhiteSpace(impl.Query.Value))
                url = $"{url}?{impl.Query.Value}";
            
            if (errors.Any())
                return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

            return await Delete(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Empty(string queue, string vhost, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();
                
            var errors = new List<Error>();
            
            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new() {Reason = "The name of the virtual host is missing."});

            if (string.IsNullOrWhiteSpace(queue))
                errors.Add(new () {Reason = "The name of the queue is missing."});

            string url = $"api/queues/{vhost.ToSanitizedName()}/{queue}/contents";

            if (errors.Any())
                return new FaultedResult<QueueInfo> {DebugInfo = new() {URL = url, Errors = errors}};

            return await Delete(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ResultList<PeekedMessageInfo>> Peek(string queue, string vhost, Action<PeekQueueConfigurator> configurator = null,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new PeekQueueConfiguratorImpl();
            configurator?.Invoke(impl);

            impl.Validate();
                
            var errors = new List<Error>();
            
            errors.AddRange(impl.Errors.Value);
            
            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new() {Reason = "The name of the virtual host is missing."});

            if (string.IsNullOrWhiteSpace(queue))
                errors.Add(new () {Reason = "The name of the queue is missing."});

            QueuePeekDefinition definition = impl.Definition.Value;

            Debug.Assert(definition != null);

            string url = $"api/queues/{vhost.ToSanitizedName()}/{queue}/get";
            
            if (errors.Any())
                return new FaultedResultList<PeekedMessageInfo>{DebugInfo = new () {URL = url, Request = definition.ToJsonString(), Errors = errors}};

            return await PostList<PeekedMessageInfo, QueuePeekDefinition>(url, definition, cancellationToken).ConfigureAwait(false);
        }

        
        class PeekQueueConfiguratorImpl :
            PeekQueueConfigurator
        {
            uint _take;
            string _encoding;
            ulong _truncateIfAbove;
            string _requeueMode;
            bool _takeCalled;
            bool _encodingCalled;
            
            readonly List<Error> _errors;

            public Lazy<QueuePeekDefinition> Definition { get; }
            public Lazy<List<Error>> Errors { get; }

            public PeekQueueConfiguratorImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Definition = new Lazy<QueuePeekDefinition>(
                    () => new()
                    {
                        Take = _take,
                        RequeueMode = _requeueMode,
                        Encoding = _encoding,
                        TruncateMessageThreshold = _truncateIfAbove
                    }, LazyThreadSafetyMode.PublicationOnly);
            }
            
            public void Take(uint count)
            {
                _takeCalled = true;
                
                _take = count;
            
                if (_take < 1)
                    _errors.Add(new () {Reason = "Must be set a value greater than 1."});
            }

            public void Encoding(MessageEncoding encoding)
            {
                _encodingCalled = true;
                
                _encoding = encoding.Convert();

                if (string.IsNullOrWhiteSpace(_encoding))
                    _errors.Add(new () {Reason = "Encoding must be set to auto or base64."});
            }

            public void TruncateIfAbove(uint bytes) => _truncateIfAbove = bytes;

            public void AckMode(RequeueMode mode) => _requeueMode = mode.Convert();

            public void Validate()
            {
                if (!_takeCalled)
                    _errors.Add(new() {Reason = "Must be set a value greater than 1."});

                if (!_encodingCalled)
                    _errors.Add(new() {Reason = "Encoding must be set to auto or base64."});
            }
        }


        class DeleteQueueConfiguratorImpl :
            DeleteQueueConfigurator
        {
            string _query;

            public Lazy<string> Query { get; }

            public DeleteQueueConfiguratorImpl()
            {
                Query = new Lazy<string>(() => _query, LazyThreadSafetyMode.PublicationOnly);
            }

            public void When(Action<QueueDeleteCondition> condition)
            {
                var impl = new QueueDeleteConditionImpl();
                condition?.Invoke(impl);
                
                string query = string.Empty;

                if (impl.DeleteIfUnused)
                    query = "if-unused=true";

                if (impl.DeleteIfEmpty)
                    query = !string.IsNullOrWhiteSpace(query) ? $"{query}&if-empty=true" : "if-empty=true";

                _query = query;
            }


            class QueueDeleteConditionImpl :
                QueueDeleteCondition
            {
                public bool DeleteIfUnused { get; private set; }
                public bool DeleteIfEmpty { get; private set; }

                public void HasNoConsumers() => DeleteIfUnused = true;

                public void IsEmpty() => DeleteIfEmpty = true;
            }
        }


        class NewQueueConfiguratorImpl :
            NewQueueConfigurator
        {
            bool _durable;
            bool _autoDelete;
            IDictionary<string, ArgumentValue<object>> _arguments;
            
            readonly List<Error> _errors;

            public Lazy<QueueDefinition> Definition { get; }
            public Lazy<List<Error>> Errors { get; }

            public NewQueueConfiguratorImpl(string node)
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Definition = new Lazy<QueueDefinition>(
                    () => new QueueDefinition
                    {
                        Durable = _durable,
                        AutoDelete = _autoDelete,
                        Node = node,
                        Arguments = _arguments.GetArguments()
                    }, LazyThreadSafetyMode.PublicationOnly);
            }
            
            public void IsDurable() => _durable = true;

            public void HasArguments(Action<QueueCreateArguments> arguments)
            {
                var impl = new QueueCreateArgumentsImpl();
                arguments?.Invoke(impl);

                _arguments = impl.Arguments;
            }

            public void AutoDeleteWhenNotInUse() => _autoDelete = true;

            public void Validate()
            {
                if (_arguments.IsNotNull())
                    _errors.AddRange(_arguments.Select(x => x.Value?.Error).Where(error => error.IsNotNull()).ToList());
            }

            
            class QueueCreateArgumentsImpl :
                QueueCreateArguments
            {
                public IDictionary<string, ArgumentValue<object>> Arguments { get; }

                public QueueCreateArgumentsImpl()
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