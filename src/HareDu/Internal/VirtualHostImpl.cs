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
    using Model;

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
            
            return await GetAll<VirtualHostInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Create(Action<NewVirtualHostConfiguration> configuration, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new NewVirtualHostConfigurationImpl();
            configuration?.Invoke(impl);

            impl.Validate();

            VirtualHostDefinition definition = impl.Definition.Value;

            string url = $"api/vhosts/{impl.VirtualHostName.Value.ToSanitizedName()}";

            if (impl.Errors.Value.Any())
                return new FaultedResult{Errors = impl.Errors.Value, DebugInfo = new (){URL = url, Request = definition.ToJsonString()}};

            return await Put(url, definition, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(Action<DeleteVirtualHostConfiguration> configuration, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new DeleteVirtualHostConfigurationImpl();
            configuration?.Invoke(impl);

            impl.Validate();

            string vhost = impl.VirtualHostName.Value.ToSanitizedName();

            string url = $"api/vhosts/{vhost}";

            if (impl.Errors.Value.Any())
                return new FaultedResult{Errors = impl.Errors.Value, DebugInfo = new (){URL = url, Request = null}};

            if (vhost == "2%f")
                return new FaultedResult{Errors = new List<Error> {new () {Reason = "Cannot delete the default virtual host."}}, DebugInfo = new (){URL = url, Request = null}};

            return await Delete(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Startup(Action<StartupVirtualHostConfiguration> configuration, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new StartupVirtualHostConfigurationImpl();
            configuration?.Invoke(impl);

            impl.Validate();

            string url = $"/api/vhosts/{impl.VirtualHostName.Value.ToSanitizedName()}/start/{impl.Node.Value}";

            var errors = new List<Error>();
            errors.AddRange(impl.Errors.Value);
            
            if (errors.Any())
                return new FaultedResult{Errors = errors, DebugInfo = new (){URL = url, Request = null}};

            return await PostEmpty(url, cancellationToken).ConfigureAwait(false);
        }

        
        class StartupVirtualHostConfigurationImpl :
            StartupVirtualHostConfiguration
        {
            string _node;
            string _vhost;
            bool _virtualHostCalled;
            bool _targetingCalled;
            
            readonly List<Error> _errors;

            public Lazy<List<Error>> Errors { get; }
            public Lazy<string> Node { get; }
            public Lazy<string> VirtualHostName { get; }

            public StartupVirtualHostConfigurationImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Node = new Lazy<string>(() => _node, LazyThreadSafetyMode.PublicationOnly);
                VirtualHostName = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
            }

            public void VirtualHost(string name)
            {
                _virtualHostCalled = true;

                _vhost = name;

                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new () {Reason = "The name of the virtual host is missing."});
            }

            public void Targeting(Action<StartupVirtualHostTarget> target)
            {
                _targetingCalled = true;
                
                var impl = new StartupVirtualHostTargetImpl();
                target?.Invoke(impl);

                _node = impl.NodeName;

                if (string.IsNullOrWhiteSpace(_node))
                    _errors.Add(new() {Reason = "RabbitMQ node is missing."});
            }

            public void Validate()
            {
                if (!_targetingCalled)
                    _errors.Add(new() {Reason = "RabbitMQ node is missing."});

                if (!_virtualHostCalled)
                    _errors.Add(new () {Reason = "The name of the virtual host is missing."});
            }

            
            class StartupVirtualHostTargetImpl :
                StartupVirtualHostTarget
            {
                public string NodeName { get; private set; }
                
                public void Node(string name) => NodeName = name;
            }
        }


        class DeleteVirtualHostConfigurationImpl :
            DeleteVirtualHostConfiguration
        {
            string _vhost;
            bool _virtualHostCalled;
            
            readonly List<Error> _errors;

            public Lazy<string> VirtualHostName { get; }
            public Lazy<List<Error>> Errors { get; }

            public DeleteVirtualHostConfigurationImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                VirtualHostName = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
            }

            public void VirtualHost(string name)
            {
                _virtualHostCalled = true;

                _vhost = name;
                
                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new() {Reason = "The name of the virtual host is missing."});
            }

            public void Validate()
            {
                if (!_virtualHostCalled)
                    _errors.Add(new () {Reason = "The name of the virtual host is missing."});
            }
        }

        
        class NewVirtualHostConfigurationImpl :
            NewVirtualHostConfiguration
        {
            bool _tracing;
            string _vhost;
            string _description;
            string _tags;
            bool _virtualHostCalled;
            
            readonly List<Error> _errors;

            public Lazy<VirtualHostDefinition> Definition { get; }
            public Lazy<string> VirtualHostName { get; }
            public Lazy<List<Error>> Errors { get; }
            
            public NewVirtualHostConfigurationImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Definition = new Lazy<VirtualHostDefinition>(
                    () => new VirtualHostDefinition
                    {
                        Tracing = _tracing,
                        Description = _description,
                        Tags = _tags
                    }, LazyThreadSafetyMode.PublicationOnly);
                VirtualHostName = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
            }

            public void VirtualHost(string name)
            {
                _virtualHostCalled = true;

                _vhost = name;
                
                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new() {Reason = "The name of the virtual host is missing."});
            }

            public void Configure(Action<VirtualHostConfigurator> configurator)
            {
                var impl = new VirtualHostConfiguratorImpl();
                configurator?.Invoke(impl);

                _tracing = impl.Tracing;
                _description = impl.VirtualHostDescription;
                _tags = impl.VirtualHostTags;
            }

            public void Validate()
            {
                if (!_virtualHostCalled)
                    _errors.Add(new() {Reason = "The name of the virtual host is missing."});
            }


            class VirtualHostConfiguratorImpl :
                VirtualHostConfigurator
            {
                public bool Tracing { get; private set; }
                public string VirtualHostDescription { get; private set; }
                public string VirtualHostTags { get; private set; }

                public void WithTracingEnabled() => Tracing = true;
                
                public void Description(string description) => VirtualHostDescription = description;
                
                public void Tags(Action<VirtualHostTagConfigurator> configurator)
                {
                    var impl = new VirtualHostTagConfiguratorImpl();
                    configurator?.Invoke(impl);

                    StringBuilder builder = new StringBuilder();
                    
                    impl.Tags.ForEach(x => builder.AppendFormat("{0},", x));

                    VirtualHostTags = builder.ToString().TrimEnd(',');
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
}