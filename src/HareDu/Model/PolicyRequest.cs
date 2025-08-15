namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a policy request in the RabbitMQ broker. A policy is defined with a pattern, arguments,
/// priority, and a target type, specifying how the policy is applied to exchanges or queues.
/// </summary>
public record PolicyRequest
{
    /// <summary>
    /// Represents the pattern to which the RabbitMQ policy should be applied.
    /// Patterns are typically used to match queue or exchange names based on the given regular expression.
    /// </summary>
    [JsonPropertyName("pattern")]
    public string Pattern { get; init; }

    /// <summary>
    /// Represents a collection of key-value pairs that define additional parameters
    /// or settings applied to a RabbitMQ policy. The keys and values typically specify
    /// custom arguments or configuration options used by the RabbitMQ server.
    /// </summary>
    [JsonPropertyName("definition")]
    public IDictionary<string, string> Arguments { get; init; }

    /// <summary>
    /// Specifies the priority of the RabbitMQ policy.
    /// Policies with higher priority values are applied first.
    /// </summary>
    [JsonPropertyName("priority")]
    public int Priority { get; init; }

    /// <summary>
    /// Specifies the type of entity (e.g., all, exchanges, or queues) to which the RabbitMQ policy will be applied.
    /// This determines the scope of the policy within the RabbitMQ system.
    /// </summary>
    [JsonPropertyName("apply-to")]
    public PolicyAppliedTo ApplyTo { get; init; }
}