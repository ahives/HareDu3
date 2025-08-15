namespace HareDu.Model;

/// <summary>
/// Represents a classification for user access levels in the system. Each predefined tag
/// is tied to specific permissions or roles, dictating the actions a user can perform
/// based on their associated tag.
/// </summary>
public record UserAccessTag
{
    public string Value { get; }

    private UserAccessTag(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Represents a predefined user access tag that grants the user administrative privileges.
    /// This access level typically provides full control over system configurations,
    /// resources, and user permissions.
    /// </summary>
    public static UserAccessTag Administrator = new("administrator");

    /// <summary>
    /// Represents a predefined user access tag that grants the user monitoring privileges.
    /// This access level is typically used to provide access to system monitoring tools and
    /// metrics without granting broader administrative permissions.
    /// </summary>
    public static UserAccessTag Monitoring = new("monitoring");

    /// <summary>
    /// Represents a predefined user access tag that provides management-level privileges.
    /// This access level typically allows the user to manage operational aspects of the system
    /// without full administrative permissions.
    /// </summary>
    public static UserAccessTag Management = new("management");

    /// <summary>
    /// Represents a predefined user access tag that allows the user to create and manage policies.
    /// This access level is typically associated with configuring system policies and rules
    /// without extending broader administrative privileges.
    /// </summary>
    public static UserAccessTag PolicyMaker = new("policymaker");

    /// <summary>
    /// Represents a predefined user access tag that allows the user to perform actions
    /// on behalf of other users. This access level is typically used for scenarios
    /// where user impersonation is required for auditing or delegation purposes.
    /// </summary>
    public static UserAccessTag Impersonator = new("impersonator");

    /// <summary>
    /// Represents a predefined user access tag that indicates the absence of any specific
    /// privileges or roles for a user. This is typically used as a default or unprivileged state.
    /// </summary>
    public static UserAccessTag None = new(string.Empty);
}