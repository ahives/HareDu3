namespace HareDu.Diagnostics
{
    using System;
    using System.Collections.Generic;

    public record ScannerResult
    {
        public Guid Id { get; init; }
        
        public string ScannerId { get; init; }
        
        public IReadOnlyList<ProbeResult> Results { get; init; }
        
        public DateTimeOffset Timestamp { get; init; }
    }
}