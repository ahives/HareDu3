namespace HareDu
{
    using System;

    public interface NewGlobalParameterConfiguration
    {
        /// <summary>
        /// Specify the name of the global parameter.
        /// </summary>
        /// <param name="name"></param>
        void Parameter(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurator"></param>
        void Configure(Action<NewGlobalParameterConfigurator> configurator);
    }
}