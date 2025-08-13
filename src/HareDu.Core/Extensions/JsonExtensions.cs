namespace HareDu.Core.Extensions;

using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public static class JsonExtensions
{
    /// <summary>
    /// Serializes the specified object into a JSON string representation using the provided serialization options.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="obj">The object to serialize into a JSON string.</param>
    /// <param name="options">The JSON serializer options used for customizing the serialization process.</param>
    /// <returns>A JSON string representation of the object, or an empty string if the object is null.</returns>
    public static string ToJsonString<T>(this T obj, JsonSerializerOptions options) =>
        obj is null ? string.Empty : JsonSerializer.Serialize(obj, options);

    /// <summary>
    /// Deserializes the JSON content of the HTTP response message into an object of the specified type using the specified serialization options.
    /// </summary>
    /// <param name="responseMessage">The HTTP response message containing JSON data.</param>
    /// <param name="options">The serialization options to use during the deserialization process.</param>
    /// <typeparam name="T">The type of the object to deserialize the JSON data into.</typeparam>
    /// <returns>A task that represents the asynchronous operation, containing the deserialized object of type T.</returns>
    public static async Task<T> ToObject<T>(this HttpResponseMessage responseMessage, JsonSerializerOptions options)
    {
        string rawResponse = await responseMessage.Content.ReadAsStringAsync();

        return string.IsNullOrWhiteSpace(rawResponse)
            ? default
            : JsonSerializer.Deserialize<T>(rawResponse, options);
    }

    /// <summary>
    /// Converts the provided JSON string into an object of the specified type using the given serialization options.
    /// </summary>
    /// <param name="value">The JSON string to be deserialized into an object.</param>
    /// <param name="options">The serialization options to use during the deserialization process.</param>
    /// <typeparam name="T">The type of the object that the JSON string will be deserialized into.</typeparam>
    /// <returns>The deserialized object of type T, or the default value if the input string is null or whitespace.</returns>
    public static T ToObject<T>(this string value, JsonSerializerOptions options) =>
        string.IsNullOrWhiteSpace(value)
            ? default
            : JsonSerializer.Deserialize<T>(value, options);
}