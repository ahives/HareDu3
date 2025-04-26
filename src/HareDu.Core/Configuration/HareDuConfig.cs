namespace HareDu.Core.Configuration;

/// <summary>
/// Represents the configuration settings for HareDu, including broker-specific and diagnostic-related configurations.
/// </summary>
public record HareDuConfig
{
    /// <summary>
    /// Gets the configuration settings for the broker connection.
    /// </summary>
    public BrokerConfig Broker { get; init; }

    /// <summary>
    /// Gets configuration for diagnostic API.
    /// </summary>
    public DiagnosticsConfig Diagnostics { get; init; }
}