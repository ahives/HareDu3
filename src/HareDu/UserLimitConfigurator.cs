namespace HareDu;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents the configuration of resource limits for a specific RabbitMQ user.
/// </summary>
public interface UserLimitConfigurator
{
    /// <summary>
    /// Specifies a limit and its corresponding value for a RabbitMQ resource for a user.
    /// </summary>
    /// <param name="limit">
    /// The type of resource limit to be set, such as maximum connections or maximum channels.
    /// </param>
    /// <param name="value">
    /// The value of the specified resource limit. It defines the threshold for the limit being applied.
    /// </param>
    void SetLimit([NotNull] UserLimit limit, [NotNull] ulong value);
}