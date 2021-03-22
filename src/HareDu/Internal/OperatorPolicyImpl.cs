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
    using Extensions;
    using Model;
    using Serialization.Converters;

    class OperatorPolicyImpl :
        BaseBrokerObject,
        OperatorPolicy
    {
        public OperatorPolicyImpl(HttpClient client) :
            base(client)
        {
        }
        
        public async Task<ResultList<OperatorPolicyInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/operator-policies";
            
            return await GetAllRequest<OperatorPolicyInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Create(string policy, string pattern, string vhost, Action<OperatorPolicyConfigurator> configurator,
            OperatorPolicyAppliedTo appliedTo = default, int priority = default, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();
        
            var impl = new OperatorPolicyConfiguratorImpl();
            configurator?.Invoke(impl);

            impl.Validate();
            
            OperatorPolicyRequest request = new ()
            {
                Pattern = pattern,
                Priority = priority,
                Arguments = impl.Arguments.Value,
                ApplyTo = appliedTo
            };
        
            Debug.Assert(request != null);
            
            var errors = new List<Error>();
            
            errors.AddRange(impl.Errors.Value);
            
            if (string.IsNullOrWhiteSpace(policy))
                errors.Add(new(){Reason = "The name of the operator policy is missing."});
        
            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new (){Reason = "The name of the virtual host is missing."});
                
            string url = $"api/operator-policies/{vhost.ToSanitizedName()}/{policy}";
            
            if (errors.Any())
                return new FaultedResult{DebugInfo = new (){URL = url, Request = request.ToJsonString(Deserializer.Options), Errors = errors}};
        
            return await PutRequest(url, request, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(string policy, string vhost, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var errors = new List<Error>();
            
            if (string.IsNullOrWhiteSpace(policy))
                errors.Add(new(){Reason = "The name of the operator policy is missing."});

            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new(){Reason = "The name of the virtual host is missing."});
            
            string url = $"api/operator-policies/{vhost.ToSanitizedName()}/{policy}";

            if (errors.Any())
                return new FaultedResult {DebugInfo = new (){URL = url, Errors = errors}};

            return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
        }


        class OperatorPolicyConfiguratorImpl :
            OperatorPolicyConfigurator
        {
            readonly IDictionary<string, ArgumentValue<ulong>> _arguments;
            readonly List<Error> _errors;

            public Lazy<IDictionary<string, ulong>> Arguments { get; }
            public Lazy<List<Error>> Errors { get; }

            public OperatorPolicyConfiguratorImpl()
            {
                _errors = new List<Error>();
                _arguments = new Dictionary<string, ArgumentValue<ulong>>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
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
                if (_arguments.IsNull() || !_arguments.Any())
                    _errors.Add(new(){Reason = "No arguments have been set."});
            }
            
            void SetArg(string arg, ulong value) =>
                _arguments.Add(arg.Trim(),
                    _arguments.ContainsKey(arg)
                        ? new ArgumentValue<ulong>(value, $"Argument '{arg}' has already been set")
                        : new ArgumentValue<ulong>(value));
        }
    }
}