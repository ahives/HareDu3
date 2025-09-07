namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Core.Serialization;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;

class QueueImpl :
    BaseHareDuImpl,
    Queue
{
    public QueueImpl(HttpClient client, [FromKeyedServices("broker")] IHareDuDeserializer deserializer)
        : base(client, deserializer)
    {
    }

    public async Task<Results<QueueInfo>> GetAll(Action<PaginationConfigurator> pagination = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string @params = null;
        var errors = new List<Error>();

        if (pagination is not null)
        {
            var impl = new PaginationConfiguratorImpl();
            pagination(impl);

            @params = impl.BuildPaginationParams();
            errors = impl.Validate();

            errors.AddIfTrue(@params, string.IsNullOrWhiteSpace, Errors.Create("Pagination parameters are in valid."));
        }

        return errors.HaveBeenFound()
            ? Responses.Panic<QueueInfo>(Debug.Info("api/queues", errors))
            : await GetAllRequest<QueueInfo>(string.IsNullOrWhiteSpace(@params) ? "api/queues" : $"api/queues?{@params}", RequestType.Queue, cancellationToken)
                .ConfigureAwait(false);
    }

    public async Task<Results<QueueDetailInfo>> GetDetails(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<QueueDetailInfo>("api/queues/detailed", RequestType.Queue, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(
        string name,
        string vhost,
        string node,
        Action<QueueConfigurator> configurator = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator is null)
            return Response.Panic(Debug.Info("api/queues/{vhost}/{name}", Errors.Create(e => { e.Add("No queue was defined."); })));

        var impl = new QueueConfiguratorImpl(node);
        configurator(impl);

        var request = impl.Request.Value;
        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = impl.Validate();

        errors.AddIfTrue(name, string.IsNullOrWhiteSpace, Errors.Create("The name of the queue is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("api/queues/{vhost}/{name}", errors, request: Deserializer.ToJsonString(request)))
            : await PutRequest($"api/queues/{sanitizedVHost}/{name}", request, RequestType.Queue, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(
        string name,
        string vhost,
        Action<QueueDeletionConfigurator> configurator = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = new List<Error>();

        errors.AddIfTrue(name, string.IsNullOrWhiteSpace, Errors.Create("The name of the queue is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        if (errors.HaveBeenFound())
            return Response.Panic(Debug.Info("api/queues/{vhost}/{name}", errors));

        var impl = new QueueDeletionConfiguratorImpl();
        configurator?.Invoke(impl);

        string queryParams = impl.BuildQueryParams();

        string url = string.IsNullOrWhiteSpace(queryParams)
            ? $"api/queues/{sanitizedVHost}/{name}"
            : $"api/queues/{sanitizedVHost}/{name}?{queryParams}";

        return await DeleteRequest(url, RequestType.Queue, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Empty(string name, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = new List<Error>();

        errors.AddIfTrue(name, string.IsNullOrWhiteSpace, Errors.Create("The name of the queue is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.Count > 0
            ? Response.Panic<QueueInfo>(Debug.Info("api/queues/{vhost}/{name}/contents", errors))
            : await DeleteRequest($"api/queues/{sanitizedVHost}/{name}/contents", RequestType.Queue, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Sync(string name, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = new List<Error>();

        errors.AddIfTrue(name, string.IsNullOrWhiteSpace, Errors.Create("The name of the queue is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic<QueueInfo>(Debug.Info("api/queues/{vhost}/{name}/actions", errors))
            : await PostRequest($"api/queues/{sanitizedVHost}/{name}/actions",
                    new QueueSyncRequest {Action = QueueSyncAction.Sync}, RequestType.Queue, cancellationToken)
                .ConfigureAwait(false);
    }

    public async Task<Result> CancelSync(string name, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = new List<Error>();

        errors.AddIfTrue(name, string.IsNullOrWhiteSpace, Errors.Create("The name of the queue is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic<QueueInfo>(Debug.Info("api/queues/{vhost}/{name}/actions", errors))
            : await PostRequest($"api/queues/{sanitizedVHost}/{name}/actions",
                    new QueueSyncRequest {Action = QueueSyncAction.CancelSync}, RequestType.Queue, cancellationToken)
                .ConfigureAwait(false);
    }

    public async Task<Result<BindingInfo>> BindToQueue(
        string vhost,
        string exchange,
        Action<BindingConfigurator> configurator,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator is null)
            return Response.Panic<BindingInfo>(Debug.Info("api/bindings/{vhost}/e/{exchange}/q/{destination}",
                Errors.Create(e => {e.Add("No binding was defined.", RequestType.Queue);})));

        var impl = new BindingConfiguratorImpl();
        configurator(impl);

        var request = impl.Request.Value;
        var errors = impl.Validate();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(exchange, string.IsNullOrWhiteSpace, Errors.Create("The name of the source binding (queue/exchange) is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic<BindingInfo>(Debug.Info("api/bindings/{vhost}/e/{exchange}/q/{destination}", errors, request: Deserializer.ToJsonString(request)))
            : await PostRequest<BindingInfo, BindingRequest>(
                    $"api/bindings/{sanitizedVHost}/e/{exchange}/q/{impl.DestinationBinding}", request, RequestType.Queue, cancellationToken)
                .ConfigureAwait(false);
    }

    public async Task<Result> Unbind(string vhost, Action<UnbindingConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator is null)
            return Response.Panic(Debug.Info("api/bindings/{vhost}/e/{exchange}/q/{destination}",
                Errors.Create(e => { e.Add("No binding configuration was provided."); })));

        var impl = new UnbindingConfiguratorImpl();
        configurator(impl);

        var errors = impl.Validate();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info($"api/bindings/{sanitizedVHost}/e/{impl.SourceBinding}/q/{impl.DestinationBinding}", errors))
            : await DeleteRequest($"api/bindings/{sanitizedVHost}/e/{impl.SourceBinding}/q/{impl.DestinationBinding}", RequestType.Queue, cancellationToken)
                .ConfigureAwait(false);
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
                    Arguments = _arguments.GetArgumentsOrEmpty()
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
                    Arguments[normalizedArg] = new ArgumentValue<object>(value, Errors.Create(errorMsg));
                else
                    Arguments.Add(normalizedArg, new ArgumentValue<object>(value, Errors.Create(errorMsg)));
            }
        }
    }
}