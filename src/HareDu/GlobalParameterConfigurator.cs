namespace HareDu;

/// <summary>
/// Defines configuration methods for global parameters in the system.
/// Provides the functionality to add arguments to a parameter configuration for global parameter creation.
/// </summary>
public interface GlobalParameterConfigurator
{
    /// <summary>
    /// Adds a new argument to the global parameter configuration.
    /// </summary>
    /// <typeparam name="T">The type of the value being added.</typeparam>
    /// <param name="arg">The name of the argument to be added. This should be a non-null, non-empty string.</param>
    /// <param name="value">The value of the argument to be added. The type of the value should match the specified type parameter.</param>
    void Add<T>(string arg, T value);
}