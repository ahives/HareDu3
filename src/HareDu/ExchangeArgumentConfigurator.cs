namespace HareDu;

/// <summary>
/// Provides methods to configure arguments for an exchange in a messaging system.
/// </summary>
public interface ExchangeArgumentConfigurator
{
    /// <summary>
    /// Adds a new exchange argument with the specified name and value.
    /// </summary>
    /// <param name="arg">The name of the argument to add.</param>
    /// <param name="value">The value associated with the argument.</param>
    /// <typeparam name="T">The type of the argument value.</typeparam>
    void Add<T>(string arg, T value);
}