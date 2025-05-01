namespace HareDu.Core.Security;

using System;

/// <summary>
/// Represents a mechanism for configuring broker credentials within the HareDu library.
/// </summary>
public interface IHareDuCredentialBuilder
{
    /// <summary>
    /// Configures the broker credentials using the provided configurator.
    /// </summary>
    /// <param name="provider">An action that specifies how to configure the credentials.</param>
    /// <returns>A <see cref="HareDuCredentials"/> object representing the configured broker credentials.</returns>
    HareDuCredentials Build(Action<HareDuCredentialProvider> provider);
}