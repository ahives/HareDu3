namespace HareDu;

using System;

/// <summary>
/// Provides methods for configuring a binding between an exchange and a queue or another exchange.
/// </summary>
public interface BindingConfigurator
{
    /// <summary>
    /// Specifies the destination for the binding.
    /// </summary>
    /// <param name="destination">The name of the destination where the binding will be applied.</param>
    void Destination(string destination);

    /// <summary>
    /// Specifies the binding key for the binding.
    /// </summary>
    /// <param name="bindingKey">The binding key used to route messages between the source and destination.</param>
    void BindingKey(string bindingKey);

    /// <summary>
    /// Configure optional arguments for the binding.
    /// </summary>
    /// <param name="configurator">A delegate to configure optional arguments using the <see cref="BindingArgumentConfigurator"/>.</param>
    void OptionalArguments(Action<BindingArgumentConfigurator> configurator);
}