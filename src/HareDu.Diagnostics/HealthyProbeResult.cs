namespace HareDu.Diagnostics
{
    using System;

    public record HealthyProbeResult :
        ProbeResult
    {
        public HealthyProbeResult()
        {
            Status = ProbeResultStatus.Healthy;
            Timestamp = DateTimeOffset.UtcNow;
        }
    }
}