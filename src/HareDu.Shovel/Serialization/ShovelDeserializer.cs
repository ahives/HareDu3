namespace HareDu.Shovel.Serialization;

using System.Text.Json;
using System.Text.Json.Serialization;
using Converters;
using Core.Serialization;
using Core.Serialization.Converters;

public class ShovelDeserializer :
    BaseHareDuDeserializer
{
    static readonly Lock _lock = new();
    static ShovelDeserializer _instance;

    public static IHareDuDeserializer Instance
    {
        get
        {
            if (_instance is not null)
                return _instance;

            lock (_lock)
            {
                _instance = new ShovelDeserializer();
            }

            return _instance;
        }
    }
    
    ShovelDeserializer()
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
                    new ShovelAckModeEnumConverter(),
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
    }
}