namespace HareDu.Core;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Extensions;

public class BaseHareDuFactory
{
    protected readonly ConcurrentDictionary<string, object> Cache = new();

    protected virtual IDictionary<string, Type> GetImplMap(Type findType, Type from)
    {
        var types = findType.Assembly.GetTypes();
        var interfaces = types
            .Where(x => from.IsAssignableFrom(x) && x.IsInterface)
            .ToList();
        var typeMap = new Dictionary<string, Type>();

        for (int i = 0; i < interfaces.Count; i++)
        {
            var type = types.Find(x => interfaces[i].IsAssignableFrom(x) && x is {IsInterface: false, IsAbstract: false});

            if (type is null)
                continue;

            if (string.IsNullOrWhiteSpace(interfaces[i].FullName))
                continue;

            typeMap.Add(interfaces[i].GetIdentifier(), type);
        }

        return typeMap;
    }

    protected bool TryGetImpl<T>(Type type, Type from, string key, T initializer, out object impl)
    {
        Throw.IfNull<Type, HareDuInitException>(type, $"Failed to find implementation for interface {type}.");
        Throw.IfNull<Type, HareDuInitException>(from, $"Failed to find implementation for interface {from}.");
        Throw.IfNull<string, HareDuInitException>(key, $"Failed to find implementation for interface {type}.");
        Throw.IfNull<T, HareDuInitException>(initializer, "Failed to initialize HareDu API.");

        if (Cache.TryGetValue(key, out impl))
            return true;

        impl = CreateInstance(type, from, initializer);

        if (impl is null)
            return false;

        if (Cache.TryAdd(key, impl))
            return Cache.TryGetValue(key, out impl);

        impl = null;

        return false;
    }

    protected (bool success, object impl) TryGetImpl(Type type, Type from, string key, params object[] initializers)
    {
        Throw.IfNull<Type, HareDuInitException>(type, $"Failed to find implementation for interface {type}.");
        Throw.IfNull<Type, HareDuInitException>(from, $"Failed to find implementation for interface {from}.");
        Throw.IfNull<string, HareDuInitException>(key, $"Failed to find implementation for interface {type}.");

        for (int i = 0; i < initializers.Length; i++)
            Throw.IfNull<HareDuInitException>(initializers[i], "Failed to initialize HareDu API.");

        if (Cache.TryGetValue(key, out var impl))
            return (true, impl);

        impl = CreateInstance(type, from, initializers);

        if (impl is null)
            return (false, null);

        if (Cache.TryAdd(key, impl))
            return (Cache.TryGetValue(key, out impl), impl);;

        return (false, null);
    }

    object CreateInstance(Type type, Type from, params object[] initializers) =>
        type.IsDerivedFrom(from)
            ? Activator.CreateInstance(type, initializers)
            : null;

    object CreateInstance<T>(Type type, Type from, T initializer)
    {
        var instance = type.IsDerivedFrom(from)
            ? Activator.CreateInstance(type, initializer)
            : Activator.CreateInstance(type);

        return instance;
    }
}