namespace HareDu;

/// <summary>
/// Specifies the various routing types that can be used for an exchange in a messaging system.
/// Routing types determine how messages are delivered to queues bound to the exchange.
/// </summary>
public enum RoutingType
{
    /// <summary>
    /// Represents the "Fanout" routing type for an exchange in a messaging system.
    /// When this routing type is used, messages published to the exchange are broadcast to all bound queues
    /// regardless of binding keys or other criteria.
    /// </summary>
    Fanout,

    /// <summary>
    /// Represents the "Direct" routing type for an exchange in a messaging system.
    /// When this routing type is used, messages are routed to queues based on an exact match between
    /// the routing key of the message and the binding key of the queue.
    /// </summary>
    Direct,

    /// <summary>
    /// Represents the "Topic" routing type for an exchange in a messaging system.
    /// This routing type routes messages to queues based on pattern matching between a routing key
    /// and the pattern specified in the queue bindings. Patterns can include wildcard characters
    /// to allow flexible matching.
    /// </summary>
    Topic,

    /// <summary>
    /// Represents the "Headers" routing type for an exchange in a messaging system.
    /// This routing type allows messages to be routed based on matching header attributes
    /// using a key-value mapping provided in the message and binding headers.
    /// </summary>
    Headers,

    /// <summary>
    /// Represents the "Federated" routing type for an exchange in a messaging system.
    /// When this routing type is used, messages are forwarded between exchanges in different brokers,
    /// enabling communication across distributed systems or federated setups.
    /// </summary>
    Federated,

    /// <summary>
    /// Represents the "Match" routing type for an exchange in a messaging system.
    /// When this routing type is used, messages are routed based on custom criteria defined within the system,
    /// allowing for flexible and dynamic message distribution logic.
    /// </summary>
    Match
}