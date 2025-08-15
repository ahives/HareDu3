namespace HareDu.Model;

/// <summary>
/// Represents the different states of a virtual host within the RabbitMQ broker.
/// </summary>
public enum VirtualHostState
{
    /// <summary>
    /// Represents the state of a virtual host that is currently operational and running without issues.
    /// </summary>
    Running,

    /// <summary>
    /// Represents the state of a virtual host that is not operational or currently inactive.
    /// </summary>
    NotRunning,

    /// <summary>
    /// Represents a state indicating that the status of a virtual host could not be determined or is unrecognized.
    /// This state typically occurs when the system is unable to identify or map the received response to a known state.
    /// </summary>
    NotRecognized
}