namespace HareDu;

using System;

public interface BindingConfigurator
{
    void Destination(string destination);

    void BindingKey(string bindingKey);

    void OptionalArguments(Action<BindingArgumentConfigurator> configurator);
}