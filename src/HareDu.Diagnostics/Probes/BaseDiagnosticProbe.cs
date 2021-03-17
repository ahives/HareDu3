namespace HareDu.Diagnostics.Probes
{
    using System;
    using System.Collections.Generic;
    using Core.Configuration;
    using KnowledgeBase;

    public abstract class BaseDiagnosticProbe :
        IObservable<ProbeContext>
    {
        protected readonly IKnowledgeBaseProvider _kb;
        readonly List<IObserver<ProbeContext>> _resultObservers;

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

        protected virtual void NotifyObservers(ProbeResult result)
        {
            foreach (var observer in _resultObservers)
                observer.OnNext(new () {Result = result, Timestamp = DateTimeOffset.Now});
        }
        
        
        protected class UnsubscribeObserver<T> :
            IDisposable
        {
            readonly List<IObserver<T>> _observers;
            readonly IObserver<T> _observer;

            public UnsubscribeObserver(List<IObserver<T>> observers, IObserver<T> observer)
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
}