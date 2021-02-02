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

    class PolicyImpl :
        BaseBrokerObject,
        Policy
    {
        public PolicyImpl(HttpClient client)
            : base(client)
        {
        }

        public Task<ResultList<PolicyInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/policies";
            
            return GetAll<PolicyInfo>(url, cancellationToken);
        }

        public Task<Result> Create(Action<PolicyCreateAction> action, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new PolicyCreateActionImpl();
            action(impl);

            impl.Verify();

            PolicyDefinition definition = impl.Definition.Value;

            Debug.Assert(definition != null);
            
            string url = $"api/policies/{impl.VirtualHost.Value.ToSanitizedName()}/{impl.PolicyName.Value}";
            
            if (impl.Errors.Value.Any())
                return Task.FromResult<Result>(new FaultedResult{Errors = impl.Errors.Value, DebugInfo = new (){URL = url, Request = definition.ToJsonString()}});

            return Put(url, definition, cancellationToken);
        }

        public Task<Result> Delete(Action<PolicyDeleteAction> action, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new PolicyDeleteActionImpl();
            action(impl);

            impl.Verify();
            
            string url = $"api/policies/{impl.VirtualHost.Value.ToSanitizedName()}/{impl.PolicyName.Value}";

            if (impl.Errors.Value.Any())
                return Task.FromResult<Result>(new FaultedResult {Errors = impl.Errors.Value, DebugInfo = new() {URL = url, Request = null}});

            return Delete(url, cancellationToken);
       }

        
        class PolicyDeleteActionImpl :
            PolicyDeleteAction
        {
            string _vhost;
            string _policy;
            readonly List<Error> _errors;
            bool _policyCalled;
            bool _targetCalled;

            public Lazy<string> PolicyName { get; }
            public Lazy<string> VirtualHost { get; }
            public Lazy<List<Error>> Errors { get; }

            public PolicyDeleteActionImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                VirtualHost = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
                PolicyName = new Lazy<string>(() => _policy, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Policy(string name)
            {
                _policyCalled = true;
                _policy = name;

                if (string.IsNullOrWhiteSpace(_policy))
                    _errors.Add(new() {Reason = "The name of the policy is missing."});
            }
            
            public void Targeting(Action<PolicyTarget> target)
            {
                _targetCalled = true;
                var impl = new PolicyTargetImpl();
                target(impl);

                _vhost = impl.VirtualHostName;

                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new() {Reason = "The name of the virtual host is missing."});
            }

            public void Verify()
            {
                if (!_policyCalled)
                    _errors.Add(new() {Reason = "The name of the policy is missing."});

                if (!_targetCalled)
                    _errors.Add(new() {Reason = "The name of the virtual host is missing."});
            }

            
            class PolicyTargetImpl :
                PolicyTarget
            {
                public string VirtualHostName { get; private set; }

                public void VirtualHost(string name) => VirtualHostName = name;
            }
        }

        
        class PolicyCreateActionImpl :
            PolicyCreateAction
        {
            string _pattern;
            IDictionary<string, ArgumentValue<object>> _arguments;
            int _priority;
            string _applyTo;
            string _policy;
            string _vhost;
            readonly List<Error> _errors;
            bool _targetCalled;
            bool _policyCalled;

            public Lazy<PolicyDefinition> Definition { get; }
            public Lazy<string> VirtualHost { get; }
            public Lazy<string> PolicyName { get; }
            public Lazy<List<Error>> Errors { get; }

            public PolicyCreateActionImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Definition = new Lazy<PolicyDefinition>(
                    () => new PolicyDefinition
                    {
                        Pattern = _pattern,
                        Arguments = _arguments.GetStringArguments(),
                        Priority = _priority,
                        ApplyTo = _applyTo
                    }, LazyThreadSafetyMode.PublicationOnly);
                VirtualHost = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
                PolicyName = new Lazy<string>(() => _policy, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Policy(string name)
            {
                _policyCalled = true;
                _policy = name;

                if (string.IsNullOrWhiteSpace(_policy))
                    _errors.Add(new() {Reason = "The name of the policy is missing."});
            }

            public void Configure(Action<PolicyConfiguration> configuration)
            {
                var impl = new PolicyConfigurationImpl();
                configuration(impl);

                _applyTo = impl.WhereToApply;
                _pattern = impl.Pattern;
                _priority = impl.Priority;
                _arguments = impl.Arguments;

                foreach (var argument in _arguments?.Where(x => x.Value.IsNull()).Select(x => x.Key))
                    _errors.Add(new() {Reason = $"Argument '{argument}' has been set without a corresponding value."});

                if (!_arguments.TryGetValue("ha-mode", out var haMode))
                    return;
                
                string mode = haMode.Value.ToString().Trim();
                if ((mode.ConvertTo() == HighAvailabilityModes.Exactly ||
                    mode.ConvertTo() == HighAvailabilityModes.Nodes) && !_arguments.ContainsKey("ha-params"))
                    _errors.Add(new() {Reason = $"Argument 'ha-mode' has been set to {mode}, which means that argument 'ha-params' has to also be set"});
            }

            public void Targeting(Action<PolicyTarget> target)
            {
                _targetCalled = true;
                var impl = new PolicyTargetImpl();
                target(impl);

                _vhost = impl.VirtualHostName;

                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new() {Reason = "The name of the virtual host is missing."});
            }

            public void Verify()
            {
                if (!_policyCalled)
                    _errors.Add(new() {Reason = "The name of the policy is missing."});
                
                if (!_targetCalled)
                    _errors.Add(new (){Reason = "The name of the virtual host is missing."});
            }

            
            class PolicyTargetImpl :
                PolicyTarget
            {
                public string VirtualHostName { get; private set; }

                public void VirtualHost(string name) => VirtualHostName = name;
            }


            class PolicyConfigurationImpl :
                PolicyConfiguration
            {
                public int Priority { get; private set; }
                public string Pattern { get; private set; }
                public string WhereToApply { get; private set; }
                public IDictionary<string, ArgumentValue<object>> Arguments { get; private set; }

                public void UsingPattern(string pattern) => Pattern = pattern;

                public void HasArguments(Action<PolicyDefinitionArguments> arguments)
                {
                    var impl = new PolicyDefinitionArgumentsImpl();
                    arguments(impl);

                    Arguments = impl.Arguments;
                }

                public void HasPriority(int priority) => Priority = priority;

                public void ApplyTo(string applyTo) => WhereToApply = applyTo;
            }
            
            
            class PolicyDefinitionArgumentsImpl :
                PolicyDefinitionArguments
            {
                public IDictionary<string, ArgumentValue<object>> Arguments { get; }

                public PolicyDefinitionArgumentsImpl()
                {
                    Arguments = new Dictionary<string, ArgumentValue<object>>();
                }

                public void Set<T>(string arg, T value)
                {
                    SetArgWithConflictingCheck(arg, "federation-upstream", "federation-upstream-set", value);
                    SetArgWithConflictingCheck(arg, "ha-mode", value);
                    SetArgWithConflictingCheck(arg, "ha-sync-mode", value);
                    SetArgWithConflictingCheck(arg, "ha-params", value);
                    SetArgWithConflictingCheck(arg, "expires", value);
                    SetArgWithConflictingCheck(arg, "message-ttl", value);
                    SetArgWithConflictingCheck(arg, "max-length-bytes", value);
                    SetArgWithConflictingCheck(arg, "max-length", value);
                    SetArgWithConflictingCheck(arg, "dead-letter-exchange", value);
                    SetArgWithConflictingCheck(arg, "dead-letter-routing-key", value);
                    SetArgWithConflictingCheck(arg, "queue-mode", value);
                    SetArgWithConflictingCheck(arg, "alternate-exchange", value);
                }

                public void SetExpiry(long milliseconds)
                {
                    SetArg("expires", milliseconds);
                }

                public void SetFederationUpstreamSet(string value)
                {
                    SetArgWithConflictingCheck("federation-upstream-set", "federation-upstream", value.Trim());
                }

                public void SetFederationUpstream(string value)
                {
                    SetArgWithConflictingCheck("federation-upstream", "federation-upstream-set", value.Trim());
                }

                public void SetHighAvailabilityMode(HighAvailabilityModes mode)
                {
                    SetArg("ha-mode", mode.ConvertTo());
                }

                public void SetHighAvailabilityParams(int value)
                {
                    SetArg("ha-params", value);
                }

                public void SetHighAvailabilitySyncMode(HighAvailabilitySyncModes mode)
                {
                    SetArg("ha-sync-mode", mode.ConvertTo());
                }

                public void SetMessageTimeToLive(long milliseconds)
                {
                    SetArg("message-ttl", milliseconds);
                }

                public void SetMessageMaxSizeInBytes(long value)
                {
                    SetArg("max-length-bytes", value.ToString());
                }

                public void SetMessageMaxSize(long value)
                {
                    SetArg("max-length", value);
                }

                public void SetDeadLetterExchange(string value)
                {
                    SetArg("dead-letter-exchange", value.Trim());
                }

                public void SetDeadLetterRoutingKey(string value)
                {
                    SetArg("dead-letter-routing-key", value.Trim());
                }

                public void SetQueueMode()
                {
                    SetArg("queue-mode", "lazy");
                }

                public void SetAlternateExchange(string value)
                {
                    SetArg("alternate-exchange", value.Trim());
                }

                void SetArg(string arg, object value)
                {
                    Arguments.Add(arg.Trim(),
                        Arguments.ContainsKey(arg)
                            ? new ArgumentValue<object>(value, $"Argument '{arg}' has already been set")
                            : new ArgumentValue<object>(value));
                }

                void SetArgWithConflictingCheck(string arg, string targetArg, object value)
                {
                    Arguments.Add(arg.Trim(),
                        Arguments.ContainsKey(arg) || arg == targetArg && Arguments.ContainsKey(targetArg)
                            ? new ArgumentValue<object>(value, $"Argument '{arg}' has already been set or would conflict with argument '{targetArg}'")
                            : new ArgumentValue<object>(value));
                }

                void SetArgWithConflictingCheck(string arg, string targetArg, string conflictingArg, object value)
                {
                    Arguments.Add(arg.Trim(),
                        Arguments.ContainsKey(arg)
                        || arg == conflictingArg && Arguments.ContainsKey(targetArg)
                        || arg == targetArg && Arguments.ContainsKey(conflictingArg)
                            ? new ArgumentValue<object>(value, $"Argument '{conflictingArg}' has already been set or would conflict with argument '{arg}'")
                            : new ArgumentValue<object>(value));
                }
            }
        }
    }
}