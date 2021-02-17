namespace HareDu
{
    using System;

    public interface DeleteBindingConfiguration
    {
        /// <summary>
        /// Specify the source, destination, and binding type.
        /// </summary>
        /// <param name="configurator"></param>
        void Configure(Action<DeleteBindingConfigurator> configurator);

        /// <summary>
        /// Specify the target for which the binding will be deleted.
        /// </summary>
        /// <param name="target">Define the location where the binding (i.e. virtual host) will be deleted</param>
        void Targeting(Action<BindingTarget> target);
    }
}