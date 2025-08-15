namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the request payload for performing a bulk deletion of users.
/// </summary>
public record BulkUserDeleteRequest
{
    /// <summary>
    /// Represents a collection of usernames to be included in the bulk user delete request.
    /// </summary>
    [JsonPropertyName("users")]
    public IList<string> Users { get; init; }
}