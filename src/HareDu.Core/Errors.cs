namespace HareDu.Core;

using System;
using System.Collections.Generic;

public static class Errors
{
    /// <summary>
    /// Creates and returns a new <see cref="Error"/> instance with the specified parameters.
    /// </summary>
    /// <param name="reason">The reason or description for the error.</param>
    /// <param name="source">The source of the error, either internal or external. Defaults to <see cref="ErrorSource.Internal"/>.</param>
    /// <param name="type">The type of the error, such as validation or broker-related. Defaults to <see cref="ErrorType.Validation"/>.</param>
    /// <param name="criticality">The criticality level of the error, such as critical or non-critical. Defaults to <see cref="ErrorCriticality.Critical"/>.</param>
    /// <returns>A new <see cref="Error"/> instance populated with the provided details.</returns>
    public static Error Create(string reason, ErrorSource source = ErrorSource.Internal,
        ErrorType type = ErrorType.Validation, ErrorCriticality criticality = ErrorCriticality.Critical) =>
        new()
        {
            Reason = reason, Source = source, Type = type, Criticality = criticality, Timestamp = DateTimeOffset.UtcNow
        };

    public static IReadOnlyList<Error> CreateList(string reason, ErrorSource source, ErrorType type) => [Create(reason, source, type)];

    public static Error Empty() => null;
}