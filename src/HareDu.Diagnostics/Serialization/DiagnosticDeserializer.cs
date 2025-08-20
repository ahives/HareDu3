namespace HareDu.Diagnostics.Serialization;

using System.Text.Json;
using System.Text.Json.Serialization;
using Converters;
using Core.Serialization;
using Core.Serialization.Converters;

public class DiagnosticDeserializer :
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
                new ProbeResultStatusEnumConverter(),
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };
}