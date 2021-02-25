namespace HareDu.Diagnostics
{
    using System;

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