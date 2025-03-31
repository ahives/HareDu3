namespace HareDu.Extensions;

using System;

public static class EnumConversionExtensions
{
    public static string Convert(this DeleteShovelMode mode) =>
        mode switch
        {
            DeleteShovelMode.Never => "never",
            DeleteShovelMode.QueueLength => "queue-length",
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

    public static string Convert(this HighAvailabilityModes mode) =>
        mode switch
        {
            HighAvailabilityModes.All => "all",
            HighAvailabilityModes.Exactly => "exactly",
            HighAvailabilityModes.Nodes => "nodes",
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

    public static HighAvailabilityModes Convert(this string mode) =>
        mode.ToLower() switch
        {
            "all" => HighAvailabilityModes.All,
            "exactly" => HighAvailabilityModes.Exactly,
            "nodes" => HighAvailabilityModes.Nodes,
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

    public static string Convert(this HighAvailabilitySyncMode mode) =>
        mode switch
        {
            HighAvailabilitySyncMode.Manual => "manual",
            HighAvailabilitySyncMode.Automatic => "automatic",
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

    public static string Convert(this QueueMode mode) =>
        mode switch
        {
            QueueMode.Default => "default",
            QueueMode.Lazy => "lazy",
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

    public static string Convert(this QueuePromotionFailureMode mode) =>
        mode switch
        {
            QueuePromotionFailureMode.Always => "always",
            QueuePromotionFailureMode.WhenSynced => "when-synced",
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

    public static string Convert(this QueuePromotionShutdownMode mode) =>
        mode switch
        {
            QueuePromotionShutdownMode.Always => "always",
            QueuePromotionShutdownMode.WhenSynced => "when-synced",
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

    public static string Convert(this VirtualHostLimit limit) =>
        limit switch
        {
            VirtualHostLimit.MaxConnections => "max-connections",
            VirtualHostLimit.MaxQueues => "max-queues",
            _ => throw new ArgumentOutOfRangeException(nameof(limit), limit, null)
        };

    public static string Convert(this UserLimit limit) =>
        limit switch
        {
            UserLimit.MaxConnections => "max-connections",
            UserLimit.MaxChannels => "max-channels",
            _ => throw new ArgumentOutOfRangeException(nameof(limit), limit, null)
        };
}