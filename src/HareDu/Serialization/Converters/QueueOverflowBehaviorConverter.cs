namespace HareDu.Serialization.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Model;

public class QueueOverflowBehaviorConverter :
    JsonConverter<QueueOverflowBehavior>
{
    public override QueueOverflowBehavior Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "drop-head" => QueueOverflowBehavior.DropHead,
            "reject-publish" => QueueOverflowBehavior.RejectPublish,
            "reject-publish-dlx" => QueueOverflowBehavior.RejectPublishDeadLetter,
            _ => throw new JsonException()
        };
    }

    public override void Write(Utf8JsonWriter writer, QueueOverflowBehavior value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case QueueOverflowBehavior.DropHead:
                writer.WriteStringValue("drop-head");
                break;

            case QueueOverflowBehavior.RejectPublish:
                writer.WriteStringValue("reject-publish");
                break;

            case QueueOverflowBehavior.RejectPublishDeadLetter:
                writer.WriteStringValue("reject-publish-dlx");
                break;

            default:
                throw new JsonException();
        }
    }
}