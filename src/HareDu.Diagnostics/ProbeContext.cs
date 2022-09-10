namespace HareDu.Diagnostics;

using System;

public record ProbeContext
{
    public ProbeResult Result { get; init; }
        
    public DateTimeOffset Timestamp { get; init; }
}