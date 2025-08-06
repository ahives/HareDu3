namespace HareDu;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents a configurator for managing binding arguments in a messaging system.
/// </summary>
public interface BindingArgumentConfigurator
{
    /// <summary>
    /// Adds an argument and its corresponding value to the binding arguments.
    /// </summary>
    /// <param name="arg">The name of the argument to be added.</param>
    /// <param name="value">The value associated with the specified argument.</param>
    /// <typeparam name="T">The type of the value associated with the argument.</typeparam>
    void Add<T>([NotNull] string arg, [NotNull] T value);
}