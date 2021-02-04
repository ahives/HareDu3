namespace HareDu.Diagnostics
{
    using System;
    using Probes;

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