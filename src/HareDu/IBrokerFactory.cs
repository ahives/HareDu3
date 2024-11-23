namespace HareDu;

using System;
using System.Collections.Generic;

public interface IBrokerFactory
{
    /// <summary>
    /// Creates a new instance of object implemented by T, which encapsulates a group of resources (e.g. Virtual Host, Exchange, Queue, User, etc.)
    /// that are exposed by the RabbitMQ server via its REST API.
    /// </summary>
    /// <typeparam name="T">Interface that derives from base interface ResourceClient.</typeparam>
    /// <returns>An interface of resources available on a RabbitMQ server.</returns>
    T API<T>()
        where T : BrokerAPI;

    /// <summary>
    /// Obsolete. Use API<T> instead.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [Obsolete]
    T Object<T>()
        where T : BrokerAPI;

    /// <summary>
    /// Returns true if the broker object was registered.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool IsRegistered(string key);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IReadOnlyDictionary<string, object> GetObjects();
        
    /// <summary>
    /// Cancel pending running thread.
    /// </summary>
    void CancelPendingRequest();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    bool TryRegisterAll();
}