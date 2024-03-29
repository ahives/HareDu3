namespace HareDu.Diagnostics;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Core.Configuration;
using KnowledgeBase;
using Probes;
using Scanners;
using HareDu.Snapshotting.Model;

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

        if (!_scannerCache.ContainsKey(type.FullName))
        {
            scanner = new NoOpScanner<T>();
            return false;
        }

        scanner = (DiagnosticScanner<T>) _scannerCache[type.FullName];
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

        var probes = _probeCache.Values.ToList();

        for (int i = 0; i < probes.Count; i++)
            _observers.Add(probes[i].Subscribe(observer));
    }

    public bool TryRegisterProbe<T>(T probe)
        where T : DiagnosticProbe
    {
        bool probeAdded = _probeCache.TryAdd(typeof(T).FullName, probe);

        if (probe is null || !probeAdded)
            return false;

        foreach (var scanner in _scannerCache)
        {
            var method = scanner.Value
                .GetType()
                .GetMethod("Configure");

            if (method != null)
                method.Invoke(scanner.Value, new[] {_probeCache.Values});
        }

        return probeAdded;
    }

    public bool TryRegisterScanner<T>(DiagnosticScanner<T> scanner)
        where T : Snapshot =>
        scanner is not null && _scannerCache.TryAdd(typeof(T).FullName, scanner);

    public bool TryRegisterAllProbes()
    {
        var typeMap = GetProbeTypeMap(GetType());
        bool registered = true;

        foreach (var type in typeMap)
        {
            if (_probeCache.ContainsKey(type.Key))
                continue;

            registered = RegisterProbeInstance(type.Value, type.Key) & registered;
        }

        if (!registered)
            _probeCache.Clear();

        return registered;
    }

    public bool TryRegisterAllScanners()
    {
        var typeMap = GetScannerTypeMap(GetType());
        bool registered = true;

        foreach (var type in typeMap)
        {
            if (_scannerCache.ContainsKey(type.Key))
                continue;

            registered = RegisterScannerInstance(type.Value, type.Key) & registered;
        }

        if (!registered)
            _scannerCache.Clear();

        return registered;
    }

    protected virtual bool RegisterScannerInstance(Type type, string key)
    {
        try
        {
            var instance = CreateScannerInstance(type);

            return instance is not null && _scannerCache.TryAdd(key, instance);
        }
        catch
        {
            return false;
        }
    }

    protected virtual object CreateScannerInstance(Type type) => Activator.CreateInstance(type, _probeCache.Values.ToList());

    protected virtual IDictionary<string, Type> GetScannerTypeMap(Type findType)
    {
        var types = findType
            .Assembly
            .GetTypes()
            .Where(x => x.IsClass && !x.IsGenericType)
            .ToList();
        var typeMap = new Dictionary<string, Type>();

        foreach (var type in types)
        {
            if (type is null)
                continue;

            if (!type.GetInterfaces()
                    .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(DiagnosticScanner<>)))
                continue;

            var genericType = type
                .GetInterfaces()
                .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(DiagnosticScanner<>));

            if (genericType is null)
                continue;

            string key = genericType.GetGenericArguments()[0].FullName;

            if (key != null && typeMap.ContainsKey(key))
                continue;

            typeMap.Add(key, type);
        }

        return typeMap;
    }

    protected virtual IDictionary<string, Type> GetProbeTypeMap(Type type)
    {
        var types = type
            .Assembly
            .GetTypes()
            .Where(x => typeof(DiagnosticProbe).IsAssignableFrom(x) && !x.IsInterface)
            .ToList();

        var typeMap = new Dictionary<string, Type>();

        for (int i = 0; i < types.Count; i++)
        {
            if (typeMap.ContainsKey(types[i].FullName))
                continue;

            typeMap.Add(types[i].FullName, types[i]);
        }

        return typeMap;
    }

    protected virtual bool RegisterProbeInstance(Type type, string key)
    {
        try
        {
            var instance = CreateProbeInstance(type);

            return instance is not null && _probeCache.TryAdd(key, instance);
        }
        catch
        {
            return false;
        }
    }

    protected virtual DiagnosticProbe CreateProbeInstance(Type type)
    {
        var instance = type.GetConstructors()[0].GetParameters()[0].ParameterType == typeof(DiagnosticsConfig)
                       && type.GetConstructors()[0].GetParameters()[1].ParameterType == typeof(IKnowledgeBaseProvider)
            ? Activator.CreateInstance(type, _config.Diagnostics, _kb) as DiagnosticProbe
            : Activator.CreateInstance(type, _kb) as DiagnosticProbe;

        return instance;
    }
}