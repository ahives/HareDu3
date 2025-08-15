namespace HareDu.Serialization.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Model;

public class QueueSyncActionEnumConverter :
    JsonConverter<QueueSyncAction>
{
    public override QueueSyncAction Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options) =>
        reader.GetString() switch
        {
            "sync" => QueueSyncAction.Sync,
            "cancel_sync" => QueueSyncAction.CancelSync,
            _ => throw new JsonException()
        };

    public override void Write(Utf8JsonWriter writer, QueueSyncAction value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case QueueSyncAction.Sync:
                writer.WriteStringValue("sync");
                break;
                
            case QueueSyncAction.CancelSync:
                writer.WriteStringValue("cancel_sync");
                break;
                
            default:
                throw new JsonException();
        }
    }
}