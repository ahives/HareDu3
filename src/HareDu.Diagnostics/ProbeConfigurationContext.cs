namespace HareDu.Diagnostics
{
    using System;
    using Core.Configuration;

    public record ProbeConfigurationContext
    {
        public string ProbeId { get; init; }
        
        public string ProbeName { get; init; }
        
        public DiagnosticsConfig Current { get; init; }
        
        public DiagnosticsConfig New { get; init; }
        
        public DateTimeOffset Timestamp { get; init; }
    }
}