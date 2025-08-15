namespace HareDu.Model;

/// <summary>
/// Represents the possible states of an alarm.
/// </summary>
public enum AlarmState
{
    /// <summary>
    /// Indicates that the alarm is currently active and in effect.
    /// </summary>
    InEffect,

    /// <summary>
    /// Indicates that the alarm is currently not active or in effect.
    /// </summary>
    NotInEffect,

    /// <summary>
    /// Indicates that the alarm state could not be determined or is not recognized by the system.
    /// </summary>
    NotRecognized
}