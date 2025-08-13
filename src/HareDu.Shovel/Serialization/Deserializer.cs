namespace HareDu.Shovel.Serialization;

using System.Text.Json;
using System.Text.Json.Serialization;
using Core.Serialization.Converters;

public static class Deserializer
{
    public static JsonSerializerOptions Options =>
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
                new ShovelAckModeEnumConverter(),
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };
}