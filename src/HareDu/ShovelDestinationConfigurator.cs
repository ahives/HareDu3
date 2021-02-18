namespace HareDu
{
    public interface ShovelDestinationConfigurator
    {
        void Protocol(Protocol protocol);

        void Exchange(string exchange, string routingKey);

        void AddForwardHeaders();
        
        void AddTimestampHeaderToMessage();
    }
}