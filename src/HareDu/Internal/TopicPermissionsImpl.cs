namespace HareDu.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Core.Serialization;
    using Model;

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
            cancellationToken.RequestCanceled();

            string url = "api/topic-permissions";
            
            return await GetAll<TopicPermissionsInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Create(string username, string vhost, Action<TopicPermissionsConfigurator> configurator,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new TopicPermissionsConfiguratorImpl();
            configurator(impl);

            impl.Validate();

            TopicPermissionsDefinition definition =
                new()
                {
                    Exchange = impl.ExchangeName.Value,
                    Read = impl.ReadPattern.Value,
                    Write = impl.WritePattern.Value
                };

            Debug.Assert(definition != null);

            var errors = new List<Error>();
            
            errors.AddRange(impl.Errors.Value);
            
            if (string.IsNullOrWhiteSpace(username))
                errors.Add(new(){Reason = "The username and/or password is missing."});
            
            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new (){Reason = "The name of the virtual host is missing."});

            string url = $"api/topic-permissions/{vhost.ToSanitizedName()}/{username}";
            
            if (errors.Any())
                return new FaultedResult{DebugInfo = new (){URL = url, Request = definition.ToJsonString(Deserializer.Options), Errors = errors}};

            return await Put(url, definition, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(string username, string vhost, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var errors = new List<Error>();
            
            if (string.IsNullOrWhiteSpace(username))
                errors.Add(new(){Reason = "The username and/or password is missing."});
            
            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new (){Reason = "The name of the virtual host is missing."});

            string url = $"api/topic-permissions/{vhost.ToSanitizedName()}/{username}";
            
            if (errors.Any())
                return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

            return await Delete(url, cancellationToken).ConfigureAwait(false);
        }


        class TopicPermissionsConfiguratorImpl :
            TopicPermissionsConfigurator
        {
            string _exchange;
            string _writePattern;
            string _readPattern;
            bool _onExchangeCalled;
            bool _usingWritePatternCalled;
            bool _usingReadPatternCalled;
            
            readonly List<Error> _errors;

            public Lazy<string> ExchangeName { get; }
            public Lazy<string> WritePattern { get; }
            public Lazy<string> ReadPattern { get; }
            public Lazy<List<Error>> Errors { get; }

            public TopicPermissionsConfiguratorImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                ExchangeName = new Lazy<string>(() => _exchange, LazyThreadSafetyMode.PublicationOnly);
                WritePattern = new Lazy<string>(() => _writePattern, LazyThreadSafetyMode.PublicationOnly);
                ReadPattern = new Lazy<string>(() => _readPattern, LazyThreadSafetyMode.PublicationOnly);
            }

            public void OnExchange(string name)
            {
                _onExchangeCalled = true;
                
                _exchange = name;

                if (string.IsNullOrWhiteSpace(_exchange))
                    _errors.Add(new() {Reason = "Then name of the exchange is missing."});
            }

            public void UsingWritePattern(string pattern)
            {
                _usingWritePatternCalled = true;
                
                _writePattern = pattern;
                
                if (string.IsNullOrWhiteSpace(_writePattern))
                    _errors.Add(new () {Reason = "The write pattern is missing."});
            }

            public void UsingReadPattern(string pattern)
            {
                _usingReadPatternCalled = true;
                
                _readPattern = pattern;
                
                if (string.IsNullOrWhiteSpace(_readPattern))
                    _errors.Add(new () {Reason = "The read pattern is missing."});
            }

            public void Validate()
            {
                if (!_onExchangeCalled)
                    _errors.Add(new() {Reason = "Then name of the exchange is missing."});
                
                if (!_usingWritePatternCalled)
                    _errors.Add(new () {Reason = "The write pattern is missing."});
                
                if (!_usingReadPatternCalled)
                    _errors.Add(new () {Reason = "The read pattern is missing."});
            }
        }
    }
}