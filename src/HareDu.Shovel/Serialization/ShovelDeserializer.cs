namespace HareDu.Shovel.Serialization;

using System.Text.Json;
using System.Text.Json.Serialization;
using Converters;
using Core.Serialization;
using Core.Serialization.Converters;

public class ShovelDeserializer :
    BaseHareDuDeserializer
{
    public override JsonSerializerOptions Options =>
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