namespace HareDu.Core.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;

public static class ErrorExtensions
{
    /// <summary>
    /// Adds a new error entry to the collection with the specified reason and criticality.
    /// </summary>
    /// <param name="errors">The list of errors to which the new error will be added.</param>
    /// <param name="reason">The reason or message describing the error.</param>
    /// <param name="criticality">The criticality level of the error. Defaults to "Critical" if not specified.</param>
    public static void Add(this List<Error> errors, string reason, ErrorCriticality criticality = ErrorCriticality.Critical)
    {
        errors?.Add(new() {Reason = reason, Criticality = criticality, Timestamp = DateTimeOffset.UtcNow});
    }

    /// <summary>
    /// Retrieves a list of errors that have a criticality level of "Critical".
    /// </summary>
    /// <param name="errors">The collection of errors to filter.</param>
    /// <returns>A read-only list of errors with criticality level "Critical". If the input collection is null, an empty list is returned.</returns>
    public static IReadOnlyList<Error> GetCritical(this IReadOnlyList<Error> errors) =>
        errors is null
        ? Array.Empty<Error>()
        : errors.Where(x => x.Criticality == ErrorCriticality.Critical).ToList();
}