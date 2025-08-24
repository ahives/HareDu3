namespace HareDu.Core;

using System;
using System.Collections.Generic;
using System.Net;

public static class Errors
{
    /// <summary>
    /// Creates and returns a new <see cref="Error"/> instance with the specified parameters.
    /// </summary>
    /// <param name="reason">The reason or description for the error.</param>
    /// <param name="source">The source of the error, either internal or external. Defaults to <see cref="RequestSource.Internal"/>.</param>
    /// <param name="type">The type of the error, such as validation or broker-related. Defaults to <see cref="RequestType.Validation"/>.</param>
    /// <param name="criticality">The criticality level of the error, such as critical or non-critical. Defaults to <see cref="ErrorCriticality.Critical"/>.</param>
    /// <returns>A new <see cref="Error"/> instance populated with the provided details.</returns>
    public static Error Create(string reason, RequestSource source = RequestSource.Internal,
        RequestType type = RequestType.Validation, ErrorCriticality criticality = ErrorCriticality.Critical) =>
        new()
        {
            Reason = reason, Source = source, Type = type, Criticality = criticality, Timestamp = DateTimeOffset.UtcNow
        };

    /// <summary>
    /// Creates and returns a collection of <see cref="Error"/> instances based on the parameters
    /// provided through the specified <see cref="IErrorCreator"/> action.
    /// </summary>
    /// <param name="creator">An action that configures the errors to be created by invoking methods on an <see cref="IErrorCreator"/> instance.</param>
    /// <returns>An <see cref="IReadOnlyList{T}"/> of <see cref="Error"/> objects containing the details specified in the provided action.</returns>
    public static IReadOnlyList<Error> Create(Action<IErrorCreator> creator)
    {
        if (creator is null)
            return [];
        
        var impl = new ErrorCreatorImpl();
        creator(impl);

        return impl.Errors;
    }

    
    class ErrorCreatorImpl :
        IErrorCreator
    {
        public readonly List<Error> Errors = new();

        public void Add(string reason, RequestType type = RequestType.Validation, RequestSource source = RequestSource.Internal,
            ErrorCriticality criticality = ErrorCriticality.Critical)
        {
            Errors.Add(new Error {Reason = reason, Criticality = criticality, Type = type, Source = source, Timestamp = DateTimeOffset.Now});
        }

        public void Add(HttpStatusCode statusCode, RequestType type)
        {
            Errors.Add(GetError(statusCode, type));
        }

        Error GetError(HttpStatusCode statusCode, RequestType type)
        {
            string reason = statusCode switch
            {
                HttpStatusCode.BadRequest => "RabbitMQ server did not recognize the request due to malformed syntax (400) or invalid request message framing (e.g. not an HTTP/1.1 request).",
                HttpStatusCode.Forbidden => "RabbitMQ server rejected the request (403) because the user does not have the necessary permissions to access the resource.",
                HttpStatusCode.NotAcceptable => "RabbitMQ server rejected the request because the method is not acceptable (406) for the resource.",
                HttpStatusCode.MethodNotAllowed => "RabbitMQ server rejected the request because the method is not allowed (405) for the resource.",
                HttpStatusCode.InternalServerError => "Internal error happened on RabbitMQ server (500) that prevented it from fulfilling the request.",
                HttpStatusCode.RequestTimeout => "No response from the RabbitMQ server within the specified window of time (408) due to a timeout.",
                HttpStatusCode.ServiceUnavailable => "RabbitMQ server temporarily not able to handle request (503) due to overload or maintenance.",
                HttpStatusCode.Unauthorized => "Unauthorized access to RabbitMQ server resource (401) due to missing credentials or invalid credentials.",
                HttpStatusCode.TooManyRequests => "Calls to HareDu API exceeded the allowable maximum of requests to the RabbitMQ server (429).",
                HttpStatusCode.NotFound => "RabbitMQ server resource not found (404).",
                _ => null
            };
            
            return new()
            {
                Reason = reason,
                Criticality = ErrorCriticality.Critical,
                Source = RequestSource.External,
                Type = type,
                Timestamp = DateTimeOffset.UtcNow
            };

        }
    }
}