namespace HareDu.Core.Configuration;

/// <summary>
/// Represents the configuration settings for diagnostic operations.
/// </summary>
public record DiagnosticsConfig
{
    /// <summary>
    /// Represents the probes configuration used for diagnostic purposes, allowing customization of thresholds
    /// and parameters for analyzing the health and performance of the system.
    /// </summary>
    public ProbesConfig Probes { get; init; }
}