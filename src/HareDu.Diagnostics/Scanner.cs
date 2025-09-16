namespace HareDu.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;
using Scanners;
using HareDu.Snapshotting.Model;
using Model;

public class Scanner :
    IScanner
{
    readonly IScannerFactory _factory;
    readonly List<IObserver<ProbeContext>> _subscribers;

    public Scanner(IScannerFactory factory)
    {
        _factory = factory ?? throw new HareDuDiagnosticsException();
        _subscribers = new List<IObserver<ProbeContext>>();
    }

    public ScannerResult Scan<T>(T snapshot)
        where T : Snapshot
    {
        _factory.RegisterObservers(_subscribers);

        if (!_factory.TryGet(out DiagnosticScanner<T> scanner))
            return DiagnosticCache.EmptyScannerResult;

        var results = scanner.Scan(snapshot);

        return new()
        {
            Id = Guid.CreateVersion7(DateTimeOffset.UtcNow),
            ScannerId = scanner.Metadata.Identifier,
            Results = results,
            Timestamp = DateTimeOffset.UtcNow
        };
    }

    public IScanner RegisterSubscribers(IObserver<ProbeContext> subscriber, params IObserver<ProbeContext>[] subscribers)
    {
        _subscribers.AddRange(subscribers.Prepend(subscriber).ToList());

        return this;
    }
}
