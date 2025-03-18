namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Extensions;
using Model;

class VirtualHostLimitsImpl :
    BaseBrokerObject,
    VirtualHostLimits
{
    public VirtualHostLimitsImpl(IHttpClientFactory clientFactory)
        : base(clientFactory)
    {
    }

    public async Task<Results<VirtualHostLimitsInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<VirtualHostLimitsInfo>("api/vhost-limits", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Define(string vhost, Action<VirtualHostLimitsConfigurator> configurator = null,
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

    public async Task<Result> Delete(string vhost, CancellationToken cancellationToken = default)
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
}