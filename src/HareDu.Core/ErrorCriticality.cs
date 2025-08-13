namespace HareDu.Core;

/// <summary>
/// Represents the criticality level of an error, indicating the severity or nature of the issue.
/// </summary>
public enum ErrorCriticality
{
    /// <summary>
    /// Represents a critical error where the issue is considered severe and demands immediate attention.
    /// Typically indicates a failure or condition that significantly impacts functionality or operation.
    /// </summary>
    Critical,

    /// <summary>
    /// Represents a non-critical error where the issue is less severe and does not require immediate action.
    /// Typically indicates a condition that may be noteworthy but does not significantly impact functionality or operation.
    /// </summary>
    NonCritical
}