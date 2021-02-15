namespace HareDu
{
    using System;

    public interface ShovelTargetConfiguration
    {
        void Name(string name);

        void ReconnectDelay(int delay);

        void AcknowledgeMode(AcknowledgeMode mode);
        
        void Source(Action<ShovelSourceConfiguration> definition);
        
        void Destination(Action<ShovelDestinationConfiguration> definition);
    }
}