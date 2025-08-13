namespace HareDu.Extensions;

using System;

public static class EnumConversionExtensions
{
    /// <summary>
    /// Converts the specified <see cref="HighAvailabilityModes"/> enumeration value to its string equivalent.
    /// </summary>
    /// <param name="mode">The <see cref="HighAvailabilityModes"/> value to convert.</param>
    /// <returns>A string representation of the specified <see cref="HighAvailabilityModes"/> value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the value of <paramref name="mode"/> is not a defined <see cref="HighAvailabilityModes"/>.
    /// </exception>
    public static string Convert(this HighAvailabilityModes mode) =>
        mode switch
        {
            HighAvailabilityModes.All => "all",
            HighAvailabilityModes.Exactly => "exactly",
            HighAvailabilityModes.Nodes => "nodes",
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

    /// <summary>
    /// Converts the specified <see cref="HighAvailabilitySyncMode"/> enumeration value to its string equivalent.
    /// </summary>
    /// <param name="mode">The <see cref="HighAvailabilitySyncMode"/> value to convert.</param>
    /// <returns>A string representation of the specified <see cref="HighAvailabilitySyncMode"/> value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the value of <paramref name="mode"/> is not a defined <see cref="HighAvailabilitySyncMode"/>.
    /// </exception>
    public static HighAvailabilityModes Convert(this string mode) =>
        mode.ToLower() switch
        {
            "all" => HighAvailabilityModes.All,
            "exactly" => HighAvailabilityModes.Exactly,
            "nodes" => HighAvailabilityModes.Nodes,
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

    /// <summary>
    /// Converts the specified <see cref="HighAvailabilitySyncMode"/> enumeration value to its string equivalent.
    /// </summary>
    /// <param name="mode">The <see cref="HighAvailabilitySyncMode"/> value to convert.</param>
    /// <returns>A string representation of the specified <see cref="HighAvailabilitySyncMode"/> value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the value of <paramref name="mode"/> is not a defined <see cref="HighAvailabilitySyncMode"/>.
    /// </exception>
    public static string Convert(this HighAvailabilitySyncMode mode) =>
        mode switch
        {
            HighAvailabilitySyncMode.Manual => "manual",
            HighAvailabilitySyncMode.Automatic => "automatic",
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

    /// <summary>
    /// Converts the specified <see cref="QueueMode"/> enumeration value to its string equivalent.
    /// </summary>
    /// <param name="mode">The <see cref="QueueMode"/> value to convert.</param>
    /// <returns>A string representation of the specified <see cref="QueueMode"/> value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the value of <paramref name="mode"/> is not a defined <see cref="QueueMode"/>.
    /// </exception>
    public static string Convert(this QueueMode mode) =>
        mode switch
        {
            QueueMode.Default => "default",
            QueueMode.Lazy => "lazy",
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

    /// <summary>
    /// Converts the specified <see cref="QueuePromotionFailureMode"/> enumeration value to its string equivalent.
    /// </summary>
    /// <param name="mode">The <see cref="QueuePromotionFailureMode"/> value to convert.</param>
    /// <returns>A string representation of the specified <see cref="QueuePromotionFailureMode"/> value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the value of <paramref name="mode"/> is not a defined <see cref="QueuePromotionFailureMode"/>.
    /// </exception>
    public static string Convert(this QueuePromotionFailureMode mode) =>
        mode switch
        {
            QueuePromotionFailureMode.Always => "always",
            QueuePromotionFailureMode.WhenSynced => "when-synced",
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

    /// <summary>
    /// Converts the specified <see cref="QueuePromotionShutdownMode"/> enumeration value to its string equivalent.
    /// </summary>
    /// <param name="mode">The <see cref="QueuePromotionShutdownMode"/> value to convert.</param>
    /// <returns>A string representation of the specified <see cref="QueuePromotionShutdownMode"/> value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the value of <paramref name="mode"/> is not a defined <see cref="QueuePromotionShutdownMode"/>.
    /// </exception>
    public static string Convert(this QueuePromotionShutdownMode mode) =>
        mode switch
        {
            QueuePromotionShutdownMode.Always => "always",
            QueuePromotionShutdownMode.WhenSynced => "when-synced",
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

    /// <summary>
    /// Converts the specified <see cref="VirtualHostLimit"/> enumeration value to its string equivalent.
    /// </summary>
    /// <param name="limit">The <see cref="VirtualHostLimit"/> value to convert.</param>
    /// <returns>A string representation of the specified <see cref="VirtualHostLimit"/> value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the value of <paramref name="limit"/> is not a defined <see cref="VirtualHostLimit"/>.
    /// </exception>
    public static string Convert(this VirtualHostLimit limit) =>
        limit switch
        {
            VirtualHostLimit.MaxConnections => "max-connections",
            VirtualHostLimit.MaxQueues => "max-queues",
            _ => throw new ArgumentOutOfRangeException(nameof(limit), limit, null)
        };

    /// <summary>
    /// Converts the specified <see cref="UserLimit"/> enumeration value to its string equivalent.
    /// </summary>
    /// <param name="limit">The <see cref="UserLimit"/> value to convert.</param>
    /// <returns>A string representation of the specified <see cref="UserLimit"/> value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the value of <paramref name="limit"/> is not a defined <see cref="UserLimit"/>.
    /// </exception>
    public static string Convert(this UserLimit limit) =>
        limit switch
        {
            UserLimit.MaxConnections => "max-connections",
            UserLimit.MaxChannels => "max-channels",
            _ => throw new ArgumentOutOfRangeException(nameof(limit), limit, null)
        };
}