namespace HareDu.Diagnostics
{
    using System;

    public record UnhealthyProbeResult :
        ProbeResult
    {
        public UnhealthyProbeResult()
        {
            Status = ProbeResultStatus.Unhealthy;
            Timestamp = DateTimeOffset.UtcNow;
        }
    }
}