namespace HareDu.Core.Extensions;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;

public static class TypeExtensions
{
    public static bool IsDerivedFrom(this Type type, Type fromType)
    {
        while (type is not null && type != typeof(object))
        {
            Type currentType = type.IsGenericType ? type.GetGenericTypeDefinition() : type;

            if (fromType == currentType)
                return true;

            type = type.BaseType;
        }

        return false;
    }

    public static Type Find(this Type[] types, Predicate<Type> predicate) => Array.Find(types, predicate);

    public static bool InheritsFromInterface(this Type type, Type findType)
    {
        return findType.IsGenericType
            ? type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == findType)
            : type.GetInterfaces().Any(x => x == findType);
    }

    public static Dictionary<string, Type> GetTypeMap(this List<Type> types, Func<Type, string> function)
    {
        var map = new Dictionary<string, Type>();

        foreach (var type in types)
        {
            string key = function(type);

            if (key is null || !map.TryAdd(key, type))
                continue;
        }

        return map;
    }

    public static bool TryRegisterAll<T>(this IDictionary<string, Type> map, ConcurrentDictionary<string, T> cache, Func<Type, string, bool> function)
    {
        bool registered = true;

        foreach (var key in map.Keys.ToList())
        {
            if (cache.ContainsKey(key) || !map.TryGetValue(key, out Type type))
                continue;

            registered &= function(type, key);
        }

        return registered;
    }
}