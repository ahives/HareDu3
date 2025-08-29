namespace HareDu.Serialization.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Model;

public class QueueModeConverter :
    JsonConverter<QueueMode>
{
    public override QueueMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "default" => QueueMode.Default,
            "lazy" => QueueMode.Lazy,
            _ => throw new JsonException()
        };
    }

    public override void Write(Utf8JsonWriter writer, QueueMode value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case QueueMode.Default:
                writer.WriteStringValue("default");
                break;

            case QueueMode.Lazy:
                writer.WriteStringValue("lazy");
                break;

            default:
                throw new JsonException();
        }
    }
}