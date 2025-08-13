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
            "classic" => DefaultQueueType.Classic,
            "quorum" => DefaultQueueType.Quorum,
            "stream" => DefaultQueueType.Stream,
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

            case DefaultQueueType.Classic:
                writer.WriteStringValue("classic");
                break;

            case DefaultQueueType.Quorum:
                writer.WriteStringValue("quorum");
                break;

            case DefaultQueueType.Stream:
                writer.WriteStringValue("stream");
                break;

            default:
                throw new JsonException();
        }
    }
}