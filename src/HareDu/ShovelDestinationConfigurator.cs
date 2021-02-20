namespace HareDu
{
    public interface ShovelDestinationConfigurator
    {
        void Protocol(ShovelProtocolType protocol);

        void Exchange(string exchange, string routingKey);

        void AddForwardHeaders();
        
        void AddTimestampHeaderToMessage();
    }
}