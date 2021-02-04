namespace HareDu.Diagnostics
{
    using System;
    using Probes;

    public record InconclusiveProbeResult :
        ProbeResult
    {
        public InconclusiveProbeResult()
        {
            Status = ProbeResultStatus.Inconclusive;
            Timestamp = DateTimeOffset.UtcNow;
        }
    }
}