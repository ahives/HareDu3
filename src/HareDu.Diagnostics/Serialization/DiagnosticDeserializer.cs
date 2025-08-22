namespace HareDu.Diagnostics.Serialization;

using System.Text.Json;
using System.Text.Json.Serialization;
using Converters;
using Core.Serialization;
using Core.Serialization.Converters;

/// <summary>
/// The DiagnosticDeserializer class is responsible for defining JSON serialization settings specifically tailored
/// for deserializing objects and transforming them into a specific diagnostic context.
/// </summary>
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