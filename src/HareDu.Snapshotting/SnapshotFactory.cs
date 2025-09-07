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
    BaseHareDuFactory,
    ISnapshotFactory
{
    readonly IHareDuFactory _factory;

    public SnapshotFactory(IHareDuFactory factory)
    {
        _factory = factory;

        RegisterAll();
    }

    public Lens<T> Lens<T>()
        where T : Snapshot
    {
        Type type = typeof(T);

        Throw.IfNull<Type, HareDuInitException>(type, $"Failed to find implementation for interface {type}.");

        var implMap = GetImplMap(type, typeof(Snapshot));
        string key = type.GetIdentifier();

        return TryGetImpl(implMap[key], typeof(BaseLens<>), key, _factory, out var impl)
            ? impl as Lens<T>
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
        var implMap = GetImplMap(GetType(), typeof(Snapshot));
        bool registered = true;

        foreach (var type in implMap)
            registered = TryGetImpl(type.Value, typeof(BaseLens<>), type.Key, _factory, out _) & registered;

        if (!registered)
            Cache.Clear();
    }

    protected override IDictionary<string, Type> GetImplMap(Type findType, Type from)
    {
        var types = findType.Assembly.GetTypes();
        var interfaces = findType.Assembly
            .GetTypes()
            .Where(x => !x.IsInterface && x.InheritsFromInterface(from))
            .ToArray();

        var implMap = new Dictionary<string, Type>();

        for (int i = 0; i < interfaces.Length; i++)
        {
            for (int j = 0; j < types.Length; j++)
            {
                if (types[j].IsInterface)
                    continue;

                if (types[j].GetInterfaces().ImplementsInterface(Type.GetType($"{typeof(Lens<>).FullName}[{interfaces[i].FullName}]")))
                    implMap.Add(interfaces[i].FullName.GetIdentifier(), types[j]);
            }
        }

        return implMap;
    }
}