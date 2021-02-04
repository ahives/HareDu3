namespace HareDu.Diagnostics
{
    using System;
    using Probes;

    public record NotApplicableProbeResult :
        ProbeResult
    {
        public NotApplicableProbeResult()
        {
            Status = ProbeResultStatus.NA;
            Data = Array.Empty<ProbeData>();
            Timestamp = DateTimeOffset.UtcNow;
        }
    }
}