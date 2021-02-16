namespace HareDu
{
    using System;

    public interface NewBindingConfiguration
    {
        /// <summary>
        /// Specify how the binding should be configured.
        /// </summary>
        /// <param name="criteria"></param>
        void Configure(Action<NewBindingCriteria> criteria);

        /// <summary>
        /// Specify the target for which the binding will be created.
        /// </summary>
        /// <param name="target">Define the location where the binding (i.e. virtual host) will be created</param>
        void Targeting(Action<BindingTarget> target);
    }
}