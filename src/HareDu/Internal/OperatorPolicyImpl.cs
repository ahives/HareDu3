namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Model;
using Serialization;

class OperatorPolicyImpl :
    BaseHareDuImpl,
    OperatorPolicy
{
    public OperatorPolicyImpl(HttpClient client) :
        base(client, new BrokerDeserializer())
    {
    }
        
    public async Task<Results<OperatorPolicyInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<OperatorPolicyInfo>("api/operator-policies", RequestType.OperatorPolicy, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(string name, string vhost, Action<OperatorPolicyConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator is null)
            return Response.Panic(Debug.Info("api/operator-policies/{vhost}/{name}",
                Errors.Create(e => { e.Add("No operator policy was defined."); })));

        var impl = new OperatorPolicyConfiguratorImpl();
        configurator(impl);

        string sanitizedVHost = vhost.ToSanitizedName();
        var request = impl.Request.Value;
        var errors = impl.Validate();

        errors.AddIfTrue(name, string.IsNullOrWhiteSpace, Errors.Create("The name of the operator policy is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("api/operator-policies/{vhost}/{name}", errors, request: Deserializer.ToJsonString(request)))
            : await PutRequest($"api/operator-policies/{sanitizedVHost}/{name}", request, RequestType.OperatorPolicy, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string name, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = new List<Error>();

        errors.AddIfTrue(name, string.IsNullOrWhiteSpace, Errors.Create("The name of the operator policy is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("api/operator-policies/{vhost}/{name}", errors))
            : await DeleteRequest($"api/operator-policies/{sanitizedVHost}/{name}", RequestType.OperatorPolicy, cancellationToken).ConfigureAwait(false);
    }


    class OperatorPolicyConfiguratorImpl :
        OperatorPolicyConfigurator
    {
        string _pattern;
        int _priority;
        OperatorPolicyAppliedTo _appliedTo;
        OperatorPolicyDefinition _definition;

        List<Error> InternalErrors { get; } = new();

        public Lazy<OperatorPolicyRequest> Request { get; }

        public OperatorPolicyConfiguratorImpl()
        {
            Request = new Lazy<OperatorPolicyRequest>(
                () => new ()
                {
                    Pattern = _pattern,
                    Priority = _priority,
                    ApplyTo = _appliedTo,
                    Definition = _definition
                }, LazyThreadSafetyMode.PublicationOnly);
        }

        public void Definition(Action<OperatorPolicyArgumentConfigurator> configurator)
        {
            var impl = new OperatorPolicyArgumentConfiguratorImpl();
            configurator?.Invoke(impl);

            _definition = impl.Definition.Value;
            
            InternalErrors.AddRange(impl.Validate());
        }

        public void Pattern(string pattern) => _pattern = pattern;

        public void Priority(int priority) => _priority = priority;

        public void ApplyTo(OperatorPolicyAppliedTo applyTo) => _appliedTo = applyTo;

        public List<Error> Validate()
        {
            InternalErrors.AddIfTrue(_pattern, string.IsNullOrWhiteSpace, Errors.Create("The pattern is missing."));

            return InternalErrors;
        }


        class OperatorPolicyArgumentConfiguratorImpl :
            OperatorPolicyArgumentConfigurator
        {
            ulong _autoExpire;
            ulong _messageTimeToLive;
            ulong _maxLengthBytes;
            ulong _maxLength;
            uint _deliveryLimit;
            QueueOverflowBehavior _overflowBehavior;
            ulong _maxInMemoryBytes;
            ulong _maxInMemoryLength;
            uint _targetGroupSize;

            public Lazy<OperatorPolicyDefinition> Definition { get; }

            List<Error> InternalErrors { get; } = new();

            public OperatorPolicyArgumentConfiguratorImpl()
            {
                Definition = new Lazy<OperatorPolicyDefinition>(() => new OperatorPolicyDefinition
                {
                    OverflowBehavior = _overflowBehavior,
                    AutoExpire = _autoExpire,
                    DeliveryLimit = _deliveryLimit,
                    MaxLength = _maxLength,
                    MaxLengthBytes = _maxLengthBytes,
                    MessageTimeToLive = _messageTimeToLive,
                    MaxInMemoryBytes = _maxInMemoryBytes,
                    MaxInMemoryLength = _maxInMemoryLength,
                    TargetGroupSize = _targetGroupSize
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

            public void SetMaxInMemoryBytes(ulong bytes)
            {
                _maxInMemoryBytes = bytes;

                InternalErrors.AddIfTrue(bytes, x => x < 1,
                    Errors.Create("Argument 'max-in-memory-bytes' has been set without an appropriate value."));
            }

            public void SetMaxInMemoryLength(ulong messages)
            {
                _maxInMemoryLength = messages;

                InternalErrors.AddIfTrue(messages, x => x < 1,
                    Errors.Create("Argument 'max-in-memory-length' has been set without an appropriate value."));
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

            public void SetDeliveryLimit(uint limit)
            {
                _deliveryLimit = limit;

                InternalErrors.AddIfTrue(limit, x => x < 1,
                    Errors.Create("Argument 'delivery-limit' has been set without an appropriate value."));
            }

            public void SetQueueOverflowBehavior(QueueOverflowBehavior behavior) => _overflowBehavior = behavior;

            public void SetTargetGroupSize(uint size)
            {
                _targetGroupSize = size;

                InternalErrors.AddIfTrue(size, x => x < 1,
                    Errors.Create("Argument 'target-group-size' has been set without an appropriate value."));
            }
        }
    }
}