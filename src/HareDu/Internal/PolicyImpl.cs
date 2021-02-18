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

        public async Task<ResultList<PolicyInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/policies";
            
            return await GetAll<PolicyInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Create(string policy, string vhost, Action<NewPolicyConfigurator> configurator = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new NewPolicyConfiguratorImpl();
            configurator?.Invoke(impl);

            PolicyDefinition definition = impl.Definition.Value;

            Debug.Assert(definition != null);
            
            var errors = new List<Error>();
            
            errors.AddRange(impl.Errors.Value);
            
            if (string.IsNullOrWhiteSpace(policy))
                errors.Add(new() {Reason = "The name of the policy is missing."});

            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new () {Reason = "The name of the virtual host is missing."});

            string url = $"api/policies/{vhost.ToSanitizedName()}/{policy}";
            
            if (errors.Any())
                return new FaultedResult{Errors = errors, DebugInfo = new (){URL = url, Request = definition.ToJsonString()}};

            return await Put(url, definition, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(string policy, string vhost, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var errors = new List<Error>();
            
            if (string.IsNullOrWhiteSpace(policy))
                errors.Add(new() {Reason = "The name of the policy is missing."});

            if (string.IsNullOrWhiteSpace(vhost))
                errors.Add(new() {Reason = "The name of the virtual host is missing."});
            
            string url = $"api/policies/{vhost.ToSanitizedName()}/{policy}";

            if (errors.Any())
                return new FaultedResult {Errors = errors, DebugInfo = new() {URL = url, Request = null}};

            return await Delete(url, cancellationToken).ConfigureAwait(false);
       }

        
        class NewPolicyConfiguratorImpl :
            NewPolicyConfigurator
        {
            string _pattern;
            IDictionary<string, ArgumentValue<object>> _arguments;
            int _priority;
            string _applyTo;
            
            readonly List<Error> _errors;

            public Lazy<PolicyDefinition> Definition { get; }
            public Lazy<List<Error>> Errors { get; }

            public NewPolicyConfiguratorImpl()
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
            }
            
            public void UsingPattern(string pattern) => _pattern = pattern;

            public void HasArguments(Action<PolicyDefinitionArguments> arguments)
            {
                var impl = new PolicyDefinitionArgumentsImpl();
                arguments?.Invoke(impl);

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

            public void HasPriority(int priority) => _priority = priority;

            public void ApplyTo(PolicyAppliedTo appliedTo) => _applyTo = appliedTo.Convert();


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

                public void SetExpiry(long milliseconds) => SetArg("expires", milliseconds);

                public void SetFederationUpstreamSet(string value) =>
                    SetArgWithConflictingCheck("federation-upstream-set", "federation-upstream", value.Trim());

                public void SetFederationUpstream(string value) =>
                    SetArgWithConflictingCheck("federation-upstream", "federation-upstream-set", value.Trim());

                public void SetHighAvailabilityMode(HighAvailabilityModes mode) => SetArg("ha-mode", mode.ConvertTo());

                public void SetHighAvailabilityParams(int value) => SetArg("ha-params", value);

                public void SetHighAvailabilitySyncMode(HighAvailabilitySyncModes mode) => SetArg("ha-sync-mode", mode.ConvertTo());

                public void SetMessageTimeToLive(long milliseconds) => SetArg("message-ttl", milliseconds);

                public void SetMessageMaxSizeInBytes(long value) => SetArg("max-length-bytes", value.ToString());

                public void SetMessageMaxSize(long value) => SetArg("max-length", value);

                public void SetDeadLetterExchange(string value) => SetArg("dead-letter-exchange", value.Trim());

                public void SetDeadLetterRoutingKey(string value) => SetArg("dead-letter-routing-key", value.Trim());

                public void SetQueueMode() => SetArg("queue-mode", "lazy");

                public void SetAlternateExchange(string value) => SetArg("alternate-exchange", value.Trim());

                void SetArg(string arg, object value) =>
                    Arguments.Add(arg.Trim(),
                        Arguments.ContainsKey(arg)
                            ? new ArgumentValue<object>(value, $"Argument '{arg}' has already been set")
                            : new ArgumentValue<object>(value));

                void SetArgWithConflictingCheck(string arg, string targetArg, object value) =>
                    Arguments.Add(arg.Trim(),
                        Arguments.ContainsKey(arg) || arg == targetArg && Arguments.ContainsKey(targetArg)
                            ? new ArgumentValue<object>(value, $"Argument '{arg}' has already been set or would conflict with argument '{targetArg}'")
                            : new ArgumentValue<object>(value));

                void SetArgWithConflictingCheck(string arg, string targetArg, string conflictingArg, object value) =>
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