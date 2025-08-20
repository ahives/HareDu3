namespace HareDu.Core.Serialization;

using System.Text.Json;
using System.Text.Json.Serialization;
using Converters;

public abstract class BaseHareDuDeserializer :
    IHareDuDeserializer
{
    public virtual JsonSerializerOptions Options =>
        new()
        {
            WriteIndented = true,
            Converters =
            {
                new CustomDecimalConverter(),
                new CustomDateTimeConverter(),
                new CustomLongConverter(),
                new CustomULongConverter(),
                new CustomStringConverter(),
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };

    public virtual string ToJsonString<T>(T obj, JsonSerializerOptions options) =>
        obj is null ? string.Empty : JsonSerializer.Serialize(obj, options);

    public virtual T ToObject<T>(string value, JsonSerializerOptions options) =>
        string.IsNullOrWhiteSpace(value)
            ? default
            : JsonSerializer.Deserialize<T>(value, options);
}