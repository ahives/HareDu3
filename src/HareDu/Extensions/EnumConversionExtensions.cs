namespace HareDu.Extensions;

using System;
using Model;

public static class EnumConversionExtensions
{
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

    /// <summary>
    /// Converts the specified <see cref="QueueOverflowBehavior"/> enumeration value to its string equivalent.
    /// </summary>
    /// <param name="behavior">The <see cref="QueueOverflowBehavior"/> value to convert.</param>
    /// <returns>A string representation of the specified <see cref="QueueOverflowBehavior"/> value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the value of <paramref name="behavior"/> is not a defined <see cref="QueueOverflowBehavior"/>.
    /// </exception>
    public static string Convert(this QueueOverflowBehavior behavior) =>
        behavior switch
        {
            QueueOverflowBehavior.DropHead => "drop-head",
            QueueOverflowBehavior.RejectPublish => "reject-publish",
            QueueOverflowBehavior.RejectPublishDeadLetter => "reject-publish-dlx",
            _ => throw new ArgumentOutOfRangeException(nameof(behavior), behavior, null)
        };

    /// <summary>
    /// Converts the specified <see cref="QueueLeaderLocator"/> enumeration value to its string equivalent.
    /// </summary>
    /// <param name="locator">The <see cref="QueueLeaderLocator"/> value to convert.</param>
    /// <returns>A string representation of the specified <see cref="QueueLeaderLocator"/> value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the value of <paramref name="locator"/> is not a defined <see cref="QueueLeaderLocator"/>.
    /// </exception>
    public static string Convert(this QueueLeaderLocator locator) =>
        locator switch
        {
            QueueLeaderLocator.ClientLocal => "client-local",
            QueueLeaderLocator.Balanced => "balanced",
            _ => throw new ArgumentOutOfRangeException(nameof(locator), locator, null)
        };

    /// <summary>
    /// Converts the specified <see cref="DeadLetterQueueStrategy"/> enumeration value to its string representation.
    /// </summary>
    /// <param name="strategy">The <see cref="DeadLetterQueueStrategy"/> value to convert.</param>
    /// <returns>A string representation of the specified <see cref="DeadLetterQueueStrategy"/> value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the value of <paramref name="strategy"/> is not a defined <see cref="DeadLetterQueueStrategy"/>.
    /// </exception>
    public static string Convert(this DeadLetterQueueStrategy strategy) =>
        strategy switch
        {
            DeadLetterQueueStrategy.AtMostOnce => "at-most-once",
            DeadLetterQueueStrategy.AtLeastOnce => "at-least-once",
            _ => throw new ArgumentOutOfRangeException(nameof(strategy), strategy, null)
        };

    /// <summary>
    /// Converts the specified <see cref="QueueMode"/> enumeration value to its string equivalent.
    /// </summary>
    /// <param name="mode">The <see cref="QueueMode"/> value to convert.</param>
    /// <returns>A string representation of the specified <see cref="QueueMode"/> value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the value of <paramref name="mode"/> is not a defined <see cref="QueueMode"/>.
    /// </exception>
    public static string Convert(this TimeUnit unit) =>
        unit switch
        {
            TimeUnit.Days => "D",
            TimeUnit.Months => "M",
            TimeUnit.Years => "Y",
            TimeUnit.Hours => "h",
            TimeUnit.Minutes => "m",
            TimeUnit.Seconds => "s",
            _ => throw new ArgumentOutOfRangeException(nameof(unit), unit, null)
        };
}