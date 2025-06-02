namespace HareDu;

using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Extensions;
using Core.Security;
using HTTP;
using Internal;

/// <summary>
/// Represents a factory that provides APIs to interact with the broker.
/// </summary>
public sealed class BrokerFactory :
    HareDuFactory,
    IBrokerFactory
{
    readonly IHareDuClient _client;

    public BrokerFactory(IHareDuClient client)
    {
        Guard.IsNotNull(client);

        _client = client;
    }

    public T API<T>(Action<HareDuCredentialProvider> credentials)
        where T : BrokerAPI
    {
        Type type = typeof(T);

        Throw.IfNull<Type, HareDuInitException>(type, $"Failed to find implementation for interface {type}.");

        var typeMap = GetTypeMap(type);
        string key = type.GetIdentifier();

        Throw.IfNotFound<HareDuInitException>(typeMap.ContainsKey, key, $"Failed to find implementation for interface {type}.");

        var client = _client.GetClient(credentials);

        if (TryGetInstance(typeMap[key], typeof(BaseBrokerImpl), key, client, out var instance))
            return (T) instance;

        throw new HareDuInitException($"Failed to find implementation for interface {type}.");
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

            if (string.IsNullOrWhiteSpace(interfaces[i].FullName))
                continue;

            typeMap.Add(interfaces[i].GetIdentifier(), type);
        }

        return typeMap;
    }
}