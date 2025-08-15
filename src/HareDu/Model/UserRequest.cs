namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a request object used to manage user creation or update operations within the system.
/// </summary>
public record UserRequest
{
    /// <summary>
    /// Represents the hashed password of a user in the system.
    /// </summary>
    /// <remarks>
    /// The password hash is primarily used for secure storage and authentication purposes.
    /// It is mutually exclusive with the plaintext password; if a plaintext password is provided, the password hash should be null.
    /// </remarks>
    [JsonPropertyName("password_hash")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string PasswordHash { get; init; }

    /// <summary>
    /// Represents the plaintext password of a user in the system.
    /// </summary>
    /// <remarks>
    /// The password is used for user authentication when creating or managing user accounts.
    /// It is expected to be provided when the password hash is not supplied.
    /// </remarks>
    [JsonPropertyName("password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Password { get; init; }

    /// <summary>
    /// Represents the tags associated with the user's permissions and roles in the system.
    /// </summary>
    /// <remarks>
    /// Tags can define user roles, such as "administrator" or "management". Multiple tags can be combined by separating them with commas.
    /// </remarks>
    [JsonPropertyName("tags")]
    public string Tags { get; init; }
}