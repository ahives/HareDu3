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

class OperatorPolicyImpl :
    BaseBrokerImpl,
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
        IDictionary<string, ulong> _arguments;
        string _pattern;
        int _priority;
        OperatorPolicyAppliedTo _appliedTo;

        List<Error> InteernalErrors { get; } = new();

        public Lazy<OperatorPolicyRequest> Request { get; }

        public OperatorPolicyConfiguratorImpl()
        {
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
            
            InteernalErrors.AddRange(impl.Validate());
        }

        public void Pattern(string pattern) => _pattern = pattern;

        public void Priority(int priority) => _priority = priority;

        public void ApplyTo(OperatorPolicyAppliedTo applyTo) => _appliedTo = applyTo;

        public List<Error> Validate()
        {
            InteernalErrors.AddIfTrue(_pattern, string.IsNullOrWhiteSpace, Errors.Create("The pattern is missing."));

            return InteernalErrors;
        }


        class OperatorPolicyArgumentConfiguratorImpl :
            OperatorPolicyArgumentConfigurator
        {
            readonly IDictionary<string, ArgumentValue<ulong>> _arguments;

            public Lazy<IDictionary<string, ulong>> Arguments { get; }

            public OperatorPolicyArgumentConfiguratorImpl()
            {
                _arguments = new Dictionary<string, ArgumentValue<ulong>>();

                Arguments = new Lazy<IDictionary<string, ulong>>(() => _arguments.GetArgumentsOrNull(), LazyThreadSafetyMode.PublicationOnly);
            }

            public void SetExpiry(ulong milliseconds) => SetArg("expires", milliseconds);
                
            public void SetMaxInMemoryBytes(ulong bytes) => SetArg("max-in-memory-bytes", bytes);

            public void SetMaxInMemoryLength(ulong messages) => SetArg("max-in-memory-length", messages);

            public void SetDeliveryLimit(ulong limit) => SetArg("delivery-limit", limit);

            public void SetMessageTimeToLive(ulong milliseconds) => SetArg("message-ttl", milliseconds);

            public void SetMessageMaxSizeInBytes(ulong value) => SetArg("max-length-bytes", value);

            public void SetMessageMaxSize(ulong value) => SetArg("max-length", value);

            public List<Error> Validate()
            {
                List<Error> errors = new();

                errors.AddIfTrue(_arguments, x => x is null || !x.Any(), Errors.Create("No arguments have been set."));

                return errors;
            }
            
            void SetArg(string arg, ulong value) =>
                _arguments.Add(arg.Trim(),
                    _arguments.ContainsKey(arg)
                        ? new ArgumentValue<ulong>(value, Errors.Create($"Argument '{arg}' has already been set"))
                        : new ArgumentValue<ulong>(value));
        }
    }
}