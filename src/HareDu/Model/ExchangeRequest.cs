namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the request object for configuring and creating a RabbitMQ exchange.
/// </summary>
public record ExchangeRequest
{
    /// <summary>
    /// Specifies the routing type used when creating or configuring an exchange in a message broker.
    /// </summary>
    /// <remarks>
    /// The routing type determines how messages are routed from the exchange to the bound queues. Various routing types include:
    /// Fanout, Direct, Topic, Headers, Federated, and Match.
    /// </remarks>
    [JsonPropertyName("type")]
    public RoutingType RoutingType { get; init; }

    /// <summary>
    /// Indicates whether the exchange is durable, meaning that it will survive a broker restart.
    /// </summary>
    /// <remarks>
    /// A durable exchange ensures that it remains available even after the broker is restarted.
    /// Non-durable exchanges will be removed once the broker stops.
    /// This property is typically used for exchanges that need to persist across restarts for reliable messaging.
    /// </remarks>
    [JsonPropertyName("durable")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Durable { get; init; }

    /// <summary>
    /// Indicates whether the exchange should be automatically deleted when it is no longer in use.
    /// </summary>
    /// <remarks>
    /// When this property is set to true, the exchange will be deleted by the message broker once there are no more queues bound to it.
    /// If set to false, the exchange will persist regardless of its bindings.
    /// </remarks>
    [JsonPropertyName("auto_delete")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool AutoDelete { get; init; }

    /// <summary>
    /// Indicates whether the exchange is an internal exchange.
    /// </summary>
    /// <remarks>
    /// Internal exchanges are not directly exposed to publishers. Instead, they are used for routing messages solely between exchanges within the broker.
    /// </remarks>
    [JsonPropertyName("internal")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Internal { get; init; }

    /// <summary>
    /// Represents a collection of user-defined key/value pairs used to specify additional configuration options or metadata when creating or configuring an exchange.
    /// </summary>
    /// <remarks>
    /// The content of this dictionary can include various custom settings or optional parameters that influence the behavior or attributes of the exchange.
    /// These arguments are flexible and depend on the specific requirements of the messaging broker being used.
    /// </remarks>
    [JsonPropertyName("arguments")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IDictionary<string, object> Arguments { get; init; }
}