namespace HareDu.Diagnostics
{
    using System;
    using Probes;

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