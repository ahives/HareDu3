namespace HareDu;

using System;

public interface BindingConfigurator
{
    void Source(string source);

    void Destination(string destination);

    void BindingType(BindingType bindingType);

    void BindingKey(string bindingKey);

    void OptionalArguments(Action<BindingArgumentConfigurator> configurator);
}