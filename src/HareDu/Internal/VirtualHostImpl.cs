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

    public async Task<Result> Create(string vhost, Action<VirtualHostConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var impl = new VirtualHostConfiguratorImpl();
        configurator?.Invoke(impl);

        var request = impl.Request.Value;

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        string url = $"api/vhosts/{vhost.ToSanitizedName()}";

        if (errors.Count > 0)
            return new FaultedResult{DebugInfo = new (){URL = url, Request = request.ToJsonString(), Errors = errors}};

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

            StringBuilder builder = new StringBuilder();

            impl.Tags.ForEach(x => builder.AppendFormat("{0},", x));

            _tags = builder.ToString().TrimEnd(',');
        }


        class VirtualHostTagConfiguratorImpl :
            VirtualHostTagConfigurator
        {
            readonly List<string> _tags;

            public List<string> Tags => _tags;

            public VirtualHostTagConfiguratorImpl()
            {
                _tags = new List<string>();
            }

            public void Add(string tag)
            {
                if (_tags.Contains(tag))
                    return;

                _tags.Add(tag);
            }
        }
    }
}