namespace HareDu;

using System;
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

        var implMap = GetImplMap(type, typeof(BrokerAPI));
        string key = type.GetIdentifier();

        Throw.IfNotFound<HareDuInitException>(implMap.ContainsKey, key, $"Failed to find implementation for interface {type}.");

        var client = _client.GetClient(credentials);

        if (TryGetImpl(implMap[key], typeof(BaseBrokerImpl), key, client, out var impl))
            return (T) impl;

        throw new HareDuInitException($"Failed to find implementation for interface {type}.");
    }
}