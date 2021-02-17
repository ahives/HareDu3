namespace HareDu
{
    using System;

    public interface NewPolicyConfiguration
    {
        /// <summary>
        /// Specify the name of the policy.
        /// </summary>
        /// <param name="name"></param>
        void Policy(string name);
        
        /// <summary>
        /// Specify how the policy should be configured.
        /// </summary>
        /// <param name="configurator">User-defined configuration</param>
        void Configure(Action<PolicyConfigurator> configurator);

        /// <summary>
        /// Specify what virtual host will the policy be scoped to.
        /// </summary>
        /// <param name="target">Define where the policy will live</param>
        void Targeting(Action<PolicyTarget> target);
    }
}