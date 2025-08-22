namespace HareDu.Serialization;

using System.Text.Json;
using System.Text.Json.Serialization;
using Converters;
using Core.Serialization;
using Core.Serialization.Converters;

/// <summary>
/// Provides an implementation for serializing and deserializing JSON objects specific to the broker context.
/// </summary>
/// <remarks>
/// This class extends <see cref="BaseHareDuDeserializer"/> to include additional JSON converters used to handle
/// custom serialization for specific types related to the broker context, such as enumerations and data types.
/// </remarks>
/// <example>
/// This class is typically used as part of the HareDu framework for deserializing JSON objects into strongly typed
/// objects and vice versa.
/// </example>
/// <seealso cref="BaseHareDuDeserializer"/>
/// <seealso cref="IHareDuDeserializer"/>
public class BrokerDeserializer :
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
                new AckModeEnumConverter(),
                new DefaultQueueTypeEnumConverter(),
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };
}