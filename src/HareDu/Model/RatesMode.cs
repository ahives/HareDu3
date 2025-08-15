namespace HareDu.Model;

/// <summary>
/// Specifies the mode of rate calculations for monitoring or reporting purposes.
/// </summary>
public enum RatesMode
{
    /// <summary>
    /// Represents the absence of a rates mode, indicating that no rate calculations should be applied or used.
    /// </summary>
    None,

    /// <summary>
    /// Specifies that basic rates mode should be used, offering a simplified
    /// and general rate information for metrics or operations.
    /// </summary>
    Basic,

    /// <summary>
    /// Specifies that detailed rates mode should be used, providing more granular
    /// and comprehensive rate information for metrics or operations.
    /// </summary>
    Detailed
}