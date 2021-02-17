namespace HareDu
{
    using System;

    public interface ShovelConfiguration
    {
        void ReconnectDelay(int delay);

        void AcknowledgeMode(AcknowledgeMode mode);
        
        void Source(string queue, string uri, Action<ShovelSourceConfiguration> configurator = null);
        
        void Destination(string queue, string uri, Action<ShovelDestinationConfiguration> configurator = null);
    }
}