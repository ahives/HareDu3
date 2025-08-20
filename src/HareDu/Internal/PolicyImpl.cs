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
using Serialization;

class PolicyImpl :
    BaseBrokerImpl,
    Policy
{
    public PolicyImpl(HttpClient client)
        : base(client, new BrokerDeserializer())
    {
    }

    public async Task<Results<PolicyInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<PolicyInfo>("api/policies", RequestType.Policy, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(string name, string vhost, Action<PolicyConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var impl = new PolicyConfiguratorImpl();
        configurator?.Invoke(impl);

        var request = impl.Request.Value;
        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = impl.Validate();

        errors.AddIfTrue(name, string.IsNullOrWhiteSpace, Errors.Create("The name of the policy is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("api/policies/{vhost}/{name}", errors, request: request.ToJsonString(Deserializer.Options)))
            : await PutRequest($"api/policies/{sanitizedVHost}/{name}", request, RequestType.Policy, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string name, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(name, string.IsNullOrWhiteSpace, Errors.Create("The name of the policy is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("api/policies/{vhost}/{name}", errors))
            : await DeleteRequest($"api/policies/{sanitizedVHost}/{name}", RequestType.Policy, cancellationToken).ConfigureAwait(false);
    }


    class PolicyConfiguratorImpl :
        PolicyConfigurator
    {
        IDictionary<string, string> _arguments;
        string _pattern;
        int _priority;
        PolicyAppliedTo _appliedTo;

        List<Error> InternalErrors { get; } = new();

        public Lazy<PolicyRequest> Request { get; }

        public PolicyConfiguratorImpl()
        {
            Request = new Lazy<PolicyRequest>(
                () => new ()
                {
                    Pattern = _pattern,
                    Priority = _priority,
                    ApplyTo = _appliedTo,
                    Arguments = _arguments
                }, LazyThreadSafetyMode.PublicationOnly);
        }

        public void Definition(Action<PolicyArgumentConfigurator> configurator)
        {
            var impl = new PolicyArgumentConfiguratorImpl();
            configurator?.Invoke(impl);

            _arguments = impl.Arguments.Value;
            
            InternalErrors.AddRange(impl.Validate());
        }

        public void Pattern(string pattern) => _pattern = pattern;

        public void Priority(int priority) => _priority = priority;

        public void ApplyTo(PolicyAppliedTo applyTo) => _appliedTo = applyTo;

        public List<Error> Validate()
        {
            InternalErrors.AddIfTrue(_pattern, string.IsNullOrWhiteSpace, Errors.Create("Pattern was not set."));
 
            return InternalErrors;
        }


        class PolicyArgumentConfiguratorImpl :
            PolicyArgumentConfigurator
        {
            readonly IDictionary<string, ArgumentValue<object>> _arguments;

            public Lazy<IDictionary<string, string>> Arguments { get; }

            List<Error> InternalErrors { get; } = new();

            public PolicyArgumentConfiguratorImpl()
            {
                _arguments = new Dictionary<string, ArgumentValue<object>>();

                Arguments = new Lazy<IDictionary<string, string>>(() => _arguments.GetStringArguments(), LazyThreadSafetyMode.PublicationOnly);
            }

            public List<Error> Validate()
            {
                foreach (string argument in _arguments?.Where(x => x.Value is null).Select(x => x.Key)!)
                    InternalErrors.Add(Errors.Create($"Argument '{argument}' has been set without a corresponding value."));

                if (!_arguments.TryGetValue("ha-mode", out var haMode))
                    return InternalErrors;

                string mode = haMode.Value.ToString().Trim();

                InternalErrors.AddIfTrue(IsHighlyAvailable, Errors.Create($"Argument 'ha-mode' has been set to {mode}, which means that argument 'ha-params' has to also be set"));

                return InternalErrors;

                bool IsHighlyAvailable() =>
                    _arguments is not null && (mode.Convert() == HighAvailabilityMode.Exactly || mode.Convert() == HighAvailabilityMode.Nodes) && !_arguments.ContainsKey("ha-params");
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
                SetArgWithConflictingCheck(arg, "queue-master-locator", value);
                SetArgWithConflictingCheck(arg, "ha-promote-on-shutdown", value);
                SetArgWithConflictingCheck(arg, "ha-promote-on-failure", value);
                SetArgWithConflictingCheck(arg, "delivery-limit", value);
            }

            public void SetExpiry(ulong milliseconds) => SetArg("expires", milliseconds);

            public void SetFederationUpstreamSet(string value) => SetArgWithConflictingCheck("federation-upstream-set", "federation-upstream", value.Trim());

            public void SetFederationUpstream(string value) => SetArgWithConflictingCheck("federation-upstream", "federation-upstream-set", value.Trim());

            public void SetHighAvailabilityMode(HighAvailabilityMode mode) => SetArg("ha-mode", mode.Convert());

            public void SetHighAvailabilityParams(uint value) => SetArg("ha-params", value);

            public void SetHighAvailabilitySyncMode(HighAvailabilitySyncMode mode) => SetArg("ha-sync-mode", mode.Convert());

            public void SetMessageTimeToLive(ulong milliseconds) => SetArg("message-ttl", milliseconds);

            public void SetMessageMaxSizeInBytes(ulong value) => SetArg("max-length-bytes", value.ToString());

            public void SetMessageMaxSize(ulong value) => SetArg("max-length", value);

            public void SetDeadLetterExchange(string value) => SetArg("dead-letter-exchange", value.Trim());

            public void SetDeadLetterRoutingKey(string value) => SetArg("dead-letter-routing-key", value.Trim());

            public void SetQueueMode(QueueMode mode) => SetArg("queue-mode", mode.Convert());

            public void SetAlternateExchange(string value) => SetArg("alternate-exchange", value.Trim());

            public void SetQueueMasterLocator(string key) => SetArg("queue-master-locator", key.Trim());

            public void SetQueuePromotionOnShutdown(QueuePromotionShutdownMode mode) => SetArg("ha-promote-on-shutdown", mode.Convert());

            public void SetQueuePromotionOnFailure(QueuePromotionFailureMode mode) => SetArg("ha-promote-on-failure", mode.Convert());

            public void SetDeliveryLimit(ulong limit) => SetArg("delivery-limit", limit);

            void SetArg(string arg, object value) =>
                _arguments.Add(arg.Trim(),
                    _arguments.ContainsKey(arg)
                        ? new ArgumentValue<object>(value, Errors.Create($"Argument '{arg}' has already been set"))
                        : new ArgumentValue<object>(value));

            void SetArgWithConflictingCheck(string arg, string targetArg, object value) =>
                _arguments.Add(arg.Trim(),
                    _arguments.ContainsKey(arg) || arg == targetArg && _arguments.ContainsKey(targetArg)
                        ? new ArgumentValue<object>(value, Errors.Create($"Argument '{arg}' has already been set or would conflict with argument '{targetArg}'"))
                        : new ArgumentValue<object>(value));

            void SetArgWithConflictingCheck(string arg, string targetArg, string conflictingArg, object value) =>
                _arguments.Add(arg.Trim(),
                    _arguments.ContainsKey(arg)
                    || arg == conflictingArg && _arguments.ContainsKey(targetArg)
                    || arg == targetArg && _arguments.ContainsKey(conflictingArg)
                        ? new ArgumentValue<object>(value, Errors.Create($"Argument '{conflictingArg}' has already been set or would conflict with argument '{arg}'"))
                        : new ArgumentValue<object>(value));
        }
    }
}