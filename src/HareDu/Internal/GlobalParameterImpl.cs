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

        public async Task<Result> Create(Action<NewGlobalParameterConfiguration> configuration, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();
            
            var impl = new NewGlobalParameterConfigurationImpl();
            configuration?.Invoke(impl);
            
            impl.Validate();
            
            GlobalParameterDefinition definition = impl.Definition.Value;

            Debug.Assert(definition != null);

            string url = $"api/global-parameters/{definition.Name}";
            
            if (impl.Errors.Value.Any())
                return new FaultedResult{Errors = impl.Errors.Value, DebugInfo = new (){URL = url, Request = definition.ToJsonString()}};

            return await Put(url, definition, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(Action<DeleteGlobalParameterConfiguration> configuration, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new DeleteGlobalParameterConfigurationImpl();
            configuration?.Invoke(impl);
            
            if (string.IsNullOrWhiteSpace(impl.ParameterName))
                return new FaultedResult{Errors = new List<Error> {new (){Reason = "The name of the parameter is missing."}}, DebugInfo = new (){URL = @"api/global-parameters/", Request = null}};

            string url = $"api/global-parameters/{impl.ParameterName}";

            return await Delete(url, cancellationToken).ConfigureAwait(false);
        }

        
        class DeleteGlobalParameterConfigurationImpl :
            DeleteGlobalParameterConfiguration
        {
            public string ParameterName { get; private set; }
            
            public void Parameter(string name) => ParameterName = name;
        }


        class NewGlobalParameterConfigurationImpl :
            NewGlobalParameterConfiguration
        {
            IDictionary<string, ArgumentValue<object>> _arguments;
            string _name;
            object _argument;
            readonly List<Error> _errors;

            public Lazy<GlobalParameterDefinition> Definition { get; }
            public Lazy<List<Error>> Errors { get; }

            public NewGlobalParameterConfigurationImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Definition = new Lazy<GlobalParameterDefinition>(
                    () => new GlobalParameterDefinition
                    {
                        Name = _name,
                        Value = _argument.IsNotNull() ? _argument : _arguments.GetArguments()
                    }, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Parameter(string name) => _name = name;
            
            public void Configure(Action<NewGlobalParameterConfigurator> configurator)
            {
                var impl = new NewGlobalParameterConfiguratorImpl();
                configurator?.Invoke(impl);

                _argument = impl.Argument.Value;
                _arguments = impl.Arguments.Value;
            }

            
            class NewGlobalParameterConfiguratorImpl :
                NewGlobalParameterConfigurator
            {
                IDictionary<string, ArgumentValue<object>> _arguments;
                object _argument;
                
                readonly List<Error> _errors;

                public Lazy<IDictionary<string, ArgumentValue<object>>> Arguments { get; }
                public Lazy<object> Argument { get; }
                public Lazy<List<Error>> Errors { get; }

                public NewGlobalParameterConfiguratorImpl()
                {
                    _errors = new List<Error>();
                
                    Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                    Arguments = new Lazy<IDictionary<string, ArgumentValue<object>>>(() => _arguments, LazyThreadSafetyMode.PublicationOnly);
                    Argument = new Lazy<object>(() => _argument, LazyThreadSafetyMode.PublicationOnly);
                }

                public void Value(Action<GlobalParameterArguments> arguments)
                {
                    var impl = new GlobalParameterArgumentsImpl();
                    arguments?.Invoke(impl);

                    _arguments = impl.Arguments;
                }

                public void Value<T>(T argument)
                {
                    _argument = argument;
                }
            }

            public void Validate()
            {
                if (string.IsNullOrWhiteSpace(_name))
                    _errors.Add(new() {Reason = "The name of the parameter is missing."});

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


            class GlobalParameterArgumentsImpl :
                GlobalParameterArguments
            {
                public IDictionary<string, ArgumentValue<object>> Arguments { get; } =
                    new Dictionary<string, ArgumentValue<object>>();

                public void Set<T>(string arg, T value) =>
                    Arguments.Add(arg.Trim(),
                        Arguments.ContainsKey(arg)
                            ? new ArgumentValue<object>(value, $"Argument '{arg}' has already been set")
                            : new ArgumentValue<object>(value));
            }
        }
    }
}