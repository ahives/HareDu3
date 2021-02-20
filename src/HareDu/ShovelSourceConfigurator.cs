namespace HareDu
{
    public interface ShovelSourceConfigurator
    {
        void Protocol(ShovelProtocolType protocol);

        void DeleteAfter(DeleteShovelAfterMode mode);
        
        void DeleteAfter(int value);

        void MaxCopiedMessages(long messages);

        void Exchange(string exchange, string routingKey);
    }
}