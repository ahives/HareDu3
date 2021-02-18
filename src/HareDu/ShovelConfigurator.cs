namespace HareDu
{
    using System;

    public interface ShovelConfigurator
    {
        void ReconnectDelay(int delay);

        void AcknowledgeMode(AcknowledgeMode mode);
        
        void Source(string queue, string uri, Action<ShovelSourceConfigurator> configurator = null);
        
        void Destination(string queue, string uri, Action<ShovelDestinationConfigurator> configurator = null);
    }
}