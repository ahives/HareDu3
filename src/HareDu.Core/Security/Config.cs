namespace HareDu.Core;

using Configuration;
using Security;

public static class Config
{
    /// <summary>
    /// Validates the given credentials and ensures they are not null or contain empty values.
    /// Throws a <see cref="HareDuSecurityException"/> if the credentials are invalid.
    /// </summary>
    /// <param name="credentials">The credentials to be validated.</param>
    /// <exception cref="HareDuSecurityException">Thrown when the provided credentials are invalid.</exception>
    public static void IfInvalid(HareDuCredentials credentials)
    {
        if (credentials is not null && !string.IsNullOrWhiteSpace(credentials.Username) && !string.IsNullOrWhiteSpace(credentials.Password))
            return;

        throw new HareDuSecurityException("Invalid user credentials.");
    }

    /// <summary>
    /// Validates the specified diagnostics configuration and ensures that it meets required conditions.
    /// Throws a <see cref="HareDuConfigurationException"/> if the configuration is invalid.
    /// </summary>
    /// <param name="config">The diagnostics configuration to be validated.</param>
    /// <exception cref="HareDuConfigurationException">Thrown when the diagnostics configuration is invalid.</exception>
    public static void IfInvalid(DiagnosticsConfig config)
    {
        if (IsValid(config))
            return;

        throw new HareDuConfigurationException("Invalid configuration.");
    }

    /// <summary>
    /// Validates the provided broker configuration and determines if it is invalid.
    /// Throws an exception if the configuration does not meet the required criteria.
    /// </summary>
    /// <param name="config">The broker configuration to validate.</param>
    /// <exception cref="HareDuConfigurationException">Thrown when the provided configuration is invalid.</exception>
    public static void IfInvalid(BrokerConfig config)
    {
        if (IsValid(config))
            return;

        throw new HareDuConfigurationException("Invalid configuration.");
    }

    static bool IsValid(DiagnosticsConfig config) =>
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

    static bool IsValid(BrokerConfig config)
        => !string.IsNullOrWhiteSpace(config.Url) &&
           config.Behavior is not null &&
           config.Behavior.MaxConcurrentRequests >= 1 &&
           config.Behavior.RequestReplenishmentInterval >= 1 &&
           config.Behavior.RequestsPerReplenishment >= 1;
}