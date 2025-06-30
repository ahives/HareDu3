namespace HareDu.Core.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;

public static class ErrorExtensions
{
    /// <summary>
    /// Adds the specified error to the collection if the provided condition evaluates to true for the given value.
    /// </summary>
    /// <param name="errors">The list of errors to which the error will be added if the condition is met.</param>
    /// <param name="value">The value to be evaluated by the condition function.</param>
    /// <param name="condition">The condition function that evaluates the supplied value. It should return true if the error needs to be added.</param>
    /// <param name="error">The error to be added to the collection if the condition evaluates to true.</param>
    /// <typeparam name="T">The type of the value being evaluated by the condition function.</typeparam>
    public static void AddIfTrue<T>(this List<Error> errors, T value, Func<T, bool> condition, Error error)
    {
        if (errors is not null && condition is not null && condition(value))
            errors.Add(error);
    }

    /// <summary>
    /// Adds an error to the collection if the specified condition evaluates to true
    /// when comparing two values of the specified type.
    /// </summary>
    /// <param name="errors">The list of errors to which the new error will be added if the condition is met.</param>
    /// <param name="value1">The first value to evaluate in the condition.</param>
    /// <param name="value2">The second value to evaluate in the condition.</param>
    /// <param name="condition">The condition that evaluates two values to determine whether the error should be added.</param>
    /// <param name="error">The error to add to the collection if the condition is met.</param>
    /// <typeparam name="T">The type of the values to be compared in the condition.</typeparam>
    public static void AddIfTrue<T>(this List<Error> errors, T value1, T value2, Func<T, T, bool> condition, Error error)
    {
        if (errors is not null && condition is not null && condition(value1, value2))
            errors.Add(error);
    }

    /// <summary>
    /// Adds the specified error to the collection if the provided condition evaluates to true.
    /// </summary>
    /// <param name="errors">The list of errors to which the new error will be added if the condition is true.</param>
    /// <param name="condition">The condition to evaluate. If true, the error will be added to the list.</param>
    /// <param name="error">The error to add to the collection if the condition is satisfied.</param>
    public static void AddIfTrue(this List<Error> errors, Func<bool> condition, Error error)
    {
        if (errors is not null && condition is not null && condition())
            errors.Add(error);
    }

    /// <summary>
    /// Determines whether the specified list of errors contains any elements.
    /// </summary>
    /// <param name="errors">The list of errors to be checked for existence of elements.</param>
    /// <returns>True if the list of errors contains one or more elements; otherwise, false.</returns>
    public static bool HaveBeenFound(this List<Error> errors) => errors is not null && errors.Count > 0;

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