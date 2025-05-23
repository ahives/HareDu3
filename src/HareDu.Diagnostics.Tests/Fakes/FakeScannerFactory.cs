namespace HareDu.Diagnostics.Tests.Fakes;

using System;
using System.Collections.Generic;
using Diagnostics.Probes;
using Diagnostics.Scanners;
using HareDu.Snapshotting.Model;

public class FakeScannerFactory :
    IScannerFactory
{
    public IReadOnlyDictionary<string, DiagnosticProbe> Probes { get; }
    public IReadOnlyDictionary<string, object> Scanners { get; }

    public bool TryGet<T>(out DiagnosticScanner<T> scanner)
        where T : Snapshot
    {
        scanner = new NoOpScanner<T>(DiagnosticCache.EmptyProbes);
        return false;
    }

    public void RegisterObservers(IReadOnlyList<IObserver<ProbeContext>> observers)
    {
    }

    public void RegisterObserver(IObserver<ProbeContext> observer) => throw new NotImplementedException();

    public bool TryRegisterProbe<T>(T probe) where T : DiagnosticProbe => throw new NotImplementedException();
    public bool TryRegisterScanner<T>(DiagnosticScanner<T> scanner) where T : Snapshot => throw new NotImplementedException();

    public bool TryRegisterAllProbes() => throw new NotImplementedException();
    public bool TryRegisterAllScanners() => throw new NotImplementedException();
}