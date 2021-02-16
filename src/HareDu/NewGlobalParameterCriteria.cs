namespace HareDu
{
    using System;

    public interface NewGlobalParameterCriteria
    {
        /// <summary>
        /// Specify global parameter arguments.
        /// </summary>
        /// <param name="arguments"></param>
        void Value(Action<GlobalParameterArguments> arguments);
        
        /// <summary>
        /// Specify global parameter argument.
        /// </summary>
        /// <param name="arguments"></param>
        void Value<T>(T argument);
    }
}