namespace HareDu;

/// <summary>
/// Represents the possible states of the protocol listener on the RabbitMQ broker.
/// </summary>
public enum ProtocolListenerState
{
    /// <summary>
    /// Represents the state where the protocol listener is actively engaged
    /// and successfully handling incoming communication on the RabbitMQ broker.
    /// </summary>
    Active,

    /// <summary>
    /// Represents the state where the protocol listener is not actively engaged
    /// and unable to handle incoming communication on the RabbitMQ broker.
    /// </summary>
    NotActive,

    /// <summary>
    /// Represents a state where the protocol listener's condition or state cannot be identified or mapped
    /// due to unexpected input or unknown circumstances.
    /// </summary>
    NotRecognized
}