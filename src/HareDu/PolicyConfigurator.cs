namespace HareDu;

using System;
using System.Diagnostics.CodeAnalysis;
using Model;

/// <summary>
/// Represents a configuration interface for defining policies in a message broker.
/// </summary>
public interface PolicyConfigurator
{
    /// <summary>
    /// Configures the definition of a policy for a message broker by specifying its arguments.
    /// </summary>
    /// <param name="configurator">Action delegate that allows configuring the policy arguments using the provided PolicyArgumentConfigurator object.</param>
    void Definition([NotNull] Action<PolicyArgumentConfigurator> configurator);

    /// <summary>
    /// Specifies the matching expression used by the policy to determine which entities in the message broker it applies to.
    /// </summary>
    /// <param name="pattern">The string pattern used to match entities such as queues or exchanges.</param>
    void Pattern([NotNull] string pattern);

    /// <summary>
    /// Specifies the priority level for the policy.
    /// </summary>
    /// <param name="priority">The priority value to be assigned to the policy, where a lower value indicates a higher priority.</param>
    void Priority(int priority);

    /// <summary>
    /// Specifies the type of broker object, such as Queues or Exchanges, to which the policy should be applied.
    /// </summary>
    /// <param name="applyTo">The target broker object to which the policy is applied. Must be one of the values defined in the PolicyAppliedTo enumeration.</param>
    void ApplyTo(PolicyAppliedTo applyTo);
}