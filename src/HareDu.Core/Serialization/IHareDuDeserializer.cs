namespace HareDu.Core.Serialization;

using System.Text.Json;

/// <summary>
/// Represents a contract for a deserialization engine that provides functionality
/// for serializing objects to JSON and deserializing JSON into objects.
/// </summary>
public interface IHareDuDeserializer
{
    /// <summary>
    /// Represents the serialization options used by the deserializer to configure how
    /// objects are serialized to JSON and deserialized from JSON.
    /// </summary>
    JsonSerializerOptions Options { get; }

    /// <summary>
    /// Serializes the specified object into a JSON string representation using the provided serialization options.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="obj">The object to serialize into a JSON string.</param>
    /// <param name="options">The JSON serializer options used for customizing the serialization process.</param>
    /// <returns>A JSON string representation of the object, or an empty string if the object is null.</returns>
    string ToJsonString<T>(T obj, JsonSerializerOptions options);

    /// <summary>
    /// Converts the provided JSON string into an object of the specified type using the given serialization options.
    /// </summary>
    /// <typeparam name="T">The type of the object to which the JSON string is deserialized.</typeparam>
    /// <param name="value">The JSON string to be deserialized into an object.</param>
    /// <param name="options">The JSON serialization options used to customize the deserialization process.</param>
    /// <returns>The object of type T obtained by deserializing the JSON string, or the default value if the input string is null or whitespace.</returns>
    T ToObject<T>(string value, JsonSerializerOptions options);
}