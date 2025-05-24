namespace HareDu.Diagnostics.Scanners;

using System;
using System.Collections.Generic;
using Probes;

public abstract class BaseDiagnosticScanner
{
    protected BaseDiagnosticScanner(IReadOnlyList<DiagnosticProbe> probes)
    {
        Configure(probes ?? throw new ArgumentNullException(nameof(probes)));
    }

    protected abstract void Configure(IReadOnlyList<DiagnosticProbe> probes);
}