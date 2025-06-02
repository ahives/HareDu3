namespace HareDu.Snapshotting;

using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Extensions;
using Lens;
using Model;
using HareDu.Snapshotting.Lens.Internal;

/// <summary>
/// A factory class for creating and managing instances of snapshots and their corresponding lenses.
/// Provides methods to retrieve or register a lens implementation for a specific snapshot type.
/// </summary>
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

        Throw.IfNull<Type, HareDuInitException>(type, $"Failed to find implementation for interface {type}.");

        var typeMap = GetTypeMap(GetType());
        string key = type.GetIdentifier();

        return TryGetInstance(typeMap[key], typeof(BaseLens<>), key, _factory, out var instance)
            ? instance as Lens<T>
            : new NoOpLens<T>();
    }

    public ISnapshotFactory Register<T>(Lens<T> lens)
        where T : Snapshot
    {
        Type type = typeof(T);

        string key = type.GetIdentifier();

        if (Cache.ContainsKey(key))
            return this;

        Cache.TryAdd(key, lens);

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

            interfaces.TryAdd(type.GetIdentifier(), type);
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