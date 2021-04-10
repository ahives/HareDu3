namespace HareDu.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using Core.Extensions;
    using MassTransit;
    using Scanners;
    using Snapshotting.Model;

    public record EmptyScannerResult :
        ScannerResult
    {
        public EmptyScannerResult()
        {
            Id = NewId.NextGuid();
            ScannerId = typeof(NoOpScanner<EmptySnapshot>).GetIdentifier();
            Results = new List<ProbeResult>();
            Timestamp = DateTimeOffset.UtcNow;
        }
    }
}