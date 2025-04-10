namespace HareDu;

using Model;

public interface UserAccessOptions
{
    /// <summary>
    /// Adds a specific user access tag.
    /// </summary>
    /// <param name="tag">The user access tag to be added.</param>
    void AddTag(UserAccessTag tag);
}