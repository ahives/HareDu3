namespace HareDu.Serialization.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Model;

public class DeadLetterQueueStrategyConverter :
    JsonConverter<DeadLetterQueueStrategy>
{
    public override DeadLetterQueueStrategy Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "at-most-once" => DeadLetterQueueStrategy.AtMostOnce,
            "at-least-once" => DeadLetterQueueStrategy.AtLeastOnce,
            _ => throw new JsonException()
        };
    }

    public override void Write(Utf8JsonWriter writer, DeadLetterQueueStrategy value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case DeadLetterQueueStrategy.AtMostOnce:
                writer.WriteStringValue("at-most-once");
                break;

            case DeadLetterQueueStrategy.AtLeastOnce:
                writer.WriteStringValue("at-least-once");
                break;

            default:
                throw new JsonException();
        }
    }
}