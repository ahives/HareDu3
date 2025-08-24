namespace HareDu.Core.Extensions;

using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public static class JsonExtensions
{
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
}