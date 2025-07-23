namespace HareDu.Core;

using System.Net;

/// <summary>
/// Defines methods for creating and adding error details within an implementation.
/// </summary>
public interface IErrorCreator
{
    /// <summary>
    /// Adds an error to the collection with the specified details.
    /// </summary>
    /// <param name="reason">The reason or message describing the error.</param>
    /// <param name="type">The type of the error providing context about the issue. Defaults to <see cref="RequestType.Validation"/>.</param>
    /// <param name="source">The source of the error, indicating whether it is internal or external. Defaults to <see cref="RequestSource.Internal"/>.</param>
    /// <param name="criticality">The criticality level of the error, indicating severity. Defaults to <see cref="ErrorCriticality.Critical"/>.</param>
    void Add(string reason, RequestType type = RequestType.Validation, RequestSource source = RequestSource.Internal, ErrorCriticality criticality = ErrorCriticality.Critical);

    /// <summary>
    /// Adds an error to the collection with the specified HTTP status code and error type.
    /// </summary>
    /// <param name="statusCode">The HTTP status code representing the nature of the error.</param>
    /// <param name="type">The type of the error providing context about the issue.</param>
    void Add(HttpStatusCode statusCode, RequestType type);
}