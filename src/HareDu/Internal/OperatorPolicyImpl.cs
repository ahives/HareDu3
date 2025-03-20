namespace HareDu.Internal;

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

class OperatorPolicyImpl :
    BaseBrokerImpl,
    OperatorPolicy
{
    public OperatorPolicyImpl(IHttpClientFactory clientFactory) :
        base(clientFactory)
    {
    }
        
    public async Task<Results<OperatorPolicyInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<OperatorPolicyInfo>("api/operator-policies", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(string name, string vhost, Action<OperatorPolicyConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var impl = new OperatorPolicyConfiguratorImpl();
        configurator?.Invoke(impl);

        impl.Validate();

        var request = impl.Request.Value;

        var errors = impl.Errors;

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new(){Reason = "The name of the operator policy is missing."});

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        string url = $"api/operator-policies/{vhost.ToSanitizedName()}/{name}";

        if (errors.Any())
            return new FaultedResult{DebugInfo = new (){URL = url, Request = request.ToJsonString(), Errors = errors}};

        return await PutRequest(url, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string name, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new(){Reason = "The name of the operator policy is missing."});

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new(){Reason = "The name of the virtual host is missing."});

        string url = $"api/operator-policies/{vhost.ToSanitizedName()}/{name}";

        if (errors.Any())
            return new FaultedResult {DebugInfo = new (){URL = url, Errors = errors}};

        return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
    }


    class OperatorPolicyConfiguratorImpl :
        OperatorPolicyConfigurator
    {
        IDictionary<string, ulong> _arguments;
        string _pattern;
        int _priority;
        OperatorPolicyAppliedTo _appliedTo;

        public List<Error> Errors { get; }
        public Lazy<OperatorPolicyRequest> Request { get; }

        public OperatorPolicyConfiguratorImpl()
        {
            Errors = new List<Error>();
            Request = new Lazy<OperatorPolicyRequest>(
                () => new ()
                {
                    Pattern = _pattern,
                    Priority = _priority,
                    ApplyTo = _appliedTo,
                    Arguments = _arguments
                }, LazyThreadSafetyMode.PublicationOnly);
        }

        public void Definition(Action<OperatorPolicyArgumentConfigurator> configurator)
        {
            var impl = new OperatorPolicyArgumentConfiguratorImpl();
            configurator?.Invoke(impl);

            _arguments = impl.Arguments.Value;
            
            impl.Validate();
            
            Errors.AddRange(impl.Errors);
        }

        public void Pattern(string pattern) => _pattern = pattern;

        public void Priority(int priority) => _priority = priority;

        public void ApplyTo(OperatorPolicyAppliedTo applyTo) => _appliedTo = applyTo;

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(_pattern))
                Errors.Add(new(){Reason = "The pattern is missing."});
        }


        class OperatorPolicyArgumentConfiguratorImpl :
            OperatorPolicyArgumentConfigurator
        {
            readonly IDictionary<string, ArgumentValue<ulong>> _arguments;

            public Lazy<IDictionary<string, ulong>> Arguments { get; }
            public List<Error> Errors { get; }

            public OperatorPolicyArgumentConfiguratorImpl()
            {
                _arguments = new Dictionary<string, ArgumentValue<ulong>>();

                Errors = new List<Error>();
                Arguments = new Lazy<IDictionary<string, ulong>>(() => _arguments.GetArgumentsOrNull(), LazyThreadSafetyMode.PublicationOnly);
            }

            public void SetExpiry(ulong milliseconds) => SetArg("expires", milliseconds);
                
            public void SetMaxInMemoryBytes(ulong bytes) => SetArg("max-in-memory-bytes", bytes);

            public void SetMaxInMemoryLength(ulong messages) => SetArg("max-in-memory-length", messages);

            public void SetDeliveryLimit(ulong limit) => SetArg("delivery-limit", limit);

            public void SetMessageTimeToLive(ulong milliseconds) => SetArg("message-ttl", milliseconds);

            public void SetMessageMaxSizeInBytes(ulong value) => SetArg("max-length-bytes", value);

            public void SetMessageMaxSize(ulong value) => SetArg("max-length", value);

            public void Validate()
            {
                if (_arguments is null || !_arguments.Any())
                    Errors.Add(new(){Reason = "No arguments have been set."});
            }
            
            void SetArg(string arg, ulong value) =>
                _arguments.Add(arg.Trim(),
                    _arguments.ContainsKey(arg)
                        ? new ArgumentValue<ulong>(value, $"Argument '{arg}' has already been set")
                        : new ArgumentValue<ulong>(value));
        }
    }
}