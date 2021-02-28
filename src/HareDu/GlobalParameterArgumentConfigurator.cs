namespace HareDu
{
    public interface GlobalParameterArgumentConfigurator
    {
        /// <summary>
        /// Create a new argument.
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        void Add<T>(string arg, T value);
    }
}