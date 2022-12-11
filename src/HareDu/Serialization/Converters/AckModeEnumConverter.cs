namespace HareDu.Serialization.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class AckModeEnumConverter :
    JsonConverter<AckMode>
{
    public override AckMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "on-confirm" => AckMode.OnConfirm,
            "on-publish" => AckMode.OnPublish,
            "no-ack" => AckMode.NoAck,
            _ => throw new JsonException()
        };
    }

    public override void Write(Utf8JsonWriter writer, AckMode value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case AckMode.OnConfirm:
                writer.WriteStringValue("on-confirm");
                break;
                
            case AckMode.OnPublish:
                writer.WriteStringValue("on-publish");
                break;
                
            case AckMode.NoAck:
                writer.WriteStringValue("no-ack");
                break;
                
            default:
                throw new JsonException();
        }
    }
}