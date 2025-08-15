namespace HareDu.Model;

/// <summary>
/// Represents the various states of a broker connection during its lifecycle.
/// This enumeration defines the possible conditions the connection might experience
/// while establishing, maintaining, or terminating communication.
/// </summary>
public enum BrokerConnectionState
{
    /// <summary>
    /// Represents the state of a broker connection when it is in the process of starting up.
    /// This state indicates that the connection is initializing and preparing for further operations.
    /// </summary>
    Starting,

    /// <summary>
    /// Represents the state of a broker connection when it is being tuned for optimal configuration.
    /// This state indicates adjustments or settings are being applied to the connection.
    /// </summary>
    Tuning,

    /// <summary>
    /// Represents the state of a broker connection when it is in the process of being opened.
    /// This state indicates that the connection is transitioning from initialization to becoming active.
    /// </summary>
    Opening,

    /// <summary>
    /// Represents the state of a broker connection when it is actively running and operational.
    /// This state indicates that the connection is functioning normally without disruptions.
    /// </summary>
    Running,

    /// <summary>
    /// Represents the state of a broker connection when it is regulating the flow of messages.
    /// This state indicates that the connection is controlling or throttling message transmission
    /// to maintain stability or ensure resource availability.
    /// </summary>
    Flow,

    /// <summary>
    /// Represents the state of a broker connection when it is actively engaging in a blocking operation.
    /// This state is typically used to indicate that connections are being restricted or are unable to process further actions temporarily.
    /// </summary>
    Blocking,

    /// <summary>
    /// Represents the state of a broker connection when it is blocked.
    /// This state indicates that the connection has been restricted and cannot process or respond to operations
    /// until the blocking condition is resolved.
    /// </summary>
    Blocked,

    /// <summary>
    /// Represents the state of a broker connection when it is in the process of closing.
    /// This state indicates that the connection is terminating its operations and preparing to shut down.
    /// </summary>
    Closing,

    /// <summary>
    /// Represents the state of a broker connection when it has completed its lifecycle and is no longer active.
    /// This state indicates that the connection has been fully closed and is not capable of performing any operations.
    /// </summary>
    Closed,

    /// <summary>
    /// Represents a state of a broker connection that cannot be conclusively determined.
    /// This state indicates that the connection's status is ambiguous or unclear during diagnostic evaluation.
    /// </summary>
    Inconclusive
}