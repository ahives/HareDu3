namespace HareDu;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Core.Configuration;
using Core.Extensions;
using Internal;

public sealed class BrokerObjectFactory :
    IBrokerObjectFactory
{
    readonly HttpClient _client;
    readonly ConcurrentDictionary<string, object> _cache;

    public BrokerObjectFactory(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _cache = new ConcurrentDictionary<string, object>();
            
        if (!TryRegisterAll())
            throw new HareDuBrokerObjectInitException("Could not register broker objects.");
    }

    public BrokerObjectFactory(HareDuConfig config)
    {
        _client = GetClient(config);
        _cache = new ConcurrentDictionary<string, object>();
            
        if (!TryRegisterAll())
            throw new HareDuBrokerObjectInitException("Could not register broker objects.");
    }

    public T Object<T>()
        where T : BrokerObject
    {
        Type type = typeof(T);
            
        if (type is null)
            throw new HareDuBrokerObjectInitException($"Failed to find implementation class for interface {typeof(T)}");

        var typeMap = GetTypeMap(typeof(T));

        if (!typeMap.ContainsKey(type.FullName))
            return default;
            
        if (_cache.ContainsKey(type.FullName))
            return (T) _cache[type.FullName];
                
        bool registered = RegisterInstance(typeMap[type.FullName], type.FullName, _client);

        if (registered)
            return (T) _cache[type.FullName];

        return default;
    }

    public bool IsRegistered(string key) => _cache.ContainsKey(key);
        
    public IReadOnlyDictionary<string, object> GetObjects() => _cache;

    public void CancelPendingRequest() => _client.CancelPendingRequests();

    public bool TryRegisterAll()
    {
        var typeMap = GetTypeMap(GetType());
        bool registered = true;

        foreach (var type in typeMap)
        {
            if (_cache.ContainsKey(type.Key))
                continue;
                
            registered = RegisterInstance(type.Value, type.Key) & registered;
        }

        if (!registered)
            _cache.Clear();

        return registered;
    }

    HttpClient GetClient(HareDuConfig config)
    {
        var uri = new Uri($"{config.Broker.Url}/");
        var handler = new HttpClientHandler
        {
            Credentials = new NetworkCredential(config.Broker.Credentials.Username, config.Broker.Credentials.Password)
        };
            
        var client = new HttpClient(handler){BaseAddress = uri};
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (config.Broker.Timeout != TimeSpan.Zero)
            client.Timeout = config.Broker.Timeout;

        return client;
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

    bool RegisterInstance(Type type, string key)
    {
        try
        {
            var instance = CreateInstance(type);

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
            .Where(x => typeof(BrokerObject).IsAssignableFrom(x) && x.IsInterface)
            .ToList();
        var typeMap = new Dictionary<string, Type>();

        for (int i = 0; i < interfaces.Count; i++)
        {
            var type = types.Find(x => interfaces[i].IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            if (type is null)
                continue;
                
            typeMap.Add(interfaces[i].FullName, type);
        }

        return typeMap;
    }

    object CreateInstance(Type type) =>
        type.IsDerivedFrom(typeof(BaseBrokerObject))
        ? Activator.CreateInstance(type, _client)
        : Activator.CreateInstance(type);

    object CreateInstance(Type type, HttpClient client) => Activator.CreateInstance(type, client);
}