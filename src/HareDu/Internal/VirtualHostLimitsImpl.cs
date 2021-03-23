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
    using Extensions;
    using Model;
    using Serialization;

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
            
            return await GetAllRequest<VirtualHostLimitsInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Define(string vhost, Action<VirtualHostLimitsConfigurator> configurator = null,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new VirtualHostLimitsConfiguratorImpl();
            configurator?.Invoke(impl);

            impl.Validate();
            
            VirtualHostLimitsRequest request = impl.Request.Value;

            var errors = new List<Error>();
            
            errors.AddRange(impl.Errors.Value);
            
            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new(){Reason = "The name of the virtual host is missing."});

            string url = $"api/vhost-limits/vhost/{vhost.ToSanitizedName()}";

            if (errors.Any())
                return new FaultedResult{DebugInfo = new (){URL = url, Request = request.ToJsonString(Deserializer.Options), Errors = errors}};

            return await PutRequest(url, request, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(string vhost, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var errors = new List<Error>();
            
            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new(){Reason = "The name of the virtual host is missing."});

            string url = $"api/vhost-limits/vhost/{vhost.ToSanitizedName()}";

            if (errors.Any())
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

            readonly List<Error> _errors;

            public Lazy<VirtualHostLimitsRequest> Request { get; }
            public Lazy<List<Error>> Errors { get; }

            public VirtualHostLimitsConfiguratorImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
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
                
                if (_maxConnectionLimits < 1)
                    _errors.Add(new (){Reason = "Max connection limit value is missing."});
            }

            public void SetMaxQueueLimit(ulong value)
            {
                _setMaxQueueLimitCalled = true;
                
                _maxQueueLimits = value;
                
                if (_maxQueueLimits < 1)
                    _errors.Add(new (){Reason = "Max queue limit value is missing."});
            }

            public void Validate()
            {
                if (!_setMaxConnectionLimitCalled && !_setMaxQueueLimitCalled)
                    _errors.Add(new (){Reason = "There are no limits to define."});
            }
        }
    }
}