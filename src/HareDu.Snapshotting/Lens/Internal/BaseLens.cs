namespace HareDu.Snapshotting.Lens.Internal;

using System;
using System.Collections.Generic;
using Model;

public abstract class BaseLens<T> :
    IObservable<SnapshotContext<T>>
    where T : Snapshot
{
    protected readonly IBrokerObjectFactory _factory;
    protected readonly Lazy<SnapshotHistory<T>> _timeline;
    protected readonly IDictionary<string, SnapshotResult<T>> _snapshots;
        
    readonly List<IObserver<SnapshotContext<T>>> _observers;

    protected BaseLens(IBrokerObjectFactory factory)
    {
        _factory = factory;
        _observers = new List<IObserver<SnapshotContext<T>>>();
        _snapshots = new Dictionary<string, SnapshotResult<T>>();
        _timeline = new Lazy<SnapshotHistory<T>>(() => new SnapshotHistory<T>(_snapshots));
    }

    public IDisposable Subscribe(IObserver<SnapshotContext<T>> observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);

        return new UnsubscribeObserver(_observers, observer);
    }

    protected virtual void NotifyObservers(string identifier, T snapshot, DateTimeOffset timestamp)
    {
        foreach (var observer in _observers)
            observer.OnNext(new SnapshotContext<T>{Identifier = identifier, Snapshot = snapshot, Timestamp = timestamp});
    }

    protected virtual void NotifyObserversOfError(HareDuSnapshotException e)
    {
        foreach (var observer in _observers)
            observer.OnError(e);
    }

    protected virtual void SaveSnapshot(string identifier, T snapshot, DateTimeOffset timestamp)
    {
        if (snapshot is null)
            return;
            
        _snapshots.Add(identifier, new SnapshotResult<T>{Identifier = identifier, Snapshot = snapshot, Timestamp = timestamp});
    }


    class UnsubscribeObserver :
        IDisposable
    {
        readonly List<IObserver<SnapshotContext<T>>> _observers;
        readonly IObserver<SnapshotContext<T>> _observer;

        public UnsubscribeObserver(List<IObserver<SnapshotContext<T>>> observers, IObserver<SnapshotContext<T>> observer)
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