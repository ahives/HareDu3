namespace HareDu.Diagnostics;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Core.Configuration;
using KnowledgeBase;
using Probes;
using Extensions;
using Scanners;
using HareDu.Snapshotting.Model;
using HareDu.Core.Extensions;

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

        Span<DiagnosticProbe> memoryFrames = CollectionsMarshal.AsSpan(_probeCache.Values.ToList());
        ref var ptr = ref MemoryMarshal.GetReference(memoryFrames);

        for (int i = 0; i < memoryFrames.Length; i++)
        {
            var probe = Unsafe.Add(ref ptr, i);

            _observers.Add(probe.Subscribe(observer));
        }
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
        where T : Snapshot =>
        scanner is not null && _scannerCache.TryAdd(typeof(T).FullName, scanner);

    public bool TryRegisterAllProbes()
    {
        bool registered = GetType()
            .Assembly
            .GetTypes()
            .Where(x => typeof(DiagnosticProbe).IsAssignableFrom(x) && !x.IsInterface)
            .ToList()
            .GetTypeMap(GetProbeKey)
            .TryRegisterAll(_probeCache, RegisterProbeInstance);

        if (!registered)
            _probeCache.Clear();

        return registered;
    }

    public bool TryRegisterAllScanners()
    {
        bool registered = GetType()
            .Assembly
            .GetTypes()
            .Where(x => typeof(DiagnosticScanner<>).IsAssignableFrom(x) && !x.IsGenericType)
            .ToList()
            .GetTypeMap(GetScannerKey)
            .TryRegisterAll(_scannerCache, RegisterScannerInstance);

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

    void Configure(string key)
    {
        if (!_scannerCache.TryGetValue(key, out var scanner))
            return;
        
        var method = scanner
            .GetType()
            .GetMethod("Configure");
        
        if (method != null)
            method.Invoke(scanner, new[] {_probeCache.Values});
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

        if (genericType is null)
            return null;

        return genericType.GetGenericArguments()[0].FullName;
    }
}