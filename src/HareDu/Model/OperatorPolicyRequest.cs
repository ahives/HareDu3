namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a request for defining or configuring an operator policy in a RabbitMQ system.
/// </summary>
public record OperatorPolicyRequest
{
    /// <summary>
    /// Defines the matching criteria for the target elements to which the operator policy should apply.
    /// This field typically contains a regular expression that is evaluated against names such as queue or exchange names
    /// to determine applicability of the policy.
    /// </summary>
    [JsonPropertyName("pattern")]
    public string Pattern { get; init; }

    /// <summary>
    /// Represents a collection of optional key-value pairs used to define specific behavior for an operator policy.
    /// These arguments customize various aspects of the policy's functionality, where the key
    /// is a string, and the value is an unsigned long integer.
    /// </summary>
    [JsonPropertyName("definition")]
    public IDictionary<string, ulong> Arguments { get; init; }

    /// <summary>
    /// Defines the priority level for the operator policy.
    /// This property determines the order in which the policy is applied,
    /// with lower values indicating higher priority.
    /// </summary>
    [JsonPropertyName("priority")]
    public int Priority { get; init; }

    /// <summary>
    /// Specifies the type of RabbitMQ components the operator policy should be applied to.
    /// This property determines the scope of the operator policy, such as queues or other applicable components.
    /// </summary>
    [JsonPropertyName("apply-to")]
    public OperatorPolicyAppliedTo ApplyTo { get; init; }
}