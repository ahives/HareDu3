namespace HareDu
{
    using System;

    public interface NewBindingConfigurator
    {
        /// <summary>
        /// Specify the binding source (i.e. queue/exchange).
        /// </summary>
        /// <param name="binding"></param>
        void Source(string binding);

        /// <summary>
        /// Specify the binding destination (i.e. queue/exchange).
        /// </summary>
        /// <param name="binding"></param>
        void Destination(string binding);

        /// <summary>
        /// Specify the binding type (i.e. queue or exchange).
        /// </summary>
        /// <param name="bindingType"></param>
        void Type(BindingType bindingType);

        /// <summary>
        /// Specify how the binding will be set up rout messages.
        /// </summary>
        /// <param name="routingKey"></param>
        void HasRoutingKey(string routingKey);
        
        /// <summary>
        /// Specify user-defined binding arguments.
        /// </summary>
        /// <param name="arguments"></param>
        void HasArguments(Action<BindingArguments> arguments);
    }
}