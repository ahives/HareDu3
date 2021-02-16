namespace HareDu
{
    using System;

    public interface NewExchangeConfiguration
    {
        /// <summary>
        /// Specify the name of the exchange.
        /// </summary>
        /// <param name="name">RabbitMQ exchange name</param>
        void Exchange(string name);

        /// <summary>
        /// Specify how should the exchange be configured.
        /// </summary>
        /// <param name="configure">User-defined configuration</param>
        void Configure(Action<NewExchangeCriteria> configure);

        /// <summary>
        /// Specify the target for which the exchange will be created.
        /// </summary>
        /// <param name="target">Define the location of the exchange (i.e. virtual host) that is targeted for deletion</param>
        void Targeting(Action<ExchangeTarget> target);
    }
}