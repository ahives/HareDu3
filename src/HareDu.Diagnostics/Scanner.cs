namespace HareDu.Diagnostics;

using System;
using System.Collections.Generic;
using MassTransit;
using Scanners;
using HareDu.Snapshotting.Model;

public class Scanner :
    IScanner
{
    readonly IScannerFactory _factory;

    public Scanner(IScannerFactory factory)
    {
        _factory = factory ?? throw new HareDuDiagnosticsException();
    }

    public ScannerResult Scan<T>(T snapshot)
        where T : Snapshot
    {
        if (!_factory.TryGet(out DiagnosticScanner<T> scanner))
            return DiagnosticCache.EmptyScannerResult;

        var results = scanner.Scan(snapshot);

        return new()
        {
            Id = NewId.NextGuid(),
            ScannerId = scanner.Metadata.Identifier,
            Results = results,
            Timestamp = DateTimeOffset.Now
        };
    }

    public IScanner RegisterObservers(IReadOnlyList<IObserver<ProbeContext>> observers)
    {
        _factory.RegisterObservers(observers);

        return this;
    }

    public IScanner RegisterObserver(IObserver<ProbeContext> observer)
    {
        _factory.RegisterObserver(observer);

        return this;
    }
}
