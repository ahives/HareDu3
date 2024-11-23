namespace HareDu.Diagnostics.Scanners;

using System.Collections.Generic;
using Probes;

public abstract class BaseDiagnosticScanner
{
    protected abstract void Configure(IReadOnlyList<DiagnosticProbe> probes);
}