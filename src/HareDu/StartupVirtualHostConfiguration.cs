namespace HareDu
{
    using System;

    public interface StartupVirtualHostConfiguration
    {
        /// <summary>
        /// Specify the name of the virtual host.
        /// </summary>
        /// <param name="name">RabbitMQ virtual host name</param>
        void VirtualHost(string name);

        /// <summary>
        /// Specify the target for which the exchange will be created.
        /// </summary>
        /// <param name="target">Define the location of the exchange (i.e. virtual host) that is targeted for deletion</param>
        void Targeting(Action<StartupVirtualHostTarget> target);
    }
}