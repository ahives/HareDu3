namespace HareDu.Diagnostics
{
    using System;

    public record NotApplicableProbeResult :
        ProbeResult
    {
        public NotApplicableProbeResult()
        {
            Data = Array.Empty<ProbeData>();
            Status = ProbeResultStatus.NA;
            Timestamp = DateTimeOffset.UtcNow;
        }
    }
}