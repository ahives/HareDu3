namespace HareDu.Core;

using System;
using Extensions;
using HTTP;
using Security;

/// <summary>
/// Represents a factory that provides APIs to interact with the broker.
/// </summary>
public sealed class HareDuFactory :
    BaseHareDuFactory,
    IHareDuFactory
{
    readonly IHareDuClient _client;

    public HareDuFactory(IHareDuClient client)
    {
        Throw.IfNull<HareDuInitException>(client, "Failed to initialize HareDu API.");
 
        _client = client;
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
        (bool success, object impl) = TryGetImpl(implMap[key], typeof(BaseHareDuImpl), key, client);

        if (success)
            return (T) impl;

        throw new HareDuInitException($"Failed to find implementation for interface {type}.");
    }
}