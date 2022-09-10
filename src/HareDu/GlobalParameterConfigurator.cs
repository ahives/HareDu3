namespace HareDu;

using System;

public interface GlobalParameterConfigurator
{
    /// <summary>
    /// Specify global parameter arguments.
    /// </summary>
    /// <param name="configurator"></param>
    void Value(Action<GlobalParameterArgumentConfigurator> configurator);
        
    /// <summary>
    /// Specify global parameter argument.
    /// </summary>
    /// <param name="argument"></param>
    void Value<T>(T argument);
}