namespace HareDu;

using System;

public interface PolicyConfigurator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configurator"></param>
    void Definition(Action<PolicyArgumentConfigurator> configurator);

    /// <summary>
    /// The pattern to apply the policy on.
    /// </summary>
    /// <param name="pattern"></param>
    void Pattern(string pattern);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="priority"></param>
    void Priority(int priority);

    /// <summary>
    /// The broker object for which the policy is to be applied to.
    /// </summary>
    /// <param name="applyTo"></param>
    void ApplyTo(PolicyAppliedTo applyTo);
}