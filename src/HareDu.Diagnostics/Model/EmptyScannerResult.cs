namespace HareDu.Diagnostics.Model;

using System;
using System.Collections.Generic;
using Core.Extensions;
using Scanners;
using Snapshotting.Model;

/// <summary>
/// Represents an implementation of <see cref="ScannerResult"/> with default values.
/// Used as a placeholder or default result when no actual diagnostic scanning is performed.
/// </summary>
public record EmptyScannerResult :
    ScannerResult
{
    public EmptyScannerResult()
    {
        Id = Guid.CreateVersion7(DateTimeOffset.UtcNow);
        ScannerId = typeof(NoOpScanner<EmptySnapshot>).GetIdentifier();
        Results = new List<ProbeResult>();
        Timestamp = DateTimeOffset.UtcNow;
    }
}