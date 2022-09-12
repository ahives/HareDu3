namespace HareDu.Diagnostics.Tests.Fakes;

using System.Collections.Generic;
using Core.Extensions;
using Diagnostics.Probes;
using Diagnostics.Scanners;

public class FakeDiagnosticScanner :
    DiagnosticScanner<FakeSnapshot>
{
    public DiagnosticScannerMetadata Metadata => new()
    {
        Identifier = GetType().GetIdentifier()
    };

    public IReadOnlyList<ProbeResult> Scan(FakeSnapshot snapshot) => throw new System.NotImplementedException();

    public void Configure(IReadOnlyList<DiagnosticProbe> probes)
    {
        throw new System.NotImplementedException();
    }
}