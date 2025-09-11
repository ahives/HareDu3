namespace HareDu.Diagnostics.Serialization;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
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
    static readonly Lock _lock = new();
    static DiagnosticDeserializer _instance;

    public static IHareDuDeserializer Instance
    {
        get
        {
            if (_instance is not null)
                return _instance;

            lock (_lock)
            {
                _instance = new DiagnosticDeserializer();
            }

            return _instance;
        }
    }
    
    DiagnosticDeserializer()
    {
        Options =
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
}