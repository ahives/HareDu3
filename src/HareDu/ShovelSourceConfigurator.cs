namespace HareDu
{
    public interface ShovelSourceConfigurator
    {
        /// <summary>
        /// Sets the protocol that is to be used to shovel messages.
        /// </summary>
        /// <param name="protocol"></param>
        void Protocol(ShovelProtocolType protocol);

        /// <summary>
        /// Determines when the shovel should delete itself.
        /// </summary>
        /// <param name="mode"></param>
        void DeleteAfter(DeleteShovelAfterMode mode);
        
        /// <summary>
        /// The shovel will delete itself after the number of <see cref="messages"/> is reached.
        /// </summary>
        /// <param name="messages"></param>
        void DeleteAfter(uint messages);

        /// <summary>
        /// Sets the prefetch count on the shovel.
        /// </summary>
        /// <param name="messages"></param>
        void MaxCopiedMessages(ulong messages);

        /// <summary>
        /// Sets the exchange and its routing key.
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="routingKey"></param>
        void Exchange(string exchange, string routingKey);
    }
}