namespace HareDu.Serialization.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Model;

public class DefaultQueueTypeEnumConverter :
    JsonConverter<DefaultQueueType>
{
    public override DefaultQueueType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "undefined" => DefaultQueueType.Undefined,
            _ => throw new JsonException()
        };
    }

    public override void Write(Utf8JsonWriter writer, DefaultQueueType value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case DefaultQueueType.Undefined:
                writer.WriteStringValue("undefined");
                break;

            default:
                throw new JsonException();
        }
    }
}