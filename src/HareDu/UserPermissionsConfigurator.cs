namespace HareDu;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Configures user permissions for a specific virtual host in the RabbitMQ broker.
/// </summary>
public interface UserPermissionsConfigurator
{
    /// <summary>
    /// Defines the configuration pattern for the user on a specific virtual host in the RabbitMQ broker.
    /// </summary>
    /// <param name="pattern">The regular expression pattern specifying configuration permissions for the user.</param>
    void UsingConfigurePattern([AllowNull] string pattern);

    /// <summary>
    /// Sets the write permissions pattern for the user on a specific virtual host in the RabbitMQ broker.
    /// </summary>
    /// <param name="pattern">The regular expression pattern defining write permissions for the user.</param>
    void UsingWritePattern([AllowNull] string pattern);

    /// <summary>
    /// Sets the read permissions pattern for the user on a specific virtual host in the RabbitMQ broker.
    /// </summary>
    /// <param name="pattern">The regular expression pattern defining read permissions for the user.</param>
    void UsingReadPattern([AllowNull] string pattern);
}