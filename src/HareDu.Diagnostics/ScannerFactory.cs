namespace HareDu.Diagnostics;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Configuration;
using KnowledgeBase;
using Probes;
using Extensions;
using Scanners;
using HareDu.Snapshotting.Model;
using HareDu.Core.Extensions;

/// <summary>
/// A factory that provides functionality to manage and create diagnostic scanners
/// and probes for the purpose of analyzing system snapshots and diagnostics contexts.
/// </summary>
public class ScannerFactory :
    IScannerFactory
{
    readonly IKnowledgeBaseProvider _kb;
    readonly ConcurrentDictionary<string, object> _scannerCache;
    readonly ConcurrentDictionary<string, DiagnosticProbe> _probeCache;
    readonly IList<IDisposable> _observers;
    readonly HareDuConfig _config;

    public IReadOnlyDictionary<string, DiagnosticProbe> Probes => _probeCache;

    public IReadOnlyDictionary<string, object> Scanners => _scannerCache;

    public ScannerFactory(HareDuConfig config, IKnowledgeBaseProvider kb)
    {
        _config = config ?? throw new HareDuDiagnosticsException($"{nameof(config)} argument missing.");
        _kb = kb ?? throw new HareDuDiagnosticsException($"{nameof(kb)} argument missing.");
        _scannerCache = new ConcurrentDictionary<string, object>();
        _probeCache = new ConcurrentDictionary<string, DiagnosticProbe>();
        _observers = new List<IDisposable>();

        if (!TryRegisterAllProbes())
            throw new HareDuDiagnosticsException("Could not register diagnostic probes.");

        if (!TryRegisterAllScanners())
            throw new HareDuDiagnosticsException("Could not register diagnostic scanners.");
    }

    public bool TryGet<T>(out DiagnosticScanner<T> scanner)
        where T : Snapshot
    {
        Type type = typeof(T);

        if (string.IsNullOrWhiteSpace(type.FullName) || !_scannerCache.TryGetValue(type.FullName, out var value))
        {
            scanner = new NoOpScanner<T>(DiagnosticCache.EmptyProbes);
            return false;
        }

        scanner = (DiagnosticScanner<T>) value;
        return true;
    }

    public void RegisterObservers(IReadOnlyList<IObserver<ProbeContext>> observers)
    {
        var probes = _probeCache.Values.ToList();

        for (int i = 0; i < observers.Count; i++)
        {
            if (observers[i] is null)
                continue;

            for (int j = 0; j < probes.Count; j++)
                _observers.Add(probes[j].Subscribe(observers[i]));
        }
    }

    public void RegisterObserver(IObserver<ProbeContext> observer)
    {
        if (observer is null)
            return;

        foreach (var probe in _probeCache.Values.ToList())
            _observers.Add(probe.Subscribe(observer));
    }

    public bool TryRegisterProbe<T>(T probe)
        where T : DiagnosticProbe
    {
        bool probeAdded = _probeCache.TryAdd(typeof(T).FullName, probe);

        if (probe is null || !probeAdded)
            return false;

        _scannerCache.ConfigureAll(Configure);

        return true;
    }

    public bool TryRegisterScanner<T>(DiagnosticScanner<T> scanner)
        where T : Snapshot => scanner is not null && _scannerCache.TryAdd(typeof(T).FullName, scanner);

    public bool TryRegisterAllProbes()
    {
        Throw.IfInvalid(_config.Diagnostics);

        bool registered = GetType()
            .Assembly
            .GetTypes()
            .Where(x => typeof(DiagnosticProbe).IsAssignableFrom(x) && !x.IsInterface)
            .ToList()
            .GetTypeMap(GetProbeKey)
            .TryRegisterAll(_probeCache, RegisterInstance, CreateProbeInstance);

        if (!registered)
            _probeCache.Clear();

        return registered;
    }

    public bool TryRegisterAllScanners()
    {
        bool registered = GetType()
            .Assembly
            .GetTypes()
            .Where(x => x.InheritsFromInterface(typeof(DiagnosticScanner<>)) && x.IsDerivedFrom(typeof(BaseDiagnosticScanner)) && x != typeof(BaseDiagnosticScanner))
            .ToList()
            .GetTypeMap(GetScannerKey)
            .TryRegisterAll(_scannerCache, RegisterInstance, CreateScannerInstance);

        if (!registered)
            _scannerCache.Clear();

        return registered;
    }

    protected virtual bool RegisterInstance<T>(Type type, string key, Func<Type, T> createInstance, Func<string, T, bool> cache)
    {
        try
        {
            var instance = createInstance(type);

            return instance is not null && cache(key, instance);
        }
        catch
        {
            return false;
        }
    }

    protected virtual object CreateScannerInstance(Type type) => Activator.CreateInstance(type, _probeCache.Values.ToList());

    DiagnosticProbe CreateProbeInstance(Type type) =>
        type.GetConstructors()[0].GetParameters()[0].ParameterType == typeof(DiagnosticsConfig)
        && type.GetConstructors()[0].GetParameters()[1].ParameterType == typeof(IKnowledgeBaseProvider)
            ? Activator.CreateInstance(type, _config.Diagnostics, _kb) as DiagnosticProbe
            : Activator.CreateInstance(type, _kb) as DiagnosticProbe;

    void Configure(string key)
    {
        if (!_scannerCache.TryGetValue(key, out var scanner))
            return;

        var method = scanner
            .GetType()
            .GetMethod("Configure");

        if (method != null)
            method.Invoke(scanner, [_probeCache.Values]);
    }

    string GetProbeKey(Type type) => type?.FullName;

    string GetScannerKey(Type type)
    {
        if (type is null)
            return null;

        if (!type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(DiagnosticScanner<>)))
            return null;

        var genericType = type
            .GetInterfaces()
            .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(DiagnosticScanner<>));

        return genericType?.GetGenericArguments()[0].FullName;
    }
}