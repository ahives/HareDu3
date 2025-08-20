namespace HareDu.Extensions;

public static class ValueCastingExtensions
{
    /// <summary>
    /// Attempts to cast the specified object to the desired type.
    /// If the cast is successful, the casted value is returned; otherwise, the default value of the type is returned.
    /// </summary>
    /// <param name="value">The object to be cast to the specified type.</param>
    /// <typeparam name="T">The type to which the object is to be cast.</typeparam>
    /// <returns>The casted object of type <c>T</c>, or the default value of type <c>T</c> if the cast is unsuccessful.</returns>
    public static T Cast<T>(this object value)
    {
        if (value is T obj)
            return obj;

        return default;
    }
}