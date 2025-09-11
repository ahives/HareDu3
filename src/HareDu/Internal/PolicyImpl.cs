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
using Serialization;

class PolicyImpl :
    BaseHareDuImpl,
    Policy
{
    public PolicyImpl(HttpClient client)
        : base(client, BrokerDeserializer.Instance)
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
            ? Response.Panic(Debug.Info("api/policies/{vhost}/{name}", errors, request: Deserializer.ToJsonString(request)))
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
        string _pattern;
        int _priority;
        PolicyAppliedTo _appliedTo;
        PolicyDefinition _definition;

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
                    Definition = _definition
                }, LazyThreadSafetyMode.PublicationOnly);
        }

        public void Definition(Action<PolicyArgumentConfigurator> configurator)
        {
            var impl = new PolicyArgumentConfiguratorImpl();
            configurator?.Invoke(impl);

            _definition = impl.Definition.Value;
            
            InternalErrors.AddRange(impl.Validate());
        }

        public void Pattern(string pattern) => _pattern = pattern;

        public void Priority(int priority) => _priority = priority;

        public void ApplyTo(PolicyAppliedTo applyTo) => _appliedTo = applyTo;

        public List<Error> Validate()
        {
            if (_definition is not null)
            {
                InternalErrors.AddIfTrue(_definition.OverflowBehavior,
                    x => _appliedTo == PolicyAppliedTo.QuorumQueues && x is QueueOverflowBehavior.RejectPublishDeadLetter,
                    Errors.Create("Quorum queues do not support the 'reject-publish-dlx' overflow behavior."));
            }
 
            InternalErrors.AddIfTrue(_pattern, string.IsNullOrWhiteSpace, Errors.Create("Pattern was not set."));
 
            return InternalErrors;
        }


        class PolicyArgumentConfiguratorImpl :
            PolicyArgumentConfigurator
        {
            ulong _autoExpire;
            string _federationUpstreamSet;
            string _federationUpstream;
            ulong _messageTimeToLive;
            string _alternateExchange;
            ulong _maxLengthBytes;
            ulong _maxLength;
            string _deadLetterExchangeName;
            string _deadLetterRoutingKey;
            QueueMode _queueMode;
            string _queueMasterLocator;
            uint _deliveryLimit;
            QueueOverflowBehavior _overflowBehavior;
            QueueLeaderLocator _queueLeaderLocator;
            uint _consumerTimeout;
            DeadLetterQueueStrategy _deadLetterQueueStrategy;
            string _maxAge;

            public Lazy<PolicyDefinition> Definition { get; }

            List<Error> InternalErrors { get; } = new();

            public PolicyArgumentConfiguratorImpl()
            {
                Definition = new Lazy<PolicyDefinition>(() => new PolicyDefinition
                {
                    DeadLetterExchangeName = _deadLetterExchangeName,
                    DeadLetterQueueStrategy = _deadLetterQueueStrategy,
                    OverflowBehavior = _overflowBehavior,
                    QueueLeaderLocator = _queueLeaderLocator,
                    AutoExpire = _autoExpire,
                    MaxAge = _maxAge,
                    ConsumerTimeout = _consumerTimeout,
                    DeadLetterRoutingKey = _deadLetterRoutingKey,
                    DeliveryLimit = _deliveryLimit,
                    MaxLength = _maxLength,
                    MaxLengthBytes = _maxLengthBytes,
                    MessageTimeToLive = _messageTimeToLive,
                    FederationUpstream = _federationUpstream,
                    FederationUpstreamSet = _federationUpstreamSet,
                    QueueMode = _queueMode,
                    AlternateExchange = _alternateExchange,
                    QueueMasterLocator = _queueMasterLocator
                }, LazyThreadSafetyMode.PublicationOnly);
            }

            public List<Error> Validate()
            {
                return InternalErrors;
            }

            public void SetExpiry(ulong milliseconds)
            {
                _autoExpire = milliseconds;

                InternalErrors.AddIfTrue(milliseconds, x => x < 1,
                    Errors.Create("Argument 'expires' has been set without an appropriate value."));
            }

            public void SetFederationUpstreamSet(string value)
            {
                _federationUpstreamSet = value.Trim();

                InternalErrors.AddIfTrue(value, string.IsNullOrWhiteSpace,
                    Errors.Create("Argument 'federation-upstream-set' has been set without a corresponding value."));
            }

            public void SetFederationUpstream(string value)
            {
                _federationUpstream = value.Trim();

                InternalErrors.AddIfTrue(value, string.IsNullOrWhiteSpace,
                    Errors.Create("Argument 'federation-upstream' has been set without a corresponding value."));
            }

            public void SetMessageTimeToLive(ulong milliseconds)
            {
                _messageTimeToLive = milliseconds;

                InternalErrors.AddIfTrue(milliseconds, x => x < 1,
                    Errors.Create("Argument 'message-ttl' has been set without an appropriate value."));
            }

            public void SetMessageMaxSizeInBytes(ulong value)
            {
                _maxLengthBytes = value;

                InternalErrors.AddIfTrue(value, x => x < 1,
                    Errors.Create("Argument 'max-length-bytes' has been set without an appropriate value."));
            }

            public void SetMessageMaxSize(ulong value)
            {
                _maxLength = value;

                InternalErrors.AddIfTrue(value, x => x < 1,
                    Errors.Create("Argument 'max-length' has been set without an appropriate value."));
            }

            public void SetDeadLetterExchangeName(string name)
            {
                _deadLetterExchangeName = name.Trim();

                InternalErrors.AddIfTrue(name, string.IsNullOrWhiteSpace,
                    Errors.Create("Argument 'dead-letter-exchange' has been set without a corresponding value."));
            }

            public void SetDeadLetterRoutingKey(string value)
            {
                _deadLetterRoutingKey = value.Trim();

                InternalErrors.AddIfTrue(value, string.IsNullOrWhiteSpace,
                    Errors.Create("Argument 'dead-letter-routing-key' has been set without a corresponding value."));
            }

            public void SetQueueMode(QueueMode mode) => _queueMode = mode;

            public void SetAlternateExchange(string value)
            {
                _alternateExchange = value.Trim();

                InternalErrors.AddIfTrue(value, string.IsNullOrWhiteSpace,
                    Errors.Create("Argument 'alternate-exchange' has been set without a corresponding value."));
            }

            public void SetQueueMasterLocator(string locator)
            {
                _queueMasterLocator = locator.Trim();

                InternalErrors.AddIfTrue(locator, string.IsNullOrWhiteSpace,
                    Errors.Create("Argument 'queue-leader-locator' has been set without a corresponding value."));
            }

            public void SetDeliveryLimit(uint limit)
            {
                _deliveryLimit = limit;

                InternalErrors.AddIfTrue(limit, x => x < 1,
                    Errors.Create("Argument 'delivery-limit' has been set without an appropriate value."));
            }

            public void SetQueueOverflowBehavior(QueueOverflowBehavior behavior) => _overflowBehavior = behavior;

            public void SetQueueLeaderLocator(QueueLeaderLocator locator) => _queueLeaderLocator = locator;

            public void SetConsumerTimeout(uint timeout)
            {
                _consumerTimeout = timeout;

                InternalErrors.AddIfTrue(timeout, x => x < 1,
                    Errors.Create("Argument 'consumer-timeout' has been set without an appropriate value."));
            }

            public void SetDeadLetterQueueStrategy(DeadLetterQueueStrategy strategy) => _deadLetterQueueStrategy = strategy;

            public void SetMaxAge(uint duration, TimeUnit units)
            {
                _maxAge = $"{duration}{units.Convert()}";

                InternalErrors.AddIfTrue(duration, x => x < 1,
                    Errors.Create("Argument 'max-age' has been set without an appropriate value."));
            }
        }
    }
}