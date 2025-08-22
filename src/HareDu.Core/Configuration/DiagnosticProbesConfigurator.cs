namespace HareDu.Core.Configuration;

/// <summary>
/// Defines methods for configuring diagnostic probes for the RabbitMQ broker.
/// </summary>
public interface DiagnosticProbesConfigurator
{
    /// <summary>
    /// Sets the maximum acceptable rate at which connections can be closed on the RabbitMQ broker.
    /// </summary>
    /// <param name="threshold">Maximum acceptable rate at which connections can be closed.</param>
    void SetHighConnectionClosureRateThreshold(uint threshold);

    /// <summary>
    /// Sets the maximum acceptable rate at which connections to the RabbitMQ broker can be established in order to determine whether or not it is considered healthy.
    /// </summary>
    /// <param name="threshold">Maximum acceptable rate at which connections to the RabbitMQ broker can be established</param>
    void SetHighConnectionCreationRateThreshold(uint threshold);

    /// <summary>
    /// Sets the maximum acceptable number of messages that can be published to a queue.
    /// </summary>
    /// <param name="threshold">Maximum acceptable number of messages that can be published to a queue.</param>
    void SetQueueHighFlowThreshold(uint threshold);

    /// <summary>
    /// Sets the minimum acceptable number of messages that can be published to a queue.
    /// </summary>
    /// <param name="threshold">Minimum acceptable number of messages that can be published to a queue.</param>
    void SetQueueLowFlowThreshold(uint threshold);

    /// <summary>
    /// Sets the coefficient that is used to calculate the acceptable number of message redelivers.
    /// </summary>
    /// <param name="coefficient">Coefficient that is used to calculate the acceptable number of message redelivers.</param>
    void SetMessageRedeliveryThresholdCoefficient(decimal coefficient);

    /// <summary>
    /// Sets the coefficient used to calculate the acceptable number of sockets that can be used.
    /// </summary>
    /// <param name="coefficient">Coefficient used to calculate the acceptable number of sockets that can be used.</param>
    void SetSocketUsageThresholdCoefficient(decimal coefficient);

    /// <summary>
    /// Sets the coefficient used to calculate the acceptable number of runtime processes that can be used.
    /// </summary>
    /// <param name="coefficient">Coefficient used to calculate the acceptable number of runtime processes that can be used.</param>
    void SetRuntimeProcessUsageThresholdCoefficient(decimal coefficient);

    /// <summary>
    /// Sets the coefficient used to calculate the acceptable number of file descriptors/handles that can be used.
    /// </summary>
    /// <param name="coefficient">Coefficient used to calculate the acceptable number of file descriptors/handles that can be used.</param>
    void SetFileDescriptorUsageThresholdCoefficient(decimal coefficient);

    /// <summary>
    /// Sets the minimum acceptable percentage of consumers that are consuming messages from a particular queue.
    /// </summary>
    /// <param name="threshold">Minimum acceptable percentage of consumers that are consuming messages from a particular queue.</param>
    void SetConsumerUtilizationThreshold(decimal threshold);
}