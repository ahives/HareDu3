namespace HareDu.Core;

using System;
using Configuration;
using Security;

public static class Defend
{
    /// <summary>
    /// Validates the provided credential provider action and ensures it is not null.
    /// Throws a <see cref="HareDuSecurityException"/> if the provider is invalid.
    /// </summary>
    /// <param name="provider">The action that sets up the credential provider to be validated.</param>
    /// <exception cref="HareDuSecurityException">Thrown when the provided credential provider is null.</exception>
    public static void AgainstNullParams(Action<HareDuCredentialProvider> provider)
    {
        if (provider is null)
            throw new HareDuSecurityException("Invalid user credentials.");
    }

    /// <summary>
    /// Validates the provided configurator action and ensures it is not null.
    /// Throws a <see cref="HareDuConfigurationException"/> if the configurator is invalid.
    /// </summary>
    /// <param name="configurator">The action that sets up the configurator to be validated.</param>
    /// <exception cref="HareDuConfigurationException">Thrown when the provided configurator is null.</exception>
    public static void AgainstNullParams(Action<HareDuConfigurator> configurator)
    {
        if (configurator is null)
            throw new HareDuConfigurationException("Invalid configuration.");
    }

    /// <summary>
    /// Validates the given credentials and ensures they are not null or contain empty values.
    /// Throws a <see cref="HareDuSecurityException"/> if the credentials are invalid.
    /// </summary>
    /// <param name="credentials">The credentials to be validated.</param>
    /// <exception cref="HareDuSecurityException">Thrown when the provided credentials are invalid.</exception>
    public static void AgainstInvalidCredentials(HareDuCredentials credentials)
    {
        if (!string.IsNullOrWhiteSpace(credentials?.Username) && !string.IsNullOrWhiteSpace(credentials?.Password))
            return;

        throw new HareDuSecurityException("Invalid user credentials.");
    }

    /// <summary>
    /// Validates the provided HareDu configuration and ensures it meets the required criteria for broker and diagnostics settings.
    /// Throws a <see cref="HareDuConfigurationException"/> if the configuration is invalid.
    /// </summary>
    /// <param name="config">The HareDu configuration object to be validated.</param>
    /// <exception cref="HareDuConfigurationException">Thrown when the provided HareDu configuration is invalid or incomplete.</exception>
    public static void AgainstInvalidConfig(HareDuConfig config)
    {
        if (Validate(config.Broker) && Validate(config.Diagnostics))
            return;

        throw new HareDuConfigurationException("Invalid configuration.");
    }

    static bool Validate(DiagnosticsConfig config) =>
        config?.Probes != null 
        && config.Probes.ConsumerUtilizationThreshold > 0 
        && config.Probes.HighConnectionClosureRateThreshold > 0 
        && config.Probes.HighConnectionCreationRateThreshold > 0 
        && config.Probes.MessageRedeliveryThresholdCoefficient > 0 
        && config.Probes.QueueHighFlowThreshold > 0 
        && config.Probes.QueueLowFlowThreshold > 0 
        && config.Probes.SocketUsageThresholdCoefficient > 0 
        && config.Probes.FileDescriptorUsageThresholdCoefficient > 0 
        && config.Probes.RuntimeProcessUsageThresholdCoefficient > 0;

    static bool Validate(BrokerConfig config)
        => !string.IsNullOrWhiteSpace(config.Url) &&
           config?.Behavior.MaxConcurrentRequests >= 1 &&
           config?.Behavior.RequestReplenishmentInterval >= 1 &&
           config?.Behavior.RequestsPerReplenishment >= 1;
}