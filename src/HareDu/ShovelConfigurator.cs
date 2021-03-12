namespace HareDu
{
    using System;

    public interface ShovelConfigurator
    {
        /// <summary>
        /// The duration to wait before reconnecting to the brokers after being disconnected at either end.
        /// </summary>
        /// <param name="delayInSeconds"></param>
        void ReconnectDelay(int delayInSeconds);

        /// <summary>
        /// Determine how the shovel should acknowledge consumed messages.
        /// </summary>shovel
        /// <param name="mode"></param>
        void AcknowledgementMode(AckMode mode);
        
        /// <summary>
        /// Describes how the shovel is confirmed from the source end.
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="uri"></param>
        /// <param name="configurator"></param>
        void Source(string queue, string uri, Action<ShovelSourceConfigurator> configurator = null);
        
        /// <summary>
        /// Describes how the shovel is confirmed from the destination end.
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="uri"></param>
        /// <param name="configurator"></param>
        void Destination(string queue, string uri, Action<ShovelDestinationConfigurator> configurator = null);
    }
}