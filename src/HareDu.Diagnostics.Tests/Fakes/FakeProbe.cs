namespace HareDu.Diagnostics.Tests.Fakes;

using System;
using Core.Extensions;
using Diagnostics.Probes;

public class FakeProbe :
    DiagnosticProbe
{
    public IDisposable Subscribe(IObserver<ProbeContext> observer) => throw new NotImplementedException();

    public ProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Fake Probe",
            Description = ""
        };
    public ComponentType ComponentType { get; }
    public ProbeCategory Category { get; }
    public ProbeResult Execute<T>(T snapshot) => new();
}