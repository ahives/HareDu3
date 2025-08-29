namespace HareDu.Serialization.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Model;

public class PolicyAppliedToConverter :
    JsonConverter<PolicyAppliedTo>
{
    public override PolicyAppliedTo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch
        {
            "classic_queues" => PolicyAppliedTo.ClassicQueues,
            "queues" => PolicyAppliedTo.Queues,
            "quorum_queues" => PolicyAppliedTo.QuorumQueues,
            "streams" => PolicyAppliedTo.Streams,
            "all" => PolicyAppliedTo.QueuesAndExchanges,
            _ => throw new JsonException()
        };
    }

    public override void Write(Utf8JsonWriter writer, PolicyAppliedTo value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case PolicyAppliedTo.ClassicQueues:
                writer.WriteStringValue("classic_queues");
                break;

            case PolicyAppliedTo.Queues:
                writer.WriteStringValue("queues");
                break;

            case PolicyAppliedTo.QuorumQueues:
                writer.WriteStringValue("quorum_queues");
                break;

            case PolicyAppliedTo.Streams:
                writer.WriteStringValue("streams");
                break;

            case PolicyAppliedTo.QueuesAndExchanges:
                writer.WriteStringValue("all");
                break;

            default:
                throw new JsonException();
        }
    }
}