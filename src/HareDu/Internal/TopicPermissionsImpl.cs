namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Extensions;
using Model;
using Serialization;

class TopicPermissionsImpl :
    BaseBrokerObject,
    TopicPermissions
{
    public TopicPermissionsImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<ResultList<TopicPermissionsInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
            
        return await GetAllRequest<TopicPermissionsInfo>("api/topic-permissions", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(string username, string exchange, string vhost, Action<TopicPermissionsConfigurator> configurator,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var impl = new TopicPermissionsConfiguratorImpl();
        configurator?.Invoke(impl);

        impl.Validate();

        var errors = new List<Error>();
            
        errors.AddRange(impl.Errors.Value);

        if (string.IsNullOrWhiteSpace(exchange))
            errors.Add(new(){Reason = "Then name of the exchange is missing."});
            
        if (string.IsNullOrWhiteSpace(username))
            errors.Add(new(){Reason = "The username and/or password is missing."});
            
        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        TopicPermissionsRequest request =
            new()
            {
                Exchange = exchange,
                Read = impl.ReadPattern.Value,
                Write = impl.WritePattern.Value
            };

        Debug.Assert(request != null);

        string url = $"api/topic-permissions/{vhost.ToSanitizedName()}/{username}";
            
        if (errors.Any())
            return new FaultedResult{DebugInfo = new (){URL = url, Request = request.ToJsonString(Deserializer.Options), Errors = errors}};

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
            
        if (errors.Any())
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
            
        readonly List<Error> _errors;

        public Lazy<string> WritePattern { get; }
        public Lazy<string> ReadPattern { get; }
        public Lazy<List<Error>> Errors { get; }

        public TopicPermissionsConfiguratorImpl()
        {
            _errors = new List<Error>();
                
            Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
            WritePattern = new Lazy<string>(() => _writePattern, LazyThreadSafetyMode.PublicationOnly);
            ReadPattern = new Lazy<string>(() => _readPattern, LazyThreadSafetyMode.PublicationOnly);
        }

        public void UsingWritePattern(string pattern)
        {
            _usingWritePatternCalled = true;
                
            _writePattern = pattern;
                
            if (string.IsNullOrWhiteSpace(_writePattern))
                _errors.Add(new (){Reason = "The write pattern is missing."});
        }

        public void UsingReadPattern(string pattern)
        {
            _usingReadPatternCalled = true;
                
            _readPattern = pattern;
                
            if (string.IsNullOrWhiteSpace(_readPattern))
                _errors.Add(new (){Reason = "The read pattern is missing."});
        }

        public void Validate()
        {
            if (!_usingWritePatternCalled)
                _errors.Add(new (){Reason = "The write pattern is missing."});
                
            if (!_usingReadPatternCalled)
                _errors.Add(new (){Reason = "The read pattern is missing."});
        }
    }
}