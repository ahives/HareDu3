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
    using Extensions;
    using Model;

    class GlobalParameterImpl :
        BaseBrokerObject,
        GlobalParameter
    {
        public GlobalParameterImpl(HttpClient client)
            : base(client)
        {
        }

        public async Task<ResultList<GlobalParameterInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/global-parameters";
            
            return await GetAll<GlobalParameterInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Create(string parameter, Action<NewGlobalParameterConfigurator> configurator, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();
            
            var impl = new NewGlobalParameterConfiguratorImpl(parameter);
            configurator?.Invoke(impl);
            
            impl.Validate();
            
            GlobalParameterDefinition definition = impl.Definition.Value;

            Debug.Assert(definition != null);

            var errors = new List<Error>();
            
            errors.AddRange(impl.Errors.Value);
            
            if (string.IsNullOrWhiteSpace(parameter))
                errors.Add(new(){Reason = "The name of the parameter is missing."});

            string url = $"api/global-parameters/{parameter}";
            
            if (errors.Any())
                return new FaultedResult{DebugInfo = new (){URL = url, Request = definition.ToJsonString(Deserializer.Options), Errors = errors}};

            return await Put(url, definition, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(string parameter, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();
            
            var errors = new List<Error>();
            
            if (string.IsNullOrWhiteSpace(parameter))
                errors.Add(new (){Reason = "The name of the parameter is missing."});

            string url = $"api/global-parameters/{parameter}";
            
            if (errors.Any())
                return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

            return await Delete(url, cancellationToken).ConfigureAwait(false);
        }


        class NewGlobalParameterConfiguratorImpl :
            NewGlobalParameterConfigurator
        {
            IDictionary<string, ArgumentValue<object>> _arguments;
            object _argument;
            
            readonly List<Error> _errors;

            public Lazy<GlobalParameterDefinition> Definition { get; }
            public Lazy<List<Error>> Errors { get; }

            public NewGlobalParameterConfiguratorImpl(string name)
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Definition = new Lazy<GlobalParameterDefinition>(
                    () => new GlobalParameterDefinition
                    {
                        Name = name,
                        Value = _argument.IsNotNull() ? _argument : _arguments.GetArgumentsOrNull()
                    }, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Value(Action<NewGlobalParameterArguments> arguments)
            {
                var impl = new NewGlobalParameterArgumentsImpl();
                arguments?.Invoke(impl);

                _arguments = impl.Arguments;
            }

            public void Value<T>(T argument)
            {
                _argument = argument;
            }

            public void Validate()
            {
                if (_argument != null && _argument.GetType() == typeof(string))
                {
                    if (string.IsNullOrWhiteSpace(_argument.ToString()))
                        _errors.Add(new() {Reason = "Parameter value is missing."});
                
                    return;
                }
                
                if (_argument == null && _arguments == null)
                    _errors.Add(new() {Reason = "Parameter value is missing."});
                
                if (_arguments != null)
                    _errors.AddRange(_arguments
                        .Select(x => x.Value?.Error)
                        .Where(error => error.IsNotNull())
                        .ToList());
            }


            class NewGlobalParameterArgumentsImpl :
                NewGlobalParameterArguments
            {
                public IDictionary<string, ArgumentValue<object>> Arguments { get; } =
                    new Dictionary<string, ArgumentValue<object>>();

                public void Add<T>(string arg, T value) =>
                    Arguments.Add(arg.Trim(),
                        Arguments.ContainsKey(arg)
                            ? new ArgumentValue<object>(value, $"Argument '{arg}' has already been set")
                            : new ArgumentValue<object>(value));
            }
        }
    }
}