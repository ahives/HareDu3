namespace HareDu
{
    public interface ShovelSourceConfiguration
    {
        void Protocol(Protocol protocol);

        void Uri(string uri);

        void Queue(string queue);

        void DeleteAfter();

        void MaxCopiedMessages(long messages);

        void Exchange(string exchange, string routingKey);
    }
}