namespace HareDu.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using MassTransit;

public class ScannerResultAnalyzer :
    IScannerResultAnalyzer,
    IObservable<AnalyzerContext>
{
    readonly IList<IDisposable> _disposableObservers;
    readonly List<IObserver<AnalyzerContext>> _observers;

    public ScannerResultAnalyzer()
    {
        _observers = new List<IObserver<AnalyzerContext>>();
        _disposableObservers = new List<IDisposable>();
    }

    public IReadOnlyList<AnalyzerSummary> Analyze(ScannerResult report, Func<ProbeResult, string> filterBy)
    {
        var rollup = GetRollup(report.Results, filterBy);

        var summary = (from result in rollup
                let healthy =
                    new AnalyzerResult
                    {
                        Total = result.Value.Count(x => x == ProbeResultStatus.Healthy).ConvertTo(),
                        Percentage = CalcPercentage(result.Value, ProbeResultStatus.Healthy)
                    }
                let unhealthy =
                    new AnalyzerResult
                    {
                        Total = result.Value.Count(x => x == ProbeResultStatus.Unhealthy).ConvertTo(),
                        Percentage = CalcPercentage(result.Value, ProbeResultStatus.Unhealthy)
                    }
                let warning =
                    new AnalyzerResult
                    {
                        Total = result.Value.Count(x => x == ProbeResultStatus.Warning).ConvertTo(),
                        Percentage = CalcPercentage(result.Value, ProbeResultStatus.Warning)
                    }
                let inconclusive =
                    new AnalyzerResult
                    {
                        Total = result.Value.Count(x => x == ProbeResultStatus.Inconclusive).ConvertTo(),
                        Percentage = CalcPercentage(result.Value, ProbeResultStatus.Inconclusive)
                    }
                select new AnalyzerSummary
                {
                    Id = result.Key,
                    Healthy = healthy,
                    Unhealthy = unhealthy,
                    Warning = warning,
                    Inconclusive = inconclusive
                })
            .ToList();

        NotifyObservers(summary);

        return summary;
    }

    public IScannerResultAnalyzer RegisterObserver(IObserver<AnalyzerContext> observer)
    {
        _disposableObservers.Add(Subscribe(observer));

        return this;
    }

    public IDisposable Subscribe(IObserver<AnalyzerContext> observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);

        return new UnsubscribeObserver(_observers, observer);
    }

    void NotifyObservers(List<AnalyzerSummary> result)
    {
        foreach (var observer in _observers)
            observer.OnNext(new() {Id = NewId.NextGuid(), Summary = result, Timestamp = DateTimeOffset.Now});
    }

    decimal CalcPercentage(List<ProbeResultStatus> results, ProbeResultStatus status)
    {
        decimal totalCount = Convert.ToDecimal(results.Count);
        decimal statusCount = Convert.ToDecimal(results.Count(x => x == status));

        return Convert.ToDecimal(statusCount / totalCount * 100);
    }

    IDictionary<string, List<ProbeResultStatus>> GetRollup(IReadOnlyList<ProbeResult> results,
        Func<ProbeResult, string> filterBy)
    {
        var rollup = new Dictionary<string, List<ProbeResultStatus>>();

        for (int i = 0; i < results.Count; i++)
        {
            string key = filterBy(results[i]);

            if (rollup.ContainsKey(key))
                rollup[key].Add(results[i].Status);
            else
                rollup.Add(key, new List<ProbeResultStatus> {results[i].Status});
        }

        return rollup;
    }


    class UnsubscribeObserver :
        IDisposable
    {
        readonly List<IObserver<AnalyzerContext>> _observers;
        readonly IObserver<AnalyzerContext> _observer;

        public UnsubscribeObserver(List<IObserver<AnalyzerContext>> observers, IObserver<AnalyzerContext> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer is not null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
