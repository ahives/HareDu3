namespace HareDu.Core.Serialization;

using System.Text.Json;

public abstract class BaseHareDuDeserializer :
    IHareDuDeserializer
{
    protected JsonSerializerOptions Options { get; init; }

    public virtual string ToJsonString<T>(T obj, JsonSerializerOptions options) =>
        obj is null ? string.Empty : JsonSerializer.Serialize(obj, options);

    public virtual string ToJsonString<T>(T obj) => obj is null ? string.Empty : JsonSerializer.Serialize(obj, Options);

    public virtual T ToObject<T>(string value, JsonSerializerOptions options) =>
        string.IsNullOrWhiteSpace(value)
            ? default
            : JsonSerializer.Deserialize<T>(value, options);

    public T ToObject<T>(string value) =>
        string.IsNullOrWhiteSpace(value)
            ? default
            : JsonSerializer.Deserialize<T>(value, Options);
}