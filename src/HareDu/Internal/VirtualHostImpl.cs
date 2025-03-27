namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Extensions;
using Model;

class VirtualHostImpl :
    BaseBrokerImpl,
    VirtualHost
{
    public VirtualHostImpl(IHttpClientFactory clientFactory)
        : base(clientFactory)
    {
    }

    public async Task<Results<VirtualHostInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<VirtualHostInfo>("api/vhosts", cancellationToken).ConfigureAwait(false);
    }

    public Task<Result<VirtualHostInfo>> Get(string vhost, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Create(string vhost, Action<VirtualHostConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new() {Reason = "The name of the virtual host is missing."});

        VirtualHostRequest request = null;

        if (configurator != null)
        {
            var impl = new VirtualHostConfiguratorImpl();
            configurator?.Invoke(impl);

            request = impl.Request.Value;
        }

        string url = $"api/vhosts/{vhost.ToSanitizedName()}";
        
        if (errors.Count > 0)
            return new FaultedResult {DebugInfo = new() {URL = url, Request = request.ToJsonString(), Errors = errors}};

        return await PutRequest(url, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        string virtualHost = vhost.ToSanitizedName();

        if (virtualHost == "2%f")
            errors.Add(new(){Reason = "Cannot delete the default virtual host."});
        else
            if (string.IsNullOrWhiteSpace(virtualHost))
                errors.Add(new(){Reason = "The name of the virtual host is missing."});

        string url = $"api/vhosts/{virtualHost}";

        if (errors.Count > 0)
            return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

        return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Startup(string vhost, string node, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        if (string.IsNullOrWhiteSpace(node))
            errors.Add(new(){Reason = "RabbitMQ node is missing."});

        string url = $"/api/vhosts/{vhost.ToSanitizedName()}/start/{node}";

        if (errors.Count > 0)
            return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

        return await PostEmptyRequest(url, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<VirtualHostLimitsInfo>> GetAllLimits(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<VirtualHostLimitsInfo>("api/vhost-limits", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> DefineLimits(string vhost, Action<VirtualHostLimitsConfigurator> configurator = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var impl = new VirtualHostLimitsConfiguratorImpl();
        configurator?.Invoke(impl);

        impl.Validate();

        var request = impl.Request.Value;

        var errors = impl.Errors;

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        string url = $"api/vhost-limits/vhost/{vhost.ToSanitizedName()}";

        if (errors.Count > 0)
            return new FaultedResult{DebugInfo = new (){URL = url, Request = request.ToJsonString(), Errors = errors}};

        return await PutRequest(url, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> DeleteLimits(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        string url = $"api/vhost-limits/vhost/{vhost.ToSanitizedName()}";

        if (errors.Count > 0)
            return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

        return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
    }


    class VirtualHostLimitsConfiguratorImpl :
        VirtualHostLimitsConfigurator
    {
        ulong _maxQueueLimits;
        ulong _maxConnectionLimits;
        bool _setMaxConnectionLimitCalled;
        bool _setMaxQueueLimitCalled;

        public Lazy<VirtualHostLimitsRequest> Request { get; }
        public List<Error> Errors { get; }

        public VirtualHostLimitsConfiguratorImpl()
        {
            Errors = new List<Error>();
            Request = new Lazy<VirtualHostLimitsRequest>(
                () => new ()
                {
                    MaxConnectionLimit = _maxConnectionLimits,
                    MaxQueueLimit = _maxQueueLimits
                }, LazyThreadSafetyMode.PublicationOnly);
        }

        public void SetMaxConnectionLimit(ulong value)
        {
            _setMaxConnectionLimitCalled = true;
                
            _maxConnectionLimits = value;
        }

        public void SetMaxQueueLimit(ulong value)
        {
            _setMaxQueueLimitCalled = true;
                
            _maxQueueLimits = value;
        }

        public void Validate()
        {
            if (!_setMaxConnectionLimitCalled && !_setMaxQueueLimitCalled)
                Errors.Add(new (){Reason = "There are no limits to define."});

            if (_maxQueueLimits < 1)
                Errors.Add(new (){Reason = "Max queue limit value is missing."});

            if (_maxConnectionLimits < 1)
                Errors.Add(new (){Reason = "Max connection limit value is missing."});
        }
    }


    class VirtualHostConfiguratorImpl :
        VirtualHostConfigurator
    {
        bool _tracing;
        string _description;
        string _tags;

        public Lazy<VirtualHostRequest> Request { get; }

        public VirtualHostConfiguratorImpl()
        {
            Request = new Lazy<VirtualHostRequest>(
                () => new ()
                {
                    Tracing = _tracing,
                    Description = _description,
                    Tags = _tags
                }, LazyThreadSafetyMode.PublicationOnly);
        }

        public void WithTracingEnabled() => _tracing = true;

        public void Description(string description) => _description = description;

        public void Tags(Action<VirtualHostTagConfigurator> configurator)
        {
            var impl = new VirtualHostTagConfiguratorImpl();
            configurator?.Invoke(impl);

            if (impl.Tags.IsNotEmpty())
            {
                StringBuilder builder = new StringBuilder();

                impl.Tags.ForEach(x => builder.AppendFormat("{0},", x));

                _tags = builder.ToString().TrimEnd(',');
            }
        }


        class VirtualHostTagConfiguratorImpl :
            VirtualHostTagConfigurator
        {
            readonly List<string> _tags = new();

            public List<string> Tags => _tags;

            public void Add(string tag)
            {
                if (_tags.Contains(tag) || string.IsNullOrWhiteSpace(tag))
                    return;

                _tags.Add(tag);
            }
        }
    }
}