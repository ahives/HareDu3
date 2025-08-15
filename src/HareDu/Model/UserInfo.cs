namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents information related to a RabbitMQ user.
/// </summary>
public record UserInfo
{
    /// <summary>
    /// Represents the name of the user.
    /// </summary>
    /// <remarks>
    /// The Username property identifies the user's unique name within the system.
    /// It is used to distinguish users and form the basis of various operations such as authentication and user management.
    /// This property is often paired with additional information like the user's password hash, tags, and hashing algorithm.
    /// </remarks>
    [JsonPropertyName("name")]
    public string Username { get; init; }

    /// <summary>
    /// Stores the hashed representation of the user's password.
    /// </summary>
    /// <remarks>
    /// The PasswordHash property contains the cryptographic hash of the user's password,
    /// used to ensure secure authentication and protect sensitive user credentials.
    /// It is generated using the specified hashing algorithm as indicated by the associated
    /// HashingAlgorithm property.
    /// </remarks>
    [JsonPropertyName("password_hash")]
    public string PasswordHash { get; init; }

    /// <summary>
    /// Represents the algorithm used for hashing the user's password.
    /// </summary>
    /// <remarks>
    /// The HashingAlgorithm property indicates the type of hashing algorithm applied during password encryption,
    /// such as "rabbit_password_hashing_sha256". This property is utilized for ensuring secure password storage
    /// by using a specific cryptographic hashing method.
    /// </remarks>
    [JsonPropertyName("hashing_algorithm")]
    public string HashingAlgorithm { get; init; }

    /// <summary>
    /// Defines the set of tags associated with the user.
    /// </summary>
    /// <remarks>
    /// Tags specify the roles, permissions, or attributes assigned to a user,
    /// such as "administrator", "monitoring", or custom roles.
    /// </remarks>
    [JsonPropertyName("tags")]
    public string Tags { get; init; }
}