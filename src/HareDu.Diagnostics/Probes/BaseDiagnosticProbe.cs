namespace HareDu.Diagnostics.Probes;

using System;
using System.Collections.Generic;
using KnowledgeBase;
using HareDu.Snapshotting.Model;

public abstract class BaseDiagnosticProbe<T> :
    IObservable<ProbeContext>
    where T : Snapshot
{
    protected readonly IKnowledgeBaseProvider _kb;
    readonly List<IObserver<ProbeContext>> _resultObservers;

    public abstract ProbeMetadata Metadata { get; }
    public abstract ComponentType ComponentType { get; }
    public abstract ProbeCategory Category { get; }

    protected BaseDiagnosticProbe(IKnowledgeBaseProvider kb)
    {
        _kb = kb;
        _resultObservers = new List<IObserver<ProbeContext>>();
    }

    public IDisposable Subscribe(IObserver<ProbeContext> observer)
    {
        if (!_resultObservers.Contains(observer))
            _resultObservers.Add(observer);

        return new UnsubscribeObserver<ProbeContext>(_resultObservers, observer);
    }

    protected virtual ProbeResult Execute(T snapshot)
    {
        var data = snapshot;

        if (data is not null)
            return GetProbeReadout(data);
        
        _kb.TryGet(Metadata.Id, ProbeResultStatus.Inconclusive, out var article);

        var result = Probe.Inconclusive(null, null, Metadata, ComponentType, null, article);

        NotifyObservers(result);

        return result;
    }

    protected virtual void NotifyObservers(ProbeResult result)
    {
        foreach (var observer in _resultObservers)
            observer.OnNext(new () {Result = result, Timestamp = DateTimeOffset.Now});
    }

    protected abstract ProbeResult GetProbeReadout(T data);
        
        
    protected class UnsubscribeObserver<U> :
        IDisposable
    {
        readonly List<IObserver<U>> _observers;
        readonly IObserver<U> _observer;

        public UnsubscribeObserver(List<IObserver<U>> observers, IObserver<U> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}