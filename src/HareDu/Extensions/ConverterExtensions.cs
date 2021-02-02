namespace HareDu.Extensions
{
    using System;

    public static class ConverterExtensions
    {
        internal static string ConvertTo(this HighAvailabilityModes mode)
        {
            switch (mode)
            {
                case HighAvailabilityModes.All:
                    return "all";
                    
                case HighAvailabilityModes.Exactly:
                    return "exactly";
                    
                case HighAvailabilityModes.Nodes:
                    return "nodes";
                    
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
        
        internal static HighAvailabilityModes ConvertTo(this string mode)
        {
            switch (mode.ToLower())
            {
                case "all":
                    return HighAvailabilityModes.All;
                    
                case "exactly":
                    return HighAvailabilityModes.Exactly;
                    
                case "nodes":
                    return HighAvailabilityModes.Nodes;
                    
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        internal static string ConvertTo(this HighAvailabilitySyncModes mode)
        {
            switch (mode)
            {
                case HighAvailabilitySyncModes.Manual:
                    return "manual";
                    
                case HighAvailabilitySyncModes.Automatic:
                    return "automatic";
                    
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}