namespace HareDu;

using System;

public interface OperatorPolicyConfigurator
{
    /// <summary>
    /// Defines the policy.
    /// </summary>
    /// <param name="configurator"></param>
    void Definition(Action<OperatorPolicyArgumentConfigurator> configurator);

    /// <summary>
    /// The pattern to apply the policy on.
    /// </summary>
    /// <param name="pattern"></param>
    void Pattern(string pattern);

    /// <summary>
    /// The policy's priority.
    /// </summary>
    /// <param name="priority"></param>
    void Priority(int priority);

    /// <summary>
    /// The broker object for which the policy is to be applied to.
    /// </summary>
    /// <param name="applyTo"></param>
    void ApplyTo(OperatorPolicyAppliedTo applyTo);
}