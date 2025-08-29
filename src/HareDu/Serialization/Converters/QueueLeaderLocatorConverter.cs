namespace HareDu.Serialization.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Model;

public class QueueLeaderLocatorConverter :
    JsonConverter<QueueLeaderLocator>
{
    public override QueueLeaderLocator Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "client-local" => QueueLeaderLocator.ClientLocal,
            "balanced" => QueueLeaderLocator.Balanced,
            _ => throw new JsonException()
        };
    }

    public override void Write(Utf8JsonWriter writer, QueueLeaderLocator value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case QueueLeaderLocator.ClientLocal:
                writer.WriteStringValue("client-local");
                break;

            case QueueLeaderLocator.Balanced:
                writer.WriteStringValue("balanced");
                break;

            default:
                throw new JsonException();
        }
    }
}