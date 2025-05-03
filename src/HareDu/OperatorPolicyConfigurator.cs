namespace HareDu;

using System;

/// <summary>
/// Provides methods for configuring an operator policy in RabbitMQ.
/// </summary>
public interface OperatorPolicyConfigurator
{
    /// <summary>
    /// Defines specific operator policy arguments to be used when creating or configuring an operator policy.
    /// </summary>
    /// <param name="configurator">Action used to configure the operator policy arguments.</param>
    void Definition(Action<OperatorPolicyArgumentConfigurator> configurator);

    /// <summary>
    /// Sets the pattern used for matching resources when applying the operator policy.
    /// </summary>
    /// <param name="pattern">The pattern to match resources.</param>
    void Pattern(string pattern);

    /// <summary>
    /// Sets the priority of the operator policy.
    /// </summary>
    /// <param name="priority">The priority level to assign to the operator policy.</param>
    void Priority(int priority);

    /// <summary>
    /// Specifies the target entity on which the operator policy should be applied.
    /// </summary>
    /// <param name="applyTo">The entity type to which the operator policy will be applied.</param>
    void ApplyTo(OperatorPolicyAppliedTo applyTo);
}