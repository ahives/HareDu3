namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the details of an operator policy in RabbitMQ broker.
/// </summary>
public record OperatorPolicyInfo
{
    /// <summary>
    /// Represents the name of the virtual host to which the operator policy applies.
    /// </summary>
    [JsonPropertyName("vhost")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string VirtualHost { get; init; }

    /// <summary>
    /// Represents the name of the operator policy.
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Name { get; init; }

    /// <summary>
    /// Represents the pattern used by the operator policy to match the target resources.
    /// </summary>
    [JsonPropertyName("pattern")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Pattern { get; init; }

    /// <summary>
    /// Specifies the type of entity (e.g., queues) to which the operator policy is applied.
    /// </summary>
    [JsonPropertyName("apply-to")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public OperatorPolicyAppliedTo AppliedTo { get; init; }

    /// <summary>
    /// Represents a collection of key-value pairs defining specific parameters or rules
    /// associated with the operator policy.
    /// </summary>
    [JsonPropertyName("definition")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IDictionary<string, ulong> Definition { get; init; }

    /// <summary>
    /// Represents the priority level assigned to the operator policy, which determines the order of policy execution.
    /// </summary>
    [JsonPropertyName("priority")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Priority { get; init; }
}