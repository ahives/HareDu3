namespace HareDu.Shovel.Extensions;

using Model;

public static class EnumConversionExtensions
{
    /// <summary>
    /// Converts the specified <see cref="ShovelDeleteMode"/> enumeration value to its string equivalent.
    /// </summary>
    /// <param name="mode">The <see cref="ShovelDeleteMode"/> value to convert.</param>
    /// <returns>A string representation of the specified <see cref="ShovelDeleteMode"/> value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the value of <paramref name="mode"/> is not a defined <see cref="ShovelDeleteMode"/>.
    /// </exception>
    public static string Convert(this ShovelDeleteMode mode) =>
        mode switch
        {
            ShovelDeleteMode.Never => "never",
            ShovelDeleteMode.QueueLength => "queue-length",
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

    /// <summary>
    /// Converts the specified <see cref="ShovelAckMode"/> enumeration value to its string equivalent.
    /// </summary>
    /// <param name="mode">The <see cref="ShovelAckMode"/> value to convert.</param>
    /// <returns>A string representation of the specified <see cref="ShovelAckMode"/> value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the value of <paramref name="mode"/> is not a defined <see cref="ShovelAckMode"/>.
    /// </exception>
    public static string Convert(this ShovelAckMode mode) =>
        mode switch
        {
            ShovelAckMode.NoAck => "no-ack",
            ShovelAckMode.OnConfirm => "on-confirm",
            ShovelAckMode.OnPublish => "on-publish",
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
}