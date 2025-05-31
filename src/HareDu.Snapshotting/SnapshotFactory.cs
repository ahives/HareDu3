namespace HareDu.Snapshotting;

using System;
using System.Collections.Generic;
using System.Linq;
using Lens;
using Model;
using HareDu.Snapshotting.Lens.Internal;

public sealed class SnapshotFactory :
    HareDuFactory,
    ISnapshotFactory
{
    readonly IBrokerFactory _factory;

    public SnapshotFactory(IBrokerFactory factory)
    {
        _factory = factory;

        RegisterAll();
    }

    public Lens<T> Lens<T>()
        where T : Snapshot
    {
        Type type = typeof(T);

        return TryGetInstance(type, typeof(BaseLens<>), type.FullName, _factory, out var instance)
            ? instance as Lens<T>
            : new NoOpLens<T>();
    }

    public ISnapshotFactory Register<T>(Lens<T> lens)
        where T : Snapshot
    {
        Type type = typeof(T);

        if (!string.IsNullOrWhiteSpace(type.FullName) && Cache.ContainsKey(type.FullName))
            return this;

        Cache.TryAdd(type.FullName, lens);

        return this;
    }

    void RegisterAll()
    {
        var typeMap = GetTypeMap(GetType());
        bool registered = true;

        foreach (var type in typeMap)
            registered = TryGetInstance(type.Value, typeof(BaseLens<>), type.Key, _factory, out _) & registered;

        if (!registered)
            Cache.Clear();
    }

    IDictionary<string, Type> GetTypeMap(Type findType)
    {
        var types = findType.Assembly.GetTypes();
        var interfaces = new Dictionary<string, Type>();

        foreach (var type in types)
        {
            if (!typeof(Snapshot).IsAssignableFrom(type))
                continue;

            interfaces.TryAdd(type.FullName, type);
        }

        var typeMap = new Dictionary<string, Type>();

        foreach (var @interface in interfaces)
        {
            foreach (var type in types)
            {
                if (type.IsInterface)
                    continue;

                if (type.GetInterfaces().Any(x => x == Type.GetType($"{typeof(Lens<>).FullName}[{@interface.Key}]")))
                    typeMap.Add(@interface.Key, type);
            }
        }

        return typeMap;
    }
}