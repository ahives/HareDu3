namespace HareDu
{
    public interface ShovelDestinationConfiguration
    {
        void Protocol(Protocol protocol);

        void Uri(string uri);

        void Queue(string queue);

        void Exchange(string exchange, string routingKey);

        void AddForwardHeaders();
        
        void AddTimestampHeaderToMessage();
    }
}