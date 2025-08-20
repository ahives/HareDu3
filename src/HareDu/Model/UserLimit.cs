namespace HareDu.Model;

/// <summary>
/// Defines the types of resource limits that can be applied to a RabbitMQ user.
/// These limits are used to control and manage the allocation of certain resources for specific users.
/// </summary>
public enum UserLimit
{
    /// <summary>
    /// Represents the maximum number of concurrent connections a specific RabbitMQ user is allowed to establish.
    /// This limit is used to control the connection resources allocated to a user to avoid resource exhaustion or unfair usage.
    /// </summary>
    MaxConnections,

    /// <summary>
    /// Represents the maximum number of concurrent channels a specific RabbitMQ user is allowed to open.
    /// This limit is used to manage and control the user channel resources to ensure efficient utilization and prevent overuse.
    /// </summary>
    MaxChannels
}