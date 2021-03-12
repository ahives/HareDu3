namespace HareDu
{
    public interface ShovelDestinationConfigurator
    {
        /// <summary>
        /// Sets the protocol that is to be used to shovel messages.
        /// </summary>
        /// <param name="protocol">Enumeration of protocols.</param>
        void Protocol(ShovelProtocolType protocol);

        /// <summary>
        /// Sets the exchange and its routing key.
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="routingKey"></param>
        void Exchange(string exchange, string routingKey);

        /// <summary>
        /// Sets the 'x-shovelled' header on messages that are shoveled.
        /// </summary>
        void AddForwardHeaders();
        
        /// <summary>
        /// Sets the 'x-shovelled-timestamp' header on shoveled messages.
        /// </summary>
        void AddTimestampHeaderToMessage();
    }
}