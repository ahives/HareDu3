namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core;
using Extensions;
using Model;

internal class BindingConfiguratorImpl :
    BindingConfigurator
{
    IDictionary<string, object> _arguments;
    string _bindingKeyString;

    List<Error> Errors { get; } = new();

    public string DestinationBinding { get; private set; }
    public Lazy<BindingRequest> Request { get; }

    public BindingConfiguratorImpl()
    {
        Request = new Lazy<BindingRequest>(
            () => new ()
            {
                BindingKey = _bindingKeyString,
                Arguments = _arguments
            }, LazyThreadSafetyMode.PublicationOnly);
    }

    public List<Error> Validate()
    {
        if (string.IsNullOrWhiteSpace(DestinationBinding))
            Errors.Add(new(){Reason = "The name of the destination binding (queue/exchange) is missing."});
            
        return Errors;
    }

    public void Destination(string destination) => DestinationBinding = destination;

    public void BindingKey(string bindingKey) => _bindingKeyString = bindingKey;

    public void OptionalArguments(Action<BindingArgumentConfigurator> configurator)
    {
        var impl = new BindingArgumentConfiguratorImpl();
        configurator?.Invoke(impl);

        _arguments = impl.Arguments.Value;
            
        Errors.AddRange(impl.Validate());
    }

        
    class BindingArgumentConfiguratorImpl :
        BindingArgumentConfigurator
    {
        readonly IDictionary<string, ArgumentValue<object>> _arguments;

        public Lazy<IDictionary<string, object>> Arguments { get; }

        public BindingArgumentConfiguratorImpl()
        {
            _arguments = new Dictionary<string, ArgumentValue<object>>();
                
            Arguments = new Lazy<IDictionary<string, object>>(() => _arguments.GetArgumentsOrEmpty(), LazyThreadSafetyMode.PublicationOnly);
        }

        public List<Error> Validate() =>
            _arguments
                .Select(x => x.Value?.Error)
                .Where(error => error is not null)
                .ToList();

        public void Add<T>(string arg, T value) =>
            _arguments.Add(arg.Trim(),
                _arguments.ContainsKey(arg)
                    ? new ArgumentValue<object>(value, $"Argument '{arg}' has already been set")
                    : new ArgumentValue<object>(value));
    }
}