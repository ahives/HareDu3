namespace HareDu.Core.Configuration;

using System;

/// <summary>
/// Defines a provider for configuring and retrieving instances of the HareDu configuration.
/// </summary>
public interface IHareDuConfigProvider
{
    /// <summary>
    /// Configures and returns the HareDu configuration settings by applying the specified configurator logic.
    /// </summary>
    /// <param name="configurator">
    /// A delegate that defines the configuration logic for HareDu by interacting with an instance of <see cref="HareDuConfigurator"/>.
    /// </param>
    /// <returns>
    /// A fully configured instance of <see cref="HareDuConfig"/>. Throws a <see cref="HareDuConfigurationException"/>
    /// if the configurator is invalid or the resulting configuration is not valid.
    /// </returns>
    /// <exception cref="HareDuConfigurationException">Throws if the configurator is invalid or the resulting configuration is not valid.</exception>
    HareDuConfig Configure(Action<HareDuConfigurator> configurator);
}