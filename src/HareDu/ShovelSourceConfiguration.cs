namespace HareDu
{
    public interface ShovelSourceConfiguration
    {
        void Protocol(Protocol protocol);

        void DeleteAfter();

        void MaxCopiedMessages(long messages);

        void Exchange(string exchange, string routingKey);
    }
}