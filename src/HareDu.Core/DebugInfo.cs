namespace HareDu.Core;

using System.Collections.Generic;

/// <summary>
/// Represents detailed debugging information useful for troubleshooting
/// issues in an application, including request details, exception information,
/// stack trace, and errors.
/// </summary>
public record DebugInfo
{
    /// <summary>
    /// Gets the URL associated with the operation or request that was executed.
    /// </summary>
    public string URL { get; init; }

    /// <summary>
    /// Represents the raw request data associated with the operation or request.
    /// </summary>
    public string Request { get; init; }

    /// <summary>
    /// Gets the exception message providing details about the nature of the error
    /// that occurred during the execution of an operation or process.
    /// </summary>
    public string Exception { get; init; }

    /// <summary>
    /// Gets the stack trace information associated with the exception or error that occurred during an operation.
    /// </summary>
    public string StackTrace { get; init; }

    /// <summary>
    /// Gets the raw response data returned by the operation or request, if available.
    /// </summary>
    public string Response { get; init; }

    /// <summary>
    /// Gets the collection of errors that occurred during the operation or process,
    /// providing detailed information about each error.
    /// </summary>
    public IReadOnlyList<Error> Errors { get; init; }
}