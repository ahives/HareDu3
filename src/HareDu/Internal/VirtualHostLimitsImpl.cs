namespace HareDu.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    class VirtualHostLimitsImpl :
        BaseBrokerObject,
        VirtualHostLimits
    {
        public VirtualHostLimitsImpl(HttpClient client)
            : base(client)
        {
        }

        public async Task<ResultList<VirtualHostLimitsInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/vhost-limits";
            
            return await GetAll<VirtualHostLimitsInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Define(Action<VirtualHostConfigureLimitsAction> action, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new VirtualHostConfigureLimitsActionImpl();
            action?.Invoke(impl);

            impl.Validate();

            VirtualHostLimitsDefinition definition = impl.Definition.Value;

            string url = $"api/vhost-limits/vhost/{impl.VirtualHostName.Value.ToSanitizedName()}";

            if (impl.Errors.Value.Any())
                return new FaultedResult{Errors = impl.Errors.Value, DebugInfo = new (){URL = url, Request = definition.ToJsonString()}};

            return await Put(url, definition, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(Action<VirtualHostDeleteLimitsAction> action, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new VirtualHostDeleteLimitsActionImpl();
            action?.Invoke(impl);

            impl.Validate();

            string url = $"api/vhost-limits/vhost/{impl.VirtualHostName.Value.ToSanitizedName()}";

            if (impl.Errors.Value.Any())
                return new FaultedResult{Errors = impl.Errors.Value, DebugInfo = new (){URL = url, Request = null}};

            return await Delete(url, cancellationToken).ConfigureAwait(false);
        }


        class VirtualHostConfigureLimitsActionImpl :
            VirtualHostConfigureLimitsAction
        {
            string _vhost;
            ulong _maxConnectionLimits;
            ulong _maxQueueLimits;
            readonly List<Error> _errors;

            public Lazy<string> VirtualHostName { get; }
            public Lazy<List<Error>> Errors { get; }
            public Lazy<VirtualHostLimitsDefinition> Definition { get; }

            public VirtualHostConfigureLimitsActionImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                VirtualHostName = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
                Definition = new Lazy<VirtualHostLimitsDefinition>(
                    () => new VirtualHostLimitsDefinition
                    {
                        MaxConnectionLimit = _maxConnectionLimits,
                        MaxQueueLimit = _maxQueueLimits
                    }, LazyThreadSafetyMode.PublicationOnly);
            }

            public void VirtualHost(string name) => _vhost = name;

            public void Configure(Action<VirtualHostLimitsConfiguration> configuration)
            {
                var impl = new VirtualHostLimitsConfigurationImpl();
                configuration?.Invoke(impl);

                _maxConnectionLimits = impl.MaxConnectionLimit.Value;
                _maxQueueLimits = impl.MaxQueueLimit.Value;
            }

            public void Validate()
            {
                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new () {Reason = "The name of the virtual host is missing."});
                
                if (_maxConnectionLimits < 1)
                    _errors.Add(new () {Reason = "Max connection limit value is missing."});
                
                if (_maxQueueLimits < 1)
                    _errors.Add(new () {Reason = "Max queue limit value is missing."});
            }

            
            class VirtualHostLimitsConfigurationImpl :
                VirtualHostLimitsConfiguration
            {
                ulong _maxQueueLimits;
                ulong _maxConnectionLimits;
                
                public Lazy<ulong> MaxConnectionLimit { get; }
                public Lazy<ulong> MaxQueueLimit { get; }
                
                public VirtualHostLimitsConfigurationImpl()
                {
                    MaxConnectionLimit = new Lazy<ulong>(() => _maxConnectionLimits, LazyThreadSafetyMode.PublicationOnly);
                    MaxQueueLimit = new Lazy<ulong>(() => _maxQueueLimits, LazyThreadSafetyMode.PublicationOnly);
                }

                public void SetMaxConnectionLimit(ulong value) => _maxConnectionLimits = value;

                public void SetMaxQueueLimit(ulong value) => _maxQueueLimits = value;
            }
        }


        class VirtualHostDeleteLimitsActionImpl :
            VirtualHostDeleteLimitsAction
        {
            string _vhost;
            readonly List<Error> _errors;

            public Lazy<string> VirtualHostName { get; }
            public Lazy<List<Error>> Errors { get; }

            public VirtualHostDeleteLimitsActionImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                VirtualHostName = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
            }

            public void For(string vhost) => _vhost = vhost;

            public void Validate()
            {
                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new() {Reason = "The name of the virtual host is missing."});
            }
        }
    }
}