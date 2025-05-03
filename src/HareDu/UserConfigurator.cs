namespace HareDu;

using System;

/// <summary>
/// Defines methods to configure additional settings for a user.
/// </summary>
public interface UserConfigurator
{
    /// <summary>
    /// Assigns user access tags to the user.
    /// </summary>
    /// <param name="tags">The action that configures the user access tags to be assigned.</param>
    void WithTags(Action<UserAccessOptions> tags);
}