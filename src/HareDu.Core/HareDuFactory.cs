namespace HareDu.Core;

using System;
using CommunityToolkit.Diagnostics;
using Extensions;
using HTTP;
using Security;
using Serialization;

/// <summary>
/// Represents a factory that provides APIs to interact with the broker.
/// </summary>
public sealed class HareDuFactory :
    BaseHareDuFactory,
    IHareDuFactory
{
    readonly IHareDuClient _client;
    private readonly IHareDuDeserializer _deserializer;

    public HareDuFactory(IHareDuClient client, IHareDuDeserializer deserializer)
    {
        Guard.IsNotNull(client);

        _client = client;
        _deserializer = deserializer;
    }

    public T API<T>(Action<HareDuCredentialProvider> credentials)
        where T : HareDuAPI
    {
        Type type = typeof(T);

        Throw.IfNull<Type, HareDuInitException>(type, $"Failed to find implementation for interface {type}.");

        var implMap = GetImplMap(type, typeof(HareDuAPI));
        string key = type.GetIdentifier();

        Throw.IfNotFound<HareDuInitException>(implMap.ContainsKey, key, $"Failed to find implementation for interface {type}.");

        var client = _client.GetClient(credentials);
        (bool success, object impl) = TryGetImpl(implMap[key], typeof(BaseHareDuImpl), key, client, _deserializer);

        if (success)
            return (T) impl;
        // if (TryGetImpl(implMap[key], typeof(BaseBrokerImpl), key, client, out var impl))
        //     return (T) impl;

        throw new HareDuInitException($"Failed to find implementation for interface {type}.");
    }
}