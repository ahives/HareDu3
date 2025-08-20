namespace HareDu.Shovel.Serialization.Converters;

using System.Text.Json;
using System.Text.Json.Serialization;
using Model;

public class ShovelAckModeEnumConverter :
    JsonConverter<ShovelAckMode>
{
    public override ShovelAckMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "on-confirm" => ShovelAckMode.OnConfirm,
            "on-publish" => ShovelAckMode.OnPublish,
            "no-ack" => ShovelAckMode.NoAck,
            _ => throw new JsonException()
        };
    }

    public override void Write(Utf8JsonWriter writer, ShovelAckMode value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case ShovelAckMode.OnConfirm:
                writer.WriteStringValue("on-confirm");
                break;

            case ShovelAckMode.OnPublish:
                writer.WriteStringValue("on-publish");
                break;

            case ShovelAckMode.NoAck:
                writer.WriteStringValue("no-ack");
                break;

            default:
                throw new JsonException();
        }
    }
}