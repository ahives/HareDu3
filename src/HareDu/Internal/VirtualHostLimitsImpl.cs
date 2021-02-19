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

        public async Task<Result> Define(string vhost, Action<VirtualHostLimitsConfigurator> configurator = null,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new VirtualHostLimitsConfiguratorImpl();
            configurator?.Invoke(impl);

            VirtualHostLimitsDefinition definition = impl.Definition.Value;

            var errors = new List<Error>();
            
            errors.AddRange(impl.Errors.Value);
            
            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new() {Reason = "The name of the virtual host is missing."});

            string url = $"api/vhost-limits/vhost/{vhost.ToSanitizedName()}";

            if (errors.Any())
                return new FaultedResult{DebugInfo = new () {URL = url, Request = definition.ToJsonString(), Errors = errors}};

            return await Put(url, definition, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(string vhost, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var errors = new List<Error>();
            
            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new() {Reason = "The name of the virtual host is missing."});

            string url = $"api/vhost-limits/vhost/{vhost.ToSanitizedName()}";

            if (errors.Any())
                return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

            return await Delete(url, cancellationToken).ConfigureAwait(false);
        }


        class VirtualHostLimitsConfiguratorImpl :
            VirtualHostLimitsConfigurator
        {
            ulong _maxQueueLimits;
            ulong _maxConnectionLimits;
            bool _setMaxConnectionLimitCalled;
            bool _setMaxQueueLimitCalled;

            readonly List<Error> _errors;

            public Lazy<VirtualHostLimitsDefinition> Definition { get; }
            public Lazy<List<Error>> Errors { get; }

            public VirtualHostLimitsConfiguratorImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Definition = new Lazy<VirtualHostLimitsDefinition>(
                    () => new VirtualHostLimitsDefinition
                    {
                        MaxConnectionLimit = _maxConnectionLimits,
                        MaxQueueLimit = _maxQueueLimits
                    }, LazyThreadSafetyMode.PublicationOnly);
            }

            public void SetMaxConnectionLimit(ulong value)
            {
                _setMaxConnectionLimitCalled = true;
                
                _maxConnectionLimits = value;
                
                if (_maxConnectionLimits < 1)
                    _errors.Add(new () {Reason = "Max connection limit value is missing."});
            }

            public void SetMaxQueueLimit(ulong value)
            {
                _setMaxQueueLimitCalled = true;
                
                _maxQueueLimits = value;
                
                if (_maxQueueLimits < 1)
                    _errors.Add(new () {Reason = "Max queue limit value is missing."});
            }
        }
    }
}