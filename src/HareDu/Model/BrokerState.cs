namespace HareDu.Model;

/// <summary>
/// Defines the possible states of a RabbitMQ broker in relation to its health and availability.
/// </summary>
/// <remarks>
/// The <see cref="BrokerState"/> enum represents the health status of a RabbitMQ broker based on responsiveness
/// and operational capability. Each value corresponds to a specific state of the broker, such as being active,
/// inactive, or unrecognized.
/// </remarks>
public enum BrokerState
{
    /// <summary>
    /// Represents the state of a RabbitMQ broker when it is determined to be alive and responsive.
    /// </summary>
    /// <remarks>
    /// The <see cref="BrokerState.Alive"/> status indicates that the broker is operational and able to handle requests
    /// for the specified virtual host. This state is typically returned when a successful health check confirms
    /// that the broker is active and functional.
    /// </remarks>
    Alive,

    /// <summary>
    /// Represents the state of a RabbitMQ broker when it is determined to be unresponsive or not operational.
    /// </summary>
    /// <remarks>
    /// The <see cref="BrokerState.NotAlive"/> status indicates that the broker is either not reachable or has failed
    /// to respond to connectivity or health check requests. This state is returned when the broker is found to be
    /// inactive or unavailable for the specified virtual host.
    /// </remarks>
    NotAlive,

    /// <summary>
    /// Represents a state where the status of a RabbitMQ broker could not be determined or is unrecognized.
    /// </summary>
    /// <remarks>
    /// The <see cref="BrokerState.NotRecognized"/> status indicates that the broker's state could not be classified
    /// as either alive or not alive. This may occur due to incomplete information, errors during the health check process,
    /// or an unexpected response from the broker.
    /// </remarks>
    NotRecognized
}