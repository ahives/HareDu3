namespace HareDu.Diagnostics
{
    using System;

    public record WarningProbeResult :
        ProbeResult
    {
        public WarningProbeResult()
        {
            Status = ProbeResultStatus.Warning;
            Timestamp = DateTimeOffset.UtcNow;
        }
    }
}