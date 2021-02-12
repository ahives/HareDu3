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

        public async Task<Result> Create(Action<QueueCreateAction> action, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new QueueCreateActionImpl();
            action?.Invoke(impl);
            
            impl.Validate();
            
            QueueDefinition definition = impl.Definition.Value;

            Debug.Assert(definition != null);

            string url = $"api/queues/{impl.VirtualHost.Value.ToSanitizedName()}/{impl.QueueName.Value}";
            
            if (impl.Errors.Value.Any())
                return new FaultedResult{Errors = impl.Errors.Value, DebugInfo = new (){URL = url, Request = definition.ToJsonString()}};

            return await Put(url, definition, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(Action<QueueDeleteAction> action, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new QueueDeleteActionImpl();
            action?.Invoke(impl);

            impl.Validate();
            
            string url = $"api/queues/{impl.VirtualHost.Value.ToSanitizedName()}/{impl.QueueName.Value}";
            if (!string.IsNullOrWhiteSpace(impl.Query.Value))
                url = $"{url}?{impl.Query.Value}";
            
            if (impl.Errors.Value.Any())
                return new FaultedResult{Errors = impl.Errors.Value, DebugInfo = new (){URL = url, Request = null}};

            return await Delete(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Empty(Action<QueueEmptyAction> action, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new QueueEmptyActionImpl();
            action?.Invoke(impl);

            impl.Validate();

            string url = $"api/queues/{impl.VirtualHost.Value.ToSanitizedName()}/{impl.QueueName.Value}/contents";

            if (impl.Errors.Value.Any())
                return new FaultedResult<QueueInfo> {Errors = impl.Errors.Value, DebugInfo = new() {URL = url, Request = null}};

            return await Delete(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ResultList<PeekedMessageInfo>> Peek(Action<QueuePeekAction> action, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new QueuePeekActionImpl();
            action?.Invoke(impl);

            impl.Validate();

            QueuePeekDefinition definition = impl.Definition.Value;

            Debug.Assert(definition != null);

            string url = $"api/queues/{impl.VirtualHost.Value.ToSanitizedName()}/{impl.QueueName.Value}/get";
            
            if (impl.Errors.Value.Any())
                return new FaultedResultList<PeekedMessageInfo>{Errors = impl.Errors.Value, DebugInfo = new (){URL = url, Request = definition.ToJsonString()}};

            return await PostList<PeekedMessageInfo, QueuePeekDefinition>(url, definition, cancellationToken).ConfigureAwait(false);
        }

        
        class QueuePeekActionImpl :
            QueuePeekAction
        {
            string _vhost;
            string _queue;
            uint _take;
            string _encoding;
            ulong _truncateIfAbove;
            string _requeueMode;
            readonly List<Error> _errors;

            public Lazy<QueuePeekDefinition> Definition { get; }
            public Lazy<string> QueueName { get; }
            public Lazy<string> VirtualHost { get; }
            public Lazy<List<Error>> Errors { get; }

            public QueuePeekActionImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Definition = new Lazy<QueuePeekDefinition>(
                    () => new (){
                        Take = _take,
                        RequeueMode = _requeueMode,
                        Encoding = _encoding,
                        TruncateMessageThreshold = _truncateIfAbove
                        
                    }, LazyThreadSafetyMode.PublicationOnly);
                VirtualHost = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
                QueueName = new Lazy<string>(() => _queue, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Queue(string name) => _queue = name;

            public void Configure(Action<QueuePeekConfiguration> configuration)
            {
                var impl = new QueuePeekConfigurationImpl();
                configuration?.Invoke(impl);

                _take = impl.TakeAmount;
                _requeueMode = impl.RequeueModeText;
                _encoding = impl.MessageEncodingText;
                _truncateIfAbove = impl.TruncateMessageThresholdInBytes;
            }

            public void Targeting(Action<QueuePeekTarget> target)
            {
                var impl = new QueuePeekTargetImpl();
                target?.Invoke(impl);

                _vhost = impl.VirtualHostName;
            }

            public void Validate()
            {
                if (string.IsNullOrWhiteSpace(_queue))
                    _errors.Add(new Error{Reason = "The name of the queue is missing."});
            
                if (_take < 1)
                    _errors.Add(new Error{Reason = "Must be set a value greater than 1."});

                if (string.IsNullOrWhiteSpace(_encoding))
                    _errors.Add(new Error{Reason = "Encoding must be set to auto or base64."});
            
                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new Error{Reason = "The name of the virtual host is missing."});
            }

            
            class QueuePeekConfigurationImpl :
                QueuePeekConfiguration
            {
                public uint TakeAmount { get; private set; }
                public string RequeueModeText { get; private set; }
                public string MessageEncodingText { get; private set; }
                public ulong TruncateMessageThresholdInBytes { get; private set; }

                public void Take(uint count) => TakeAmount = count;

                public void Encoding(MessageEncoding encoding)
                {
                    switch (encoding)
                    {
                        case MessageEncoding.Auto:
                            MessageEncodingText = "auto";
                            break;
                            
                        case MessageEncoding.Base64:
                            MessageEncodingText = "base64";
                            break;
                            
                        default:
                            throw new ArgumentOutOfRangeException(nameof(encoding), encoding, null);
                    }
                }

                public void TruncateIfAbove(uint bytes) => TruncateMessageThresholdInBytes = bytes;
                
                public void AckMode(RequeueMode mode)
                {
                    switch (mode)
                    {
                        case RequeueMode.DoNotAckRequeue:
                            RequeueModeText = "ack_requeue_false";
                            break;
                
                        case RequeueMode.RejectRequeue:
                            RequeueModeText = "reject_requeue_true";
                            break;
                
                        case RequeueMode.DoNotRejectRequeue:
                            RequeueModeText = "reject_requeue_false";
                            break;

                        default:
                            RequeueModeText = "ack_requeue_true";
                            break;
                    }
                }
            }

            
            class QueuePeekTargetImpl :
                QueuePeekTarget
            {
                public string VirtualHostName { get; private set; }
                
                public void VirtualHost(string name) => VirtualHostName = name;
            }
        }


        class QueueEmptyActionImpl :
            QueueEmptyAction
        {
            string _vhost;
            string _queue;
            readonly List<Error> _errors;

            public Lazy<string> QueueName { get; }
            public Lazy<string> VirtualHost { get; }
            public Lazy<List<Error>> Errors { get; }

            public QueueEmptyActionImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                VirtualHost = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
                QueueName = new Lazy<string>(() => _queue, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Queue(string name) => _queue = name;

            public void Targeting(Action<QueueTarget> target)
            {
                var impl = new QueueTargetImpl();
                target?.Invoke(impl);

                _vhost = impl.VirtualHostName;
            }

            public void Validate()
            {
                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new(){Reason = "The name of the virtual host is missing."});

                if (string.IsNullOrWhiteSpace(_queue))
                    _errors.Add(new (){Reason = "The name of the queue is missing."});
            }

            
            class QueueTargetImpl :
                QueueTarget
            {
                public string VirtualHostName { get; private set; }

                public void VirtualHost(string name) => VirtualHostName = name;
            }
        }


        class QueueDeleteActionImpl :
            QueueDeleteAction
        {
            string _vhost;
            string _queue;
            string _query;
            readonly List<Error> _errors;

            public Lazy<string> Query { get; }
            public Lazy<string> QueueName { get; }
            public Lazy<string> VirtualHost { get; }
            public Lazy<List<Error>> Errors { get; }

            public QueueDeleteActionImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                VirtualHost = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
                QueueName = new Lazy<string>(() => _queue, LazyThreadSafetyMode.PublicationOnly);
                Query = new Lazy<string>(() => _query, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Queue(string name) => _queue = name;

            public void Targeting(Action<QueueTarget> target)
            {
                var impl = new QueueTargetImpl();
                target?.Invoke(impl);

                _vhost = impl.VirtualHostName;
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

            public void Validate()
            {
                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new Error{Reason = "The name of the virtual host is missing."});

                if (string.IsNullOrWhiteSpace(_queue))
                    _errors.Add(new Error{Reason = "The name of the queue is missing."});
            }

            
            class QueueTargetImpl :
                QueueTarget
            {
                public string VirtualHostName { get; private set; }

                public void VirtualHost(string name) => VirtualHostName = name;
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


        class QueueCreateActionImpl :
            QueueCreateAction
        {
            bool _durable;
            bool _autoDelete;
            string _node;
            IDictionary<string, ArgumentValue<object>> _arguments;
            string _vhost;
            string _queue;
            readonly List<Error> _errors;

            public Lazy<QueueDefinition> Definition { get; }
            public Lazy<string> QueueName { get; }
            public Lazy<string> VirtualHost { get; }
            public Lazy<List<Error>> Errors { get; }

            public QueueCreateActionImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Definition = new Lazy<QueueDefinition>(
                    () => new QueueDefinition
                    {
                        Durable = _durable,
                        AutoDelete = _autoDelete,
                        Node = _node,
                        Arguments = _arguments.GetArguments()
                    }, LazyThreadSafetyMode.PublicationOnly);
                VirtualHost = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
                QueueName = new Lazy<string>(() => _queue, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Queue(string name) => _queue = name;

            public void Configure(Action<QueueConfiguration> configuration)
            {
                var impl = new QueueConfigurationImpl();
                configuration?.Invoke(impl);

                _durable = impl.Durable;
                _autoDelete = impl.AutoDelete;
                _arguments = impl.Arguments;
            }

            public void Targeting(Action<QueueCreateTarget> target)
            {
                var impl = new QueueCreateTargetImpl();
                target?.Invoke(impl);

                _node = impl.NodeName;
                _vhost = impl.VirtualHostName;
            }

            public void Validate()
            {
                if (_arguments.IsNotNull())
                    _errors.AddRange(_arguments.Select(x => x.Value?.Error).Where(error => error.IsNotNull()).ToList());

                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new Error{Reason = "The name of the virtual host is missing."});

                if (string.IsNullOrWhiteSpace(_queue))
                    _errors.Add(new Error{Reason = "The name of the queue is missing."});
            }

            
            class QueueCreateTargetImpl :
                QueueCreateTarget
            {
                public string VirtualHostName { get; private set; }
                public string NodeName { get; private set; }
                
                public void Node(string node) => NodeName = node;
            
                public void VirtualHost(string name) => VirtualHostName = name;
            }


            class QueueConfigurationImpl :
                QueueConfiguration
            {
                public bool Durable { get; private set; }
                public IDictionary<string, ArgumentValue<object>> Arguments { get; private set; }
                public bool AutoDelete { get; private set; }

                public void IsDurable() => Durable = true;

                public void HasArguments(Action<QueueCreateArguments> arguments)
                {
                    var impl = new QueueCreateArgumentsImpl();
                    arguments?.Invoke(impl);

                    Arguments = impl.Arguments;
                }

                public void AutoDeleteWhenNotInUse() => AutoDelete = true;
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