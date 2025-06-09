namespace HareDu.Model;

using Core;

/// <summary>
/// Represents a wrapper for an argument's value, providing the value itself and associated error information if applicable.
/// </summary>
/// <typeparam name="T">The type of the encapsulated value.</typeparam>
public record ArgumentValue<T>
{
    public T Value { get; }
    public Error Error { get; }

    public ArgumentValue(T value, Error error =  null)
    {
        Value = value;

        if (error is not null)
            Error = error;
    }
}