namespace HareDu;

using System.Collections.Generic;

public interface IBrokerFactory
{
    /// <summary>
    /// Creates a new instance of object implemented by T, which encapsulates a group of resources (e.g. Virtual Host, Exchange, Queue, User, etc.)
    /// that are exposed by the RabbitMQ server via its REST API.
    /// </summary>
    /// <typeparam name="T">Interface that derives from base interface ResourceClient.</typeparam>
    /// <returns>An interface of resources available on a RabbitMQ server.</returns>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a channel.</exception>
    T API<T>()
        where T : BrokerAPI;
}