namespace HareDu;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CommunityToolkit.Diagnostics;
using Core.Extensions;
using Core.Security;
using HTTP;
using Internal;

public sealed class BrokerFactory :
    IBrokerFactory
{
    readonly IHareDuClient _client;
    readonly ConcurrentDictionary<string, object> _cache;

    public BrokerFactory(IHareDuClient client)
    {
        Guard.IsNotNull(client);

        _client = client;

        _cache = new ConcurrentDictionary<string, object>();
    }

    public T API<T>(Action<HareDuCredentialProvider> credentials)
        where T : BrokerAPI
    {
        Type type = typeof(T);

        if (type is null)
            throw new HareDuBrokerInitException($"Failed to find implementation class for interface {typeof(T)}");

        var typeMap = GetTypeMap(typeof(T));

        if (!typeMap.ContainsKey(type.FullName ?? throw new HareDuBrokerInitException($"Failed to find implementation class for interface {typeof(T)}")))
            return default;

        if (_cache.TryGetValue(type.FullName, out var value))
            return (T) value;

        var client = _client.CreateClient(credentials);
        bool registered = RegisterInstance(typeMap[type.FullName], type.FullName, client);

        if (registered)
            return (T) _cache[type.FullName];

        return default;
    }

    bool RegisterInstance(Type type, string key, HttpClient client)
    {
        try
        {
            var instance = CreateInstance(type, client);

            return instance is not null && _cache.TryAdd(key, instance);
        }
        catch
        {
            return false;
        }
    }

    IDictionary<string, Type> GetTypeMap(Type findType)
    {
        var types = findType.Assembly.GetTypes();
        var interfaces = types
            .Where(x => typeof(BrokerAPI).IsAssignableFrom(x) && x.IsInterface)
            .ToList();
        var typeMap = new Dictionary<string, Type>();
    
        for (int i = 0; i < interfaces.Count; i++)
        {
            var type = types.Find(x => interfaces[i].IsAssignableFrom(x) && x is {IsInterface: false, IsAbstract: false});
    
            if (type is null)
                continue;
    
            string name = interfaces[i].FullName;
            if (string.IsNullOrWhiteSpace(name))
                continue;
    
            typeMap.Add(name, type);
        }
    
        return typeMap;
    }

    object CreateInstance(Type type, HttpClient client) =>
        type.IsDerivedFrom(typeof(BaseBrokerImpl))
        ? Activator.CreateInstance(type, client)
        : Activator.CreateInstance(type);
}