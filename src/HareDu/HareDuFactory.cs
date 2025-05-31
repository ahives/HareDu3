namespace HareDu;

using System;
using System.Collections.Concurrent;
using Core;
using Core.Extensions;

public class HareDuFactory
{
    protected readonly ConcurrentDictionary<string, object> Cache = new();

    protected bool TryGetInstance<T>(Type type, Type from, string key, T initializer, out object instance)
    {
        Throw.IfNull<Type, HareDuBrokerInitException>(type, $"Failed to find implementation for interface {type}.");
        Throw.IfNull<Type, HareDuBrokerInitException>(from, $"Failed to find implementation for interface {from}.");
        Throw.IfNull<string, HareDuBrokerInitException>(key, $"Failed to find implementation for interface {type}.");
        Throw.IfNull<T, HareDuBrokerInitException>(initializer, $"Failed to initialize HareDu API.");

        if (Cache.TryGetValue(key, out instance))
            return true;

        instance = CreateInstance(type, from, initializer);

        if (instance is null)
            return false;

        if (Cache.TryAdd(key, instance))
            return Cache.TryGetValue(key, out instance);

        instance = null;

        return false;
    }

    object CreateInstance<T>(Type type, Type from, T initializer)
    {
        var instance = type.IsDerivedFrom(from)
            ? Activator.CreateInstance(type, initializer)
            : Activator.CreateInstance(type);

        return instance;
    }
}