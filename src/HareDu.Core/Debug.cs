namespace HareDu.Core;

using System.Collections.Generic;

public static class Debug
{
    /// <summary>
    /// Creates and returns a DebugInfo object containing debug information such as URL, errors, and optional parameters like request, message, stack trace, and response.
    /// </summary>
    /// <param name="url">The URL associated with the debug information.</param>
    /// <param name="errors">A read-only list of errors related to the debug context.</param>
    /// <param name="request">Optional. The request details triggering the debug information. Default is null.</param>
    /// <param name="message">Optional. The message providing additional debug context. Default is null.</param>
    /// <param name="stackTrace">Optional. The stack trace associated with the debug event. Default is null.</param>
    /// <param name="response">Optional. The response received related to the debug event. Default is null.</param>
    /// <returns>A DebugInfo object populated with the provided debug information.</returns>
    public static DebugInfo Info(
        string url,
        IReadOnlyList<Error> errors,
        string request = null,
        string message = null,
        string stackTrace = null,
        string response = null) =>
        new()
        {
            Url = url,
            Errors = errors,
            Request = request,
            Message = message,
            StackTrace = stackTrace,
            Response = response
        };
}