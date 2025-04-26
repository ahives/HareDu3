namespace HareDu.Core.Configuration;

/// <summary>
/// Represents the configuration settings for diagnostic probes in HareDu, which are used to monitor and evaluate various system metrics.
/// </summary>
public record ProbesConfig
{
    /// <summary>
    /// Represents the threshold used to detect high rates of connection closures within the system
    /// over a specified period. This property helps identify potential patterns of instability
    /// or issues related to connection handling that may require investigation or corrective action.
    /// </summary>
    public uint HighConnectionClosureRateThreshold { get; init; }

    /// <summary>
    /// Represents the threshold used to identify high rates of new connection creation within a system
    /// over a defined period. This value is utilized to detect potential connection management issues
    /// or unusual activity patterns that could impact system performance.
    /// </summary>
    public uint HighConnectionCreationRateThreshold { get; init; }

    /// <summary>
    /// Specifies the threshold for identifying high flow conditions in message queues.
    /// This value represents the maximum number of messages processed over a specific
    /// period, above which the queue is marked as experiencing unusually high activity.
    /// Configuring this threshold aids in detecting potential system overloading or
    /// performance inefficiencies related to queue usage.
    /// </summary>
    public uint QueueHighFlowThreshold { get; init; }

    /// <summary>
    /// Defines the threshold for detecting low flow conditions in message queues.
    /// This value indicates the minimum number of messages processed over a specific
    /// period, below which the queue is flagged for potential performance issues or
    /// bottlenecks. Calibrating this threshold allows for better monitoring and
    /// diagnostics of queue processing rates in the system.
    /// </summary>
    public uint QueueLowFlowThreshold { get; init; }

    /// <summary>
    /// Represents the threshold coefficient used to assess message redelivery within the system.
    /// This value defines a limit at which frequent message redelivery scenarios are flagged for diagnostics,
    /// aiding in the detection of inefficiencies or bottlenecks in message processing.
    /// Adjusting this coefficient can influence sensitivity to redelivery patterns and system performance analysis.
    /// </summary>
    public decimal MessageRedeliveryThresholdCoefficient { get; init; }

    /// <summary>
    /// Represents the threshold coefficient used to monitor socket usage within the system.
    /// This value helps in identifying potential issues related to socket resource consumption
    /// by defining a threshold that triggers diagnostic probes. Adjusting this coefficient
    /// can affect the sensitivity of socket usage diagnostics and performance monitoring.
    /// </summary>
    public decimal SocketUsageThresholdCoefficient { get; init; }

    /// <summary>
    /// Represents the threshold coefficient used to monitor runtime process usage within the system.
    /// This value helps to detect potential performance issues by analyzing the system's runtime
    /// resource consumption at predefined thresholds. Adjusting this coefficient can influence
    /// the sensitivity of diagnostics related to runtime process behavior and usage levels.
    /// </summary>
    public decimal RuntimeProcessUsageThresholdCoefficient { get; init; }

    /// <summary>
    /// Represents the threshold coefficient used to monitor file descriptor usage within the system.
    /// This value is utilized to evaluate the system's ability to manage open file descriptors effectively.
    /// Adjusting this coefficient can influence the sensitivity of file descriptor utilization monitoring,
    /// potentially highlighting issues like resource exhaustion or excessive usage under high workload conditions.
    /// </summary>
    public decimal FileDescriptorUsageThresholdCoefficient { get; init; }

    /// <summary>
    /// Represents the threshold value for monitoring the consumer utilization within the system.
    /// This property defines the coefficient used to evaluate the consumer's efficiency in processing messages.
    /// A lower value may indicate under-utilization, while a higher value may point to potential performance bottlenecks.
    /// </summary>
    public decimal ConsumerUtilizationThreshold { get; init; }
}