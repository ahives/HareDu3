namespace HareDu
{
    using System;

    public interface DeleteExchangeConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        void Exchange(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurator"></param>
        void Configure(Action<DeleteExchangeConfigurator> configurator);
        
        /// <summary>
        /// Specify the target for which the exchange will be deleted.
        /// </summary>
        /// <param name="target">Define the location where the exchange (i.e. virtual host) will be deleted</param>
        void Targeting(Action<ExchangeTarget> target);
    }
}