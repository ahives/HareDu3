namespace HareDu.Diagnostics.Model;

/// <summary>
/// Defines the types of components that can be analyzed or monitored in the diagnostics module.
/// Each type represents a specific area within the system or application, allowing diagnostic probes
/// to target and evaluate the health or performance of that particular component.
/// </summary>
public enum ComponentType
{
    /// <summary>
    /// Represents the type of diagnostic component that corresponds to a connection in the system.
    /// This component type is typically used to perform diagnostics and evaluations on connection-related
    /// metrics or properties within the broader system or application.
    /// </summary>
    Connection,

    /// <summary>
    /// Represents the type of diagnostic component that corresponds to a channel within the system.
    /// This component type is commonly used to perform diagnostics, monitoring, and evaluations
    /// related to channel-specific metrics or behaviors in the broader RabbitMQ environment.
    /// </summary>
    Channel,

    /// <summary>
    /// Represents the type of diagnostic component associated with a queue in the messaging system.
    /// This component type is commonly used to analyze and evaluate queue-specific metrics such as
    /// throughput, utilization, or other performance-related characteristics.
    /// </summary>
    Queue,

    /// <summary>
    /// Represents the type of diagnostic component that corresponds to a node in the system.
    /// This component type is typically used to perform diagnostics and evaluations on node-related
    /// metrics or properties within the broader system or application.
    /// </summary>
    Node,

    /// <summary>
    /// Represents the type of diagnostic component corresponding to a storage disk in the system.
    /// This component type is typically used to assess and monitor metrics or conditions related
    /// to disk utilization and health within the system or application.
    /// </summary>
    Disk,

    /// <summary>
    /// Represents the type of diagnostic component that corresponds to memory in the system.
    /// This component type is primarily used for analyzing and evaluating memory-related metrics
    /// or performance characteristics within the context of system diagnostics.
    /// </summary>
    Memory,

    /// <summary>
    /// Represents the type of diagnostic component that corresponds to the runtime environment.
    /// This component type is used to evaluate or analyze the performance and behavior of the
    /// application's runtime execution context or environment.
    /// </summary>
    Runtime,

    /// <summary>
    /// Represents the type of diagnostic component associated with the operating system of the machine.
    /// This component type is used to evaluate and analyze metrics related to the operating system's performance
    /// and health within the system or application.
    /// </summary>
    OperatingSystem,

    /// <summary>
    /// Represents the type of diagnostic component that corresponds to an exchange in the messaging system.
    /// This component type is used to assess and evaluate metrics or properties related to exchanges,
    /// including their performance, configuration, and potential issues within the system.
    /// </summary>
    Exchange,

    /// <summary>
    /// Represents an undefined or unspecified diagnostic component type within the system.
    /// This value may be used as a default or placeholder when no specific component type
    /// is applicable or identified for a diagnostic probe or operation.
    /// </summary>
    NA
}