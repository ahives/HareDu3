namespace HareDu
{
    public interface ShovelSourceConfigurator
    {
        void Protocol(Protocol protocol);

        void DeleteAfter();

        void MaxCopiedMessages(long messages);

        void Exchange(string exchange, string routingKey);
    }
}