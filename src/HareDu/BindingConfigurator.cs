namespace HareDu
{
    using System;

    public interface BindingConfigurator
    {
        /// <summary>
        /// Specify how the binding will be set up rout messages.
        /// </summary>
        /// <param name="routingKey"></param>
        void WithRoutingKey(string routingKey);
        
        /// <summary>
        /// Specify user-defined binding arguments.
        /// </summary>
        /// <param name="arguments"></param>
        void WithArguments(Action<BindingArgumentConfigurator> arguments);
    }
}