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

class TopicPermissionsImpl :
    BaseBrokerImpl,
    TopicPermissions
{
    public TopicPermissionsImpl(IHttpClientFactory clientFactory)
        : base(clientFactory)
    {
    }

    public async Task<Results<TopicPermissionsInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<TopicPermissionsInfo>("api/topic-permissions", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(string username, string vhost, Action<TopicPermissionsConfigurator> configurator,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var impl = new TopicPermissionsConfiguratorImpl();
        configurator?.Invoke(impl);

        impl.Validate();

        var errors = impl.Errors;

        if (string.IsNullOrWhiteSpace(username))
            errors.Add(new(){Reason = "The username and/or password is missing."});

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        var request = impl.Request.Value;

        string url = $"api/topic-permissions/{vhost.ToSanitizedName()}/{username}";

        if (errors.Count > 0)
            return new FaultedResult{DebugInfo = new (){URL = url, Request = request.ToJsonString(), Errors = errors}};

        return await PutRequest(url, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string username, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(username))
            errors.Add(new(){Reason = "The username and/or password is missing."});

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        string url = $"api/topic-permissions/{vhost.ToSanitizedName()}/{username}";

        if (errors.Count > 0)
            return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

        return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
    }


    class TopicPermissionsConfiguratorImpl :
        TopicPermissionsConfigurator
    {
        string _writePattern;
        string _readPattern;
        bool _usingWritePatternCalled;
        bool _usingReadPatternCalled;
        string _exchange;

        public List<Error> Errors { get; }
        public Lazy<TopicPermissionsRequest> Request { get; }

        public TopicPermissionsConfiguratorImpl()
        {
            Errors = new List<Error>();
            Request = new Lazy<TopicPermissionsRequest>(
                () => new ()
                {
                    Exchange = _exchange,
                    Read = _readPattern,
                    Write = _writePattern
                }, LazyThreadSafetyMode.PublicationOnly);
        }

        public void Exchange(string exchange) => _exchange = exchange;

        public void UsingWritePattern(string pattern)
        {
            _usingWritePatternCalled = true;
                
            _writePattern = pattern;
        }

        public void UsingReadPattern(string pattern)
        {
            _usingReadPatternCalled = true;
                
            _readPattern = pattern;
        }

        public void Validate()
        {
            if (_usingWritePatternCalled)
            {
                if (string.IsNullOrWhiteSpace(_writePattern))
                    Errors.Add(new() {Reason = "The write pattern is missing."});
            }
            else
                Errors.Add(new() {Reason = "The write pattern is missing."});

            if (_usingReadPatternCalled)
            {
                if (string.IsNullOrWhiteSpace(_readPattern))
                    Errors.Add(new() {Reason = "The read pattern is missing."});
            }
            else
                Errors.Add(new() {Reason = "The read pattern is missing."});

            if (string.IsNullOrWhiteSpace(_exchange))
                Errors.Add(new(){Reason = "Then name of the exchange is missing."});
        }
    }
}