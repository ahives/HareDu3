namespace HareDu.Core;

using System;
using Configuration;
using Security;

public static class Throw
{
    /// <summary>
    /// Validates the specified object or delegate and checks if it is null.
    /// Throws an exception of the specified type with the provided error message if the object is null.
    /// </summary>
    /// <param name="obj">The object or delegate to be validated.</param>
    /// <param name="message">The message that describes the error.</param>
    /// <typeparam name="T">The type of the object to be validated.</typeparam>
    /// <typeparam name="TException">The type of exception to be thrown if the validation fails.</typeparam>
    /// <exception cref="TException">Thrown when the specified object or delegate is null.</exception>
    public static void IfNull<T, TException>(Action<T> obj, string message)
        where TException : Exception, new()
    {
        if (obj is null)
            throw ((TException) Activator.CreateInstance(typeof(TException), message))!;
    }

    /// <summary>
    /// Checks if the specified object is null and, if it is, throws an exception of the specified type with the provided error message.
    /// </summary>
    /// <param name="obj">The object to be validated for null.</param>
    /// <param name="message">The message to be included in the exception if the object is null.</param>
    /// <typeparam name="T">The type of the object to be validated.</typeparam>
    /// <typeparam name="TException">The type of exception to be thrown if the validation fails.</typeparam>
    /// <exception cref="TException">Thrown when the specified object is null.</exception>
    public static void IfNull<T, TException>(T obj, string message)
        where TException : Exception, new()
    {
        if (obj is null)
            throw ((TException) Activator.CreateInstance(typeof(TException), message))!;
    }

    /// <summary>
    /// Validates the specified object and checks if it is null.
    /// Throws an exception of the specified type with the provided error message if the object is null.
    /// </summary>
    /// <param name="obj">The object to be validated.</param>
    /// <param name="message">The message that describes the error.</param>
    /// <typeparam name="TException">The type of exception to be thrown if the validation fails.</typeparam>
    /// <exception cref="TException">Thrown when the specified object is null.</exception>
    public static void IfNull<TException>(object obj, string message)
        where TException : Exception, new()
    {
        if (obj is null)
            throw ((TException) Activator.CreateInstance(typeof(TException), message))!;
    }

    /// <summary>
    /// Checks if a specific key is not found in the provided function and throws an exception of the specified type with the given error message if the key is missing.
    /// </summary>
    /// <param name="contains">The function to check for the existence of the key.</param>
    /// <param name="key">The key to validate if it exists.</param>
    /// <param name="message">The message that describes the error to be included in the exception.</param>
    /// <typeparam name="TException">The type of exception to be thrown if the key is not found.</typeparam>
    /// <exception cref="TException">Thrown when the key is not found in the provided function.</exception>
    public static void IfNotFound<TException>(Func<string, bool> contains, string key, string message)
        where TException : Exception, new()
    {
        if (string.IsNullOrWhiteSpace(key) || contains is null || !contains(key))
            throw ((TException) Activator.CreateInstance(typeof(TException), message))!;
    }

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

    /// <summary>
    /// Validates the provided knowledge base configuration and determines if it is invalid.
    /// Throws an exception if the configuration does not meet the required criteria.
    /// </summary>
    /// <param name="config">The knowledge base configuration to validate.</param>
    /// <exception cref="HareDuConfigurationException">Thrown when the provided configuration is invalid.</exception>
    public static void IfInvalid(KnowledgeBaseConfig config)
    {
        if (IsValid(config))
            return;

        throw new HareDuConfigurationException("Invalid configuration.");
    }

    static bool IsValid(DiagnosticsConfig config) =>
        config?.Probes is
        {
            ConsumerUtilizationThreshold: > 0,
            HighConnectionClosureRateThreshold: > 0,
            HighConnectionCreationRateThreshold: > 0,
            MessageRedeliveryThresholdCoefficient: > 0,
            QueueHighFlowThreshold: > 0,
            QueueLowFlowThreshold: > 0,
            SocketUsageThresholdCoefficient: > 0,
            FileDescriptorUsageThresholdCoefficient: > 0,
            RuntimeProcessUsageThresholdCoefficient: > 0
        };

    static bool IsValid(BrokerConfig config)
        => !string.IsNullOrWhiteSpace(config.Url) && config.Behavior is {MaxConcurrentRequests: >= 1, RequestReplenishmentInterval: >= 1, RequestsPerReplenishment: >= 1};

    static bool IsValid(KnowledgeBaseConfig config)
        => !string.IsNullOrWhiteSpace(config.File) && !string.IsNullOrWhiteSpace(config.Path);
}