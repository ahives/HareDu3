namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a request to create a binding between a source and a destination in a messaging system (e.g., RabbitMQ).
/// A binding establishes a relationship between an exchange and a queue or between two exchanges.
/// </summary>
public record BindingRequest
{
    /// <summary>
    /// Represents the routing key used to bind an exchange to a queue or another exchange.
    /// It determines the routing criteria for delivering messages to the specified destination in the binding setup.
    /// </summary>
    [JsonPropertyName("routing_key")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string BindingKey { get; init; }

    /// <summary>
    /// Represents a set of optional key-value pairs (arguments) that can be used to customize or extend the behavior of the binding.
    /// These arguments provide additional details or functionality for the binding configuration process.
    /// </summary>
    [JsonPropertyName("arguments")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IDictionary<string, object> Arguments { get; init; }
}