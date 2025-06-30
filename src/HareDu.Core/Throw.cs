namespace HareDu.Core;

using System;

public static class Throw
{
    /// <summary>
    /// Validates the specified object or delegate and checks if it is null.
    /// Throws an exception of the specified type with the provided error message if the object is null.
    /// </summary>
    /// <param name="obj">The object or delegate to be validated.</param>
    /// <param name="message">The message that describes the error.</param>
    /// <typeparam name="T">The type of the object to be validated.</typeparam>
    /// <typeparam name="TException">The type of exception to be thrown if the validation fails.</typeparam>
    /// <exception cref="TException">Thrown when the specified object or delegate is null.</exception>
    public static void IfNull<T, TException>(Action<T> obj, string message)
        where TException : Exception, new()
    {
        if (obj is null)
            throw ((TException) Activator.CreateInstance(typeof(TException), message))!;
    }

    /// <summary>
    /// Checks if the specified object is null and, if it is, throws an exception of the specified type with the provided error message.
    /// </summary>
    /// <param name="obj">The object to be validated for null.</param>
    /// <param name="message">The message to be included in the exception if the object is null.</param>
    /// <typeparam name="T">The type of the object to be validated.</typeparam>
    /// <typeparam name="TException">The type of exception to be thrown if the validation fails.</typeparam>
    /// <exception cref="TException">Thrown when the specified object is null.</exception>
    public static void IfNull<T, TException>(T obj, string message)
        where TException : Exception, new()
    {
        if (obj is null)
            throw ((TException) Activator.CreateInstance(typeof(TException), message))!;
    }

    /// <summary>
    /// Checks if a specific key is not found in the provided function and throws an exception of the specified type with the given error message if the key is missing.
    /// </summary>
    /// <param name="contains">The function to check for the existence of the key.</param>
    /// <param name="key">The key to validate if it exists.</param>
    /// <param name="message">The message that describes the error to be included in the exception.</param>
    /// <typeparam name="TException">The type of exception to be thrown if the key is not found.</typeparam>
    /// <exception cref="TException">Thrown when the key is not found in the provided function.</exception>
    public static void IfNotFound<TException>(Func<string, bool> contains, string key, string message)
        where TException : Exception, new()
    {
        if (string.IsNullOrWhiteSpace(key) || contains is null || !contains(key))
            throw ((TException) Activator.CreateInstance(typeof(TException), message))!;
    }
}