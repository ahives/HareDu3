namespace HareDu.Diagnostics.Model;

/// <summary>
/// Represents the possible outcomes of a diagnostic probe's execution, used to indicate the state of the evaluated resource or system.
/// </summary>
public enum ProbeResultStatus
{
    /// <summary>
    /// Represents a status indicating that the probe has detected an unhealthy condition in the evaluated resource or system.
    /// </summary>
    Unhealthy,

    /// <summary>
    /// Represents a status indicating that the probe has determined the evaluated resource or system is in a healthy state with no issues detected.
    /// </summary>
    Healthy,

    /// <summary>
    /// Indicates a status where the probe has detected conditions that may lead to an issue if not addressed.
    /// This status serves as a cautionary indicator, suggesting potential risk without confirming an unhealthy system state.
    /// </summary>
    Warning,

    /// <summary>
    /// Represents a status indicating that the probe was unable to definitively determine the condition of the evaluated resource or system.
    /// </summary>
    Inconclusive,

    /// <summary>
    /// Represents a status indicating that no assessment or evaluation has been made,
    /// leaving the result as "not applicable" or undefined.
    /// </summary>
    NA
}
