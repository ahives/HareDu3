namespace HareDu.Diagnostics.Tests.Fakes;

using System.Collections.Generic;
using Core.Extensions;
using Diagnostics.Scanners;

public class FakeDiagnosticScanner :
    DiagnosticScanner<FakeSnapshot>
{
    public ScannerMetadata Metadata => new()
    {
        Identifier = GetType().GetIdentifier()
    };

    public IReadOnlyList<ProbeResult> Scan(FakeSnapshot snapshot) => throw new System.NotImplementedException();
}