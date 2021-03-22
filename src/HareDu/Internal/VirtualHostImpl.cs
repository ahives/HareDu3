namespace HareDu.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Extensions;
    using Model;
    using Serialization.Converters;

    class VirtualHostImpl :
        BaseBrokerObject,
        VirtualHost
    {
        public VirtualHostImpl(HttpClient client)
            : base(client)
        {
        }

        public async Task<ResultList<VirtualHostInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/vhosts";
            
            return await GetAllRequest<VirtualHostInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Create(string vhost, Action<VirtualHostConfigurator> configurator, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new NewVirtualHostConfiguratorImpl();
            configurator?.Invoke(impl);

            VirtualHostRequest request = impl.Request.Value;

            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new (){Reason = "The name of the virtual host is missing."});

            string url = $"api/vhosts/{vhost.ToSanitizedName()}";

            if (errors.Any())
                return new FaultedResult{DebugInfo = new (){URL = url, Request = request.ToJsonString(Deserializer.Options), Errors = errors}};

            return await PutRequest(url, request, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(string vhost, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var errors = new List<Error>();

            string virtualHost = vhost.ToSanitizedName();

            if (virtualHost == "2%f")
                errors.Add(new(){Reason = "Cannot delete the default virtual host."});
            else
            {
                if (string.IsNullOrWhiteSpace(virtualHost))
                    errors.Add(new(){Reason = "The name of the virtual host is missing."});
            }

            string url = $"api/vhosts/{virtualHost}";

            if (errors.Any())
                return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

            return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Startup(string vhost, string node, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new (){Reason = "The name of the virtual host is missing."});

            if (string.IsNullOrWhiteSpace(node))
                errors.Add(new(){Reason = "RabbitMQ node is missing."});

            string url = $"/api/vhosts/{vhost.ToSanitizedName()}/start/{node}";
            
            if (errors.Any())
                return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

            return await PostEmptyRequest(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result<ServerHealthInfo>> GetHealth(string vhost,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = $"api/aliveness-test/{vhost.ToSanitizedName()}";;

            return await GetRequest<ServerHealthInfo>(url, cancellationToken).ConfigureAwait(false);
        }


        class NewVirtualHostConfiguratorImpl :
            VirtualHostConfigurator
        {
            bool _tracing;
            string _description;
            string _tags;

            public Lazy<VirtualHostRequest> Request { get; }

            public NewVirtualHostConfiguratorImpl()
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
}